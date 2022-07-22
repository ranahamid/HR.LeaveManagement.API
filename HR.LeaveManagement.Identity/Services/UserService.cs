using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager; 
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                var employees = await _userManager.GetUsersInRoleAsync("Employee");
                return employees.Select(q => new Employee
                {
                    Id = q.Id,
                    Email = q.Email,
                    Firstname = q.Firstname,
                    Lastname = q.Lastname
                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<Employee>();
            }
      
        }
        public async Task<Employee> GetEmployee(string userId)
        {
            try
            {
                var employee = await _userManager.FindByIdAsync(userId);
                return new Employee
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname
                }
                ;
            }
            catch (Exception ex)
            {
                return new Employee();
            }

        }
    }
}