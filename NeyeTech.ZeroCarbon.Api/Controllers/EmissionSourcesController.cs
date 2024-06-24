using Microsoft.AspNetCore.Mvc;
using NeyeTech.ZeroCarbon.Business.Handlers.EmissionSources.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class EmissionSourcesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<EmissionSourceDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<EmissionSourceDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetListAsync(long categoryId)
        {
            return CreateActionResult(await Mediator.Send(new GetEmissionSourcesForSelectQuery() { CategoryId = categoryId }));
        }

    }
}
