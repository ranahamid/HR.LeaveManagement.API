using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using IAuthenticationService = HR.LeaveManagement.MVC.Contracts.IAuthenticationService;

namespace HR.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, IHttpContextAccessor contextAccessor, IMapper mapper): base(client, localStorage)
        {
            _httpContextAccessor = contextAccessor;
            _mapper= mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }
        public async  Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authRequest = new AuthRequest
                {
                    Email = email,
                    Password = password
                };
                var response = await _client.LoginAsync(authRequest);
                if (response.Token != string.Empty)
                {
                    var tokenContent = _tokenHandler.ReadJwtToken(response.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme)); 
                    if (_httpContextAccessor.HttpContext != null)
                    {
                        var login = _httpContextAccessor.HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme, user);
                        _localStorageService.SetStorageValue("token", response.Token);
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        } 
        public async  Task Logout()
        {
            _localStorageService.ClearStorage(new List<string>{"token"});
            if (_httpContextAccessor.HttpContext != null)
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        } 
        public async Task<bool> Register(RegisterVm registration)
        {
            RegistrationRequest model = _mapper.Map<RegistrationRequest>(registration);
            var response = await _client.RegisterAsync(model);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                await Authenticate(registration.Email, registration.Password);
                return true;
            }
            return false;
        }
        public List<Claim> ParseClaims(JwtSecurityToken token)
        {
            var claims = token.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, token.Subject));
            return claims;
        }
    }
}
