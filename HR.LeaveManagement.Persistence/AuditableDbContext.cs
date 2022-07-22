using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HR.LeaveManagement.Persistence
{
    public abstract class AuditableDbContext : DbContext
    {
        public AuditableDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual async Task<int> SaveChangesAsync(string userName = "SYSTEM" /*bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default*/)
        {
            var changeList = ChangeTracker.Entries<BaseDomainEntity>().Where(x => x.State == EntityState.Modified ||
             x.State == EntityState.Added).ToList();
            foreach (var entry in changeList)
            {
                entry.Entity.LastModifiedDate = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = userName;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userName;
                }
            }
            var result = await base.SaveChangesAsync(/*acceptAllChangesOnSuccess, cancellationToken*/);
            return result;
        }

    }
}
