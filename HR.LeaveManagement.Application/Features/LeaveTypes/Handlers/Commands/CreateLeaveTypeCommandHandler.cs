using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence; 
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(
           IUnitOfWork unitOfWork, 
          //  ILeaveTypeRepository leaveTypeRepository, 
            IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            //_leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult =await validator.ValidateAsync(request.CreateLeaveTypeDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorCode + ":" + x.ErrorMessage).ToList();
                // throw new ValidationException(validationResult);
                return response;
            } 
            var leaveType = _mapper.Map<Domain.LeaveType>(request.CreateLeaveTypeDto);
            leaveType = await _unitOfWork.LeaveTypeRepository.Add(leaveType);
            await _unitOfWork.Save();
            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveType.Id;
            return response; 
        }
    }
}