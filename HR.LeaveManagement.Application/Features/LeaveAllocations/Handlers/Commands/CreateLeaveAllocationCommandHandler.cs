using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(
            IUnitOfWork unitOfWork,
            //ILeaveAllocationRepository leaveAllocationRepository,
            //ILeaveTypeRepository leaveTypeRepository,
            IUserService userService,
            IMapper mapper )
        {
            this._unitOfWork = unitOfWork;
            //_leaveAllocationRepository = leaveAllocationRepository;
            //_leaveTypeRepository = leaveTypeRepository;
            this._userService = userService;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorCode + ":" + x.ErrorMessage).ToList();
                //throw new ValidationException(validationResult);
            }

            var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.CreateLeaveAllocationDto.LeaveTypeId);
            var employees = await _userService.GetEmployees();
            var period = DateTime.Now.Year;
            var allocations = new List<LeaveAllocation>();
            foreach (var employee in employees)
            {
                var ifExistAllocation =
                    await _unitOfWork.LeaveAllocationRepository.AllocationExists(employee.Id, leaveTypeId: leaveType.Id, period);
                if(ifExistAllocation)
                    continue;
                ;
                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays,
                });
            }

            await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);
            await _unitOfWork.Save();

            //var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
            //leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
          
            response.Success = true;
            response.Message = "Allocations Successful";
            //response.Id = leaveAllocation.Id;
            return response;
        }
    }
}