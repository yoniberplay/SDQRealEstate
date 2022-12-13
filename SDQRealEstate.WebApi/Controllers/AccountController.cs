using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.DTOs.Account;
using SDQRealEstate.Core.Application.Enums;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace SDQRealEstate.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Loggin y seguridad")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Loggin")]
        [Consumes(MediaTypeNames.Application.Json)] //COLOCAR EN LOS POST Y PUT
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary ="Loggin",
            Description ="Correo y contraseña para generar el token y acceder segun su rol."
            )]
        public async Task<IActionResult> Login([FromBody]AuthenticationRequest request)
        {
            var user = await _accountService.AuthenticateAsync(request);

            return Ok(user);
        }

        //[Authorize(Roles ="Admin")]
        [HttpPost("RegisterDeveloper")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Registrar desarrollador",
            Description = "Datos para crear una cuenta con el rol de desarrollador"
            )]
        public async Task<IActionResult> RegisterDeveloper([FromQuery] RegisterRequest request)
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Registrar Administrador",
            Description = "Datos para crear una cuenta con el rol de Administrador"
            )]
        public async Task<IActionResult> RegisterAdmin([FromBody]RegisterRequest request)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Confirmar correo",
            Description = "Solicita el userid y el token para validar el correo y asi mismo activar la cuenta del usuario"
            )]
        public async Task<IActionResult> confirmemail([FromQuery]String userID, [FromQuery] string token)
        {
                return Ok(await _accountService.ConfirmAccountAsync(userID,token));
        }

        [HttpPost("forgot-password")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Olvide contraseña",
            Description = "Para recuperar su contraseña o generar una nueva."
            )]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }


        [HttpPost("reset-password")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Reestablecer contraseña",
            Description = "Para generar una nueva contraseña."
            )]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }



    }
}
