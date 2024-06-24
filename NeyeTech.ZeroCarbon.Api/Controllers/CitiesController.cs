using NeyeTech.ZeroCarbon.Business.Handlers.Cities.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeyeTech.ZeroCarbon.Entities.DTOs.Cities;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class CitiesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CityDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CityDto>>))]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return CreateActionResult(await Mediator.Send(new GetCitiesQuery()));
        }
    }
}
