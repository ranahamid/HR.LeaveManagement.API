using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    { 
        private readonly IAuthService _authenticationService;
        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var response =await _authenticationService.Register(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Login(  AuthRequest request)
        {
           var response= await _authenticationService.Login(request);
           return Ok(response);
        } 
        //[HttpPost]
        //[Route("refreshtoken")]
        //public async Task<IActionResult> RefreshToken([FromBody] AuthResponse request)
        //{
        //    var tokenRequest = await _authenticationService.VerifyRefreshToken(request);
        //    if (tokenRequest is null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(tokenRequest);
        //}
    }
}
