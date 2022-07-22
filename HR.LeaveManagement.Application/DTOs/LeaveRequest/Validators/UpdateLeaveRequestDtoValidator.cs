using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    internal class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidator(leaveTypeRepository));
            RuleFor(x=>x.Id).NotEmpty().NotNull().WithMessage("{PropertyName} must be present.");
        }

    }

}
