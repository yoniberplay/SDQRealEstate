using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.ViewModels.User;
using SDQRealEstate.Core.Application.Dtos.Account;

namespace SDQRealEstate.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthenticationResponse HasUser()
        {
            AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (userViewModel == null)
            {
                return null;
            }
            return userViewModel;
        }

    }
}
