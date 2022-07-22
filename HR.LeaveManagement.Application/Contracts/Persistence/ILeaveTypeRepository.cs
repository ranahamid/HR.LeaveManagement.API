using System.Collections.Generic;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        //Task<LeaveType> GetLeaveTypeWithDetails(int id);
        //Task<List<LeaveType>> GetLeaveTypesWithDetails();
    }
}