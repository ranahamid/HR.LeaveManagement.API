 using System;
 using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public LeaveTypeService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<List<LeaveTypeVm>> GetLeaveTypes()
        {
            AddBearerToken();
            var leaveTypes = await _client.LeaveTypesAllAsync();
            var data = _mapper.Map<List<LeaveTypeVm>>(leaveTypes);
            return data;
        }

        public async Task<LeaveTypeVm> GetLeaveTypeDetails(int id)
        {
            AddBearerToken();
            var leaveType = await _client.LeaveTypesGETAsync(id);
            var data = _mapper.Map<LeaveTypeVm>(leaveType);
            return data;
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVm leaveType)
        {
            try
            {
                var response = new Response<int>();
                var data = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerToken();
                var apiResponse= await _client.LeaveTypesPOSTAsync(data);
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
    
        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                AddBearerToken();
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<int>
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVm leaveType)
        {
            try
            {
                var data = _mapper.Map<LeaveTypeDto>(leaveType);
                AddBearerToken();
                await _client.LeaveTypesPUTAsync(data);
                return new Response<int>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
