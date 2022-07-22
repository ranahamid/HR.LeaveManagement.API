using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator()
        {
            Include(new ILeaveTypeDtoValidator());
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("{PropertyName} must be present.");
        }
    }
}
