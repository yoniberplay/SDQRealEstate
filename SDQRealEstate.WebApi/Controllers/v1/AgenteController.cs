using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Application.Dtos.Agentes;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetList;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetById;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetAgentProperty;
using SDQRealEstate.Core.Application.Features.Agentes.Commands.ChangeStatus;

namespace SDQRealEstate.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    public class AgenteController : BaseApiController
    {

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgenteResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        public async Task<IActionResult> Get(GetAgentPropertyParameters filter)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
