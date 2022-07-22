using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
         private readonly IUnitOfWork _unitOfWork;
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        //private readonly ILeaveRequestRepository _leaveRequestRepository;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
          //ILeaveAllocationRepository leaveAllocationRepository,
          //  ILeaveRequestRepository leaveRequestRepository,
          //  ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
              this._unitOfWork = unitOfWork;
            //_leaveAllocationRepository = leaveAllocationRepository;
            //  _leaveRequestRepository = leaveRequestRepository;
            //_leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userId, request.CreateLeaveRequestDto.LeaveTypeId);
            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    nameof(request.CreateLeaveRequestDto.EndDate), "You do not have any allocations for this leave type."
                ));
            }
            int daysRequest = (int) (request.CreateLeaveRequestDto.EndDate - request.CreateLeaveRequestDto.StartDate)
                .TotalDays;

            if (daysRequest > allocation.NumberOfDays)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    nameof(request.CreateLeaveRequestDto.EndDate),"You do not have enough days to this request"
                    ));
            }
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors= validationResult.Errors.Select(x=>x.ErrorCode+":"+x.ErrorMessage).ToList();
                //throw new ValidationException(validationResult);
            }

          
            try
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
                leaveRequest.RequestingEmployeeId = userId;
                leaveRequest = await _unitOfWork.LeaveRequestRepository.Add(leaveRequest);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;
                #region email send

                var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                var email = new Email
                {
                    To = emailAddress,
                    Body =
                        $"Your leave request for {request.CreateLeaveRequestDto.StartDate:D} to {request.CreateLeaveRequestDto.EndDate:D}" +
                        $" has been submitted successfully",
                    Subject = "Leave Request submitted"
                };

                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //Log or handle error, but don't throw
            }
            #endregion
            return response;
        }
    }
}