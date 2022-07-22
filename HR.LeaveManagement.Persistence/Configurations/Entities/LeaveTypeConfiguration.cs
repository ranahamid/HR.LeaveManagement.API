using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Configurations.Entities
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                    new LeaveType
                    {
                        Id = 1,
                        Name = "Vaccation",
                        DefaultDays = 10,
                        //CreatedBy = "",
                        //DateCreated =DateTime.UtcNow ,
                        //LastModifiedBy = "",
                        //LastModifiedDate = DateTime.UtcNow,
                    },
                    new LeaveType
                    {
                        Id = 2,
                        Name = "Sick",
                        DefaultDays = 12 ,
                        //CreatedBy = "",
                        //DateCreated = DateTime.UtcNow,
                        //LastModifiedBy = "",
                        //LastModifiedDate = DateTime.UtcNow,
                    }
                );
        }
    }
}
