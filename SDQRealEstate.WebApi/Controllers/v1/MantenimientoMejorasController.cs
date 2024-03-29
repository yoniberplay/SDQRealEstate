﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.Dtos.Mejora;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.CreateTipoVenta;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.CreateMejora;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.DeleteMejoraById;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.UpdateMejora;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Queries.GetListMejora;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Queries.GetMejoraById;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SDQRealEstate.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantemiento de Mejoras")]
    public class MantenimientoMejorasController : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Crear una mejora",
            Description = "Agregar una nueva mejora"
            )]
        public async Task<IActionResult> Post([FromQuery] CreateMejoraCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualizar mejora",
            Description = "Actualiza una mejora con los datos requeridos."
            )]
        public async Task<IActionResult> Put([FromQuery] UpdateMejoraaCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar mejoras",
            Description = "Lista todas las mejoras."
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetListMejoraQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener mejora",
            Description = "Obtiene la mejora con el Id que ha introducido."
            )]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var temp = await Mediator.Send(new GetMejoraByIdQuery { Id = id });
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
            Summary = "Eliminar mejora",
            Description = "Eliman la mejora con el Id que ha introducido."
            )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteMejoraByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}