using System.Collections.Generic;
using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVm> GetAdminLeaveRequestList();
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVm leaveRequest);
        Task<LeaveRequestVm> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);
        Task<EmployeeLeaveRequestViewVm> GetUserLeaveRequests();
    }
}
