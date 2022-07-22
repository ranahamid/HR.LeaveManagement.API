using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Models;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVm registration);
        Task Logout();
    }
}
