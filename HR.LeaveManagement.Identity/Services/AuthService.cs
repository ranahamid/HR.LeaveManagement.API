using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService:IAuthService
    {
        private readonly  UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user =await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"User with {request.Email} not found.");
            } 
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false,
                lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for {request.Email} aren't valid.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            AuthResponse response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };
            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"Username {request.UserName} already exists.");
            }
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception($"Email {request.Email} already exists.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegistrationResponse() {UserId = user.Id};
            }
            else
            {
                throw new Exception($"Something went wrong.");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("fullName", user.Firstname + " " + user.Lastname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
                //new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"])
            }.Union(userClaims).Union(roleClaims);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
           
            var expiration = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);
            var jwtSecurityToken = new JwtSecurityToken
            ( 
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            return jwtSecurityToken;
        }

    }
}
