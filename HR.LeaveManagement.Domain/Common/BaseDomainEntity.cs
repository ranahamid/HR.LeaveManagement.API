using System;

namespace HR.LeaveManagement.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public string? LastModifiedBy { get; set; }
    }
}