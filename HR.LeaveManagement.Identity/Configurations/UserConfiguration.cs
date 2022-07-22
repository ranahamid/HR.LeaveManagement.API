using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Identity.Models;
 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public  class UserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData
            ( 
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                  Email = "admin@localhost.com",
                  NormalizedEmail = "admin@localhost.com",
                    Firstname = "System",
                  Lastname = "Admin",
                  UserName = "admin@localhost.com",
                  NormalizedUserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null,"P1ngP0ng$"),
                  EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    Email = "user@localhost.com",
                    NormalizedEmail = "user@localhost.com",
                    Firstname = "System",
                    Lastname = "User",
                    UserName = "user@localhost.com",
                    NormalizedUserName = "user@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P1ngP0ng$"),
                    EmailConfirmed = true,
                }
             );
        }
    }
}
