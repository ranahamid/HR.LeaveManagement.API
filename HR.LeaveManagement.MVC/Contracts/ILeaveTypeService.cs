using System.Collections.Generic;
using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVm>> GetLeaveTypes();
        Task<LeaveTypeVm> GetLeaveTypeDetails(int id );
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVm leaveType);
        Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVm leaveType);
        Task<Response<int>> DeleteLeaveType(int id);
    }
}
