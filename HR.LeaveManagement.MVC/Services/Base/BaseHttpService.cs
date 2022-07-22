using System;
using System.Net.Http.Headers;
using HR.LeaveManagement.MVC.Contracts;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorageService;
        protected IClient _client;

        public BaseHttpService( IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>()
                    {Message = "Validation errors have occured.", ValidationErrors = ex.Response, Success = false};
            }
            else if(ex.StatusCode==404)
            {
                return new Response<Guid>()
                    { Message = "The request item could not be found.", Success = false };
            }
            else
            {
                return new Response<Guid>()
                    { Message = "Something went wrong, please try again.", Success = false };
            }
        }

        protected void AddBearerToken()
        {
            if (_localStorageService.Exist("token"))
            {
                var token = _localStorageService.GetStorageValue<string>("token");
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
