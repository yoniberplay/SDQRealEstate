using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.Dtos.TipoPropiedades;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.CreateTipoPropiedad;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.DeleteTipoPropiedadById;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.UpdateTipoPropiedad;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Queries.GetListTipoPropiedad;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Queries.GetTipoPropiedadById;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SDQRealEstate.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [SwaggerTag("Mantemiento de Tipo de Propiedades")]
    public class MantenimientoTipoPropiedaController : BaseApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Crear un Tipo de Propiedad",
            Description = "Crear un Tipo de Propiedade con los datos requeridos"
            )]
        public async Task<IActionResult> Post([FromQuery] CreateTipoPropiedadCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type =  typeof(TipoPropiedadUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualizar Tipo de Propiedad",
            Description = "Actualiza un Tipo de Propiedad con los datos requeridos."
            )]
        public async Task<IActionResult> Put([FromQuery] UpdateTipoPropiedadCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPropiedadesReponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar Tipo de Propiedades",
            Description = "Lista todos los Tipo de Propiedades."
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetListTipoPropiedadQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPropiedadesReponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener Tipo de Propiedad",
            Description = "Obtiene el Tipo de Propiedad con el Id que ha introducido."
            )]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var temp = await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id });
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Eliminar Tipo de Propiedad",
            Description = "Eliman el Tipo de Propiedad con el Id que ha introducido."
            )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTipoPropiedadByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
