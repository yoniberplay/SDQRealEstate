using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using SDQRealEstate.Core.Application.Dtos.Agentes;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetList;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetById;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetAgentProperty;
using SDQRealEstate.Core.Application.Features.Agentes.Commands.ChangeStatus;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SDQRealEstate.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [SwaggerTag("Mantemiento de Agentes")]
    public class AgenteController : BaseApiController
    {

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgenteResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista de agentes",
            Description = "Listado de todos usuarios con el rol de agente registrados en el sistema."
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetListAgentesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgenteResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener agente por su Id",
            Description = "Devuelve los datos del agente con el id solicitado."
            )]
        public async Task<IActionResult> Get(String id)
        {
            try
            {
                var temp = await Mediator.Send(new GetAgenteByIdQuery { Id = id });
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("GetAgentProperty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Propiedades de un agente",
            Description = "Devuelve la lista de propiedades correspondientes a un agente."
            )]
        public async Task<IActionResult> Get([FromQuery]GetAgentPropertyParameters filter)
        {

            try
            {
                return Ok(await Mediator.Send(new GetAgentPropertyQuery() { Id = filter.Id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeStatus")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Cambiar estatus de un agente",
            Description = "Cambia el estatus de un agente si esta activo se inactiva y si esta inactivo se activa"
            )]
        public async Task<IActionResult> Put(String Id)
        {
            try
            {
                await Mediator.Send(new ChangeStatusCommand() { Id = Id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
