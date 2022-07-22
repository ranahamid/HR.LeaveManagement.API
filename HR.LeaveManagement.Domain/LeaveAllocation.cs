using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
        public int NumberOfDays { get; set; }=0;
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; } = 0;
        public int Period { get; set; } = 0;
        public string? EmployeeId { get; set; }
    }
}