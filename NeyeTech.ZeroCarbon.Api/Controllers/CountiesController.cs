using NeyeTech.ZeroCarbon.Business.Handlers.Counties.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Entities.DTOs.Counties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class CountiesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CountyDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CountyDto>>))]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetListAsync(long cityId)
        {
            return CreateActionResult(await Mediator.Send(new GetCountiesQuery() { CityId = cityId }));
        }
    }
}
