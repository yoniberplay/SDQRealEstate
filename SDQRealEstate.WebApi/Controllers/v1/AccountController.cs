using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDQRealEstate.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {

        //[Authorize(Roles = "Admin,SuperAdmin")]
        //[HttpPost("Create")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post(SaveTableViewModel vm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(vm);
        //        }

        //        vm.Status = (int)TableStatus.Available;

        //        var table = await _tableService.Add(vm);
        //        if (table == null)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, table);
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


    }
}
