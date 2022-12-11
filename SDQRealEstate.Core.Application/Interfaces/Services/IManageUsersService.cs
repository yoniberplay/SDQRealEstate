using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.ViewModels.User;

namespace SDQRealEstate.Core.Application.Interfaces.Services
{
    public interface IManageUsersService
    {

        Task<List<UserViewModel>> GetbyRolList(String rol);
        Task<int> ChangeStatusUser(String id);
    }
}