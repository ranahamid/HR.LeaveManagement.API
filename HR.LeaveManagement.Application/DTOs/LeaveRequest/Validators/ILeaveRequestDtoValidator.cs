using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(x => x.LeaveTypeId)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull()
                    .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.")
                    .MustAsync(async (id, token) =>
                    {
                        var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                        return leaveTypeExists;
                    }).WithMessage("{PropertyName} does not exist."); 

            RuleFor(x => x.StartDate)
                  .NotEmpty().WithMessage("Date is required!")
                .Must(BeAValidDate).WithMessage("Invalid date.")
                 .LessThan(x => x.EndDate).WithMessage("{PropertyName} must be greater than {ComparisonValue}."); ;

            RuleFor(x => x.EndDate)
                  .NotEmpty().WithMessage("Date is required!")
                .Must(BeAValidDate).WithMessage("Invalid date.")
                .GreaterThan(x => x.StartDate).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
