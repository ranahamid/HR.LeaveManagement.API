using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository; 
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateLeaveAllocationCommandHandler(
            IUnitOfWork unitOfWork,
           // ILeaveAllocationRepository leaveAllocationRepository,
           //ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_leaveAllocationRepository = leaveAllocationRepository;
            //_leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.UpdateLeaveAllocationDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var ifExist = await _unitOfWork.LeaveAllocationRepository.Exists(request.UpdateLeaveAllocationDto.Id);
            if (ifExist)
            {
                var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.Get(request.UpdateLeaveAllocationDto.Id);
                _mapper.Map(request.UpdateLeaveAllocationDto, leaveAllocation);
                await _unitOfWork.LeaveAllocationRepository.Update(leaveAllocation);
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}