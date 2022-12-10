using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;

namespace SDQRealEstate.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Desarrollador")]
    public class PropiedadController : BaseApiController
    {

        //[HttpGet("List")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadViewModel))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Get([FromQuery] GetAllPropiedadParameter filters)
        //{
        //    try
        //    {
        //        return Ok(await Mediator.Send(new GetAllPropiedadParameter() { CategoryId = filters.CategoryId }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}



    }
}
