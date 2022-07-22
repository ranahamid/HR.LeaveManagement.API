namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveTypeVm: CreateLeaveTypeVm
    {
        public int Id { get; set; }
    }

    public class CreateLeaveTypeVm
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
