using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.DTOs.Account;
using SDQRealEstate.Core.Application.Enums;

namespace SDQRealEstate.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Loggin")]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            var user = await _accountService.AuthenticateAsync(request);

            return Ok(user);
        }

        //[Authorize(Roles ="Admin")]
        [HttpPost("RegisterDeveloper")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterDeveloper(RegisterRequest request)
        {
            if(request.Tipo != Roles.Desarrollador.ToString())
            {
                return BadRequest();
            }
            try
            {
                var origin = Request.Headers["origin"];

                var user = await _accountService.RegisterClientUserAsync(request, origin);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok(user);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [Authorize(Roles ="Admin")]
        [HttpPost("RegisterAdmin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
        {
            if (request.Tipo != Roles.Admin.ToString())
            {
                return BadRequest();
            }
            try
            {
                var origin = Request.Headers["origin"];

                var user = await _accountService.RegisterClientUserAsync(request, origin);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> confirmemail([FromQuery]String userID, [FromQuery] string token)
        {
                return Ok(await _accountService.ConfirmAccountAsync(userID,token));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }



    }
}
