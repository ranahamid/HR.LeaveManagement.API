using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Contracts.Infrastructure
{
    public  interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
