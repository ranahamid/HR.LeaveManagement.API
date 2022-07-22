using System;
using System.Collections.Generic;

namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveAllocationVm : CreateLeaveAllocationVm
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeVm LeaveType { get; set; }
       
        public int Period { get; set; }
        public string EmployeeId { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class CreateLeaveAllocationVm
    { 
        public int LeaveTypeId { get; set; } 
    }
    public class UpdateLeaveAllocationVm
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeVm LeaveType { get; set; }
    }

    public class ViewLeaveAllocationsVm
    {
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVm> LeaveAllocations {   get;  set;   }
    }
}
