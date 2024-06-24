using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Commands;
using NeyeTech.ZeroCarbon.Business.Handlers.Companies.Commands;
using NeyeTech.ZeroCarbon.Business.Handlers.Companies.Queries;
using NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Queries;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;

namespace NeyeTech.ZeroCarbon.Api.Controllers
{
    public class CompaniesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createEmissionUnitCodeDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<AddCompanyDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<AddCompanyDto>))]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddCompanyDto createEmissionUnitCodeDto)
        {
            return CreateActionResult(await Mediator.Send(new CreateCompanyCommand() { Model = createEmissionUnitCodeDto }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<CompanyDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<CompanyDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return CreateActionResult(await Mediator.Send(new GetCompaniesQuery()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<DeleteCompanyDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<DeleteCompanyDto>))]
        [HttpDelete]
        public async Task<IActionResult> DeleteCompanyAsync(long id)
        {
            return CreateActionResult(await Mediator.Send(new DeleteCompanyCommand() { Id = id }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adres"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMessage<List<EmissionSourceDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseMessage<List<EmissionSourceDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetCompanyEmissionSourceListAsync(long adres)
        {
            return CreateActionResult(await Mediator.Send(new GetCompanyEmissionSourceListQuery() { CompanyAddressId = adres }));
        }
    }
}
