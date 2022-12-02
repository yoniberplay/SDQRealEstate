using SDQRealEstate.Middlewares;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using WebApp.SDQRealEstate.Controllers;

namespace WebApp.SDQRealEstate.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public LoginAuthorize(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {

            var user = _userSession.HasUser();
            if (user != null)
            {
                var controller = (UserController)context.Controller;
                if (user.Roles.Any(n => n == "Admin"))
                {
                    context.Result = controller.RedirectToAction("index", "Admin");
                }else if (user.Roles.Any(n => n == "Cliente"))
                {
                    context.Result = controller.RedirectToAction("index", "Client");
                }
                else
                {
                    context.Result = controller.RedirectToAction("index", "Agent");
                }

            }
            else
            {
                await next();
            }
        }
    }
}
