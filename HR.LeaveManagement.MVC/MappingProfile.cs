using AutoMapper;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto,CreateLeaveTypeVm>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVm>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestVm>()
                .ForMember(q=>q.DateRequested, opt=> opt.MapFrom(x=>x.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();


            CreateMap<LeaveRequestListDto, LeaveRequestVm>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();


            CreateMap<LeaveTypeDto, LeaveTypeVm>().ReverseMap();

            CreateMap<LeaveAllocationDto, LeaveAllocationVm>().ReverseMap();


            //CreateMap<CreateLeaveAllocationDto, CreateLeaveAllocationVm>().ReverseMap();
            //CreateMap<UpdateLeaveAllocationDto, UpdateLeaveAllocationVm>().ReverseMap();
            
            CreateMap<RegistrationRequest, RegisterVm>().ReverseMap();
           // CreateMap<Employee, EmployeeVm>().ReverseMap();
        }
    }
}
