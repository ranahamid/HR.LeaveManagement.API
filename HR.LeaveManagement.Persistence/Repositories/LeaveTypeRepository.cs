using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Domain;


namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly LeaveManagementDbContext _dbContext;
        public LeaveTypeRepository(LeaveManagementDbContext context) : base(context)
        {
            _dbContext = context;
        }

        //public Task<LeaveTypeDto> GetLeaveTypeWithDetails(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<LeaveTypeDto>> GetLeaveTypeWithDetails()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
