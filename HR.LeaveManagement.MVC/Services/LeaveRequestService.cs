using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveRequestService: BaseHttpService,ILeaveRequestService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;
        public LeaveRequestService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<AdminLeaveRequestViewVm> GetAdminLeaveRequestList()
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestsAllAsync(isLoggedInUser:false);
            var model = new AdminLeaveRequestViewVm
            {
                TotalRequests = leaveRequest.Count,
                ApprovedRequests = leaveRequest.Count(x=> x.Approved==true),
                PendingRequests  = leaveRequest.Count(x => x.Approved == null),
                RejectedRequests = leaveRequest.Count(x => x.Approved == false),
                LeaveRequests = _mapper.Map<List<LeaveRequestVm>>(leaveRequest),
            };
            return model;
        }

        public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVm leaveRequest)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto requestDto = _mapper.Map<CreateLeaveRequestDto>(leaveRequest);
                AddBearerToken();
                var apiResponse = await _client.LeaveRequestsPOSTAsync(requestDto);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveRequestVm> GetLeaveRequest(int id)
        {
            AddBearerToken();
            var leaveTypes = await _client.LeaveRequestsGETAsync(id);
            var data = _mapper.Map<LeaveRequestVm>(leaveTypes);
            return data;
        }

        public async Task DeleteLeaveRequest(int id)
        {
            AddBearerToken();
            await _client.LeaveRequestsDELETEAsync(id);
        }

        public async  Task ApproveLeaveRequest(int id, bool approved)
        {
            try
            {
                var request = new ChangeLeaveRequestApprovalDto()
                {
                    Approved = approved,
                    Id = id
                };
                AddBearerToken();
                await _client.ChangeapprovalAsync(id,request);
            }
            catch (Exception)
            {
                throw;
            }
        } 
        public async Task<EmployeeLeaveRequestViewVm> GetUserLeaveRequests()
        {
            AddBearerToken();
            var leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser:true);
            var leaveAllocations = await _client.LeaveAllocationsAllAsync(isLoggedInUser: true);
            var model = new EmployeeLeaveRequestViewVm
            {
                LeaveRequests = _mapper.Map<List<LeaveRequestVm>>(leaveRequests) ,
                LeaveAllocations = _mapper.Map <List<LeaveAllocationVm>>(leaveAllocations) 
            };
            return model;
        }
    }
}
