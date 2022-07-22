using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ILeaveRequestRepository _leaveRequestRepository;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            //ILeaveRequestRepository leaveRequestRepository,
            //ILeaveTypeRepository leaveTypeRepository,
            //ILeaveAllocationRepository leaveAllocationRepository,
            IEmailSender emailSender,

            IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            //_leaveRequestRepository = leaveRequestRepository;
            //_leaveTypeRepository = leaveTypeRepository;
            //_leaveAllocationRepository = leaveAllocationRepository;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.Get(request.Id);
            if (request.UpdateLeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult);
                }

                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                await _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
                await _unitOfWork.Save();

            }
            else if(request.ChangeLeaveRequestApprovalDto!=null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest,
                    request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId,
                        leaveRequest.LeaveTypeId);
                    int daysReq =
                        (int) (request.UpdateLeaveRequestDto.EndDate - request.UpdateLeaveRequestDto.StartDate)
                        .TotalDays;
                    allocation.NumberOfDays = daysReq;
                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);
                }
                await _unitOfWork.Save();
            }
         
            #region email send 
            var email = new Email
            {
                To = "ranahamid007@gmail.com",
                Body =
                    $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D}" +
                    $" has been updated successfully",
                Subject = "Leave Update Request submitted"
            };
            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //Log or handle error, but don't throw
            }
            #endregion
            return Unit.Value;
        }
    }
}