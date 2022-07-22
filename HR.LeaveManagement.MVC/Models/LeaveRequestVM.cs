using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveRequestVm: CreateLeaveRequestVm
    {
        public int Id { get; set; }
        
       
       
        public DateTime DateRequested { get; set; }
      
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        //public string RequestingEmployeeId { get; set; }
        public LeaveTypeVm LeaveType { get; set; }
        public EmployeeVm Employee { get; set; }
    }
    public class CreateLeaveRequestVm
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SelectList LeaveTypes { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
    }

    //public class UpdateLeaveRequestVM
    //{
    //    public int Id { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public int LeaveTypeId { get; set; }
    //    public string RequestComments { get; set; }
    //    public bool Cancelled { get; set; }
    //}

    //public class ChangeLeaveRequestApprovalVM
    //{
    //    public int Id { get; set; }
    //    public bool? Approved { get; set; }
    //}
    public class AdminLeaveRequestViewVm
    {
        public int TotalRequests { get; set; }
        public int ApprovedRequests { get; set; }
        public int PendingRequests { get; set; }
        public int RejectedRequests { get; set; }
        public List<LeaveRequestVm> LeaveRequests { get; set; }
    }

    public class EmployeeLeaveRequestViewVm
    {
        public List<LeaveAllocationVm> LeaveAllocations { get; set; }
        public List<LeaveRequestVm> LeaveRequests { get; set; }
    }

}
