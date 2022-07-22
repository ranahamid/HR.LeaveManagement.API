using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(
            IUnitOfWork unitOfWork, 
           // ILeaveTypeRepository leaveTypeRepository, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var ifExist = await _unitOfWork.LeaveTypeRepository.Exists(request.LeaveTypeDto.Id);
            if (ifExist)
            {
                var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.LeaveTypeDto.Id);
                if (leaveType is null)
                {
                    throw new NotFoundException(nameof(leaveType), request.LeaveTypeDto.Id);
                }
                _mapper.Map(request.LeaveTypeDto, leaveType);
                await _unitOfWork.LeaveTypeRepository.Update(leaveType);
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}