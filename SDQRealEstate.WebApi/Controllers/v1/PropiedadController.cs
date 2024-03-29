﻿using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetAllPropiedad;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetPropiedadtById;
using StockApp.Core.Application.Features.Products.Queries.GetPropiedadtByCode;
using SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetPropiedadtByCode;
using Swashbuckle.AspNetCore.Annotations;

namespace SDQRealEstate.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Desarrollador")]
    [SwaggerTag("Mantemiento de Propiedades")]
    public class PropiedadController : BaseApiController
    {

        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista todas las propiedades.",
            Description = "Devuelve un listado con todos las propiedades registradas en el sistema."
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllPropiedadQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener Propiedad",
            Description = "obtiene una propiedad con los datos del Id requeridos."
            )]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var temp = await Mediator.Send(new GetPropiedadtByIdQuery { Id = id });
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet("GetByCode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener Propiedad",
            Description = "obtiene una propiedad con los datos del codigo requeridos."
            )]
        public async Task<IActionResult> Get(GetPropiedadtByCodeParameters filters)
        {

            try
            {
                return Ok(await Mediator.Send(new GetPropiedadtByCodeQuery() { CodeId = filters.CodeId }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
