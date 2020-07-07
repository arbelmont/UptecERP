using System.Threading.Tasks;
using Definitiva.Security.Identity.AccountViewModels;

namespace Definitiva.Security.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<object> Login(LoginViewModel model);
        Task<object> AddUser(RegisterViewModel model);
        Task<object> ChangePassword(ChangePasswordViewModel model);
        Task<object> Delete(DeleteViewModel model);
        Task<object> ChangeRoles(ChangeRolesViewModel model);
        Task<object> Setup(string apiKey);
    }
}