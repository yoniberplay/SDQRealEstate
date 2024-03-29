﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.CreateTipoVenta;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.DeleteTipoVentaById;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.UpdateTipoVenta;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Queries.GetListTipoVenta;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Queries.GetTipoVentaById;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.CreateTipoPropiedad;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.UpdateTipoPropiedad;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SDQRealEstate.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantemiento de Tipo Ventas")]
    public class MantenimientoTipoVentasController : BaseApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Crear una Tipo Venta",
            Description = "Agregar un nuevo Tipo Venta"
            )]
        public async Task<IActionResult> Post([FromQuery] CreateTipoVentaCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualizar Tipo Venta",
            Description = "Actualiza un Tipo Venta con los datos requeridos."
            )]
        public async Task<IActionResult> Put([FromQuery] UpdateTipoVentaCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar Tipo de Venta",
            Description = "Lista todos los Tipo de Ventas."
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetListTipoVentaQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Desarrollador")]
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener Tipo de Venta",
            Description = "Obtiene el Tipo de Venta con el Id que ha introducido."
            )]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var temp = await Mediator.Send(new GetTipoVentaByIdQuery { Id = id });
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
            Summary = "Eliminar Tipo de Venta",
            Description = "Eliman el Tipo de Venta con el Id que ha introducido."
            )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTipoVentaByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
