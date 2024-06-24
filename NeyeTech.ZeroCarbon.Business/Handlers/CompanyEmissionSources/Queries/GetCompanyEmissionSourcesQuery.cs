using AutoMapper;
using MediatR;
using NeyeTech.ZeroCarbon.Core.Extensions;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources;

namespace CarbonCalculator.Business.Handlers.CompanyEmissionSources.Queries
{
    public class GetCompanyEmissionSourcesQuery : IRequest<ResponseMessage<List<CompanyEmissionSourceDto>>>
    {
        public long CompanyId { get; set; }
        public class GetCompanyEmissionSourcesQueryHandler : IRequestHandler<GetCompanyEmissionSourcesQuery, ResponseMessage<List<CompanyEmissionSourceDto>>>
        {
            private readonly ICompanyEmissionSourceRepository _companyEmissionSourceRepository;
            private readonly IMapper _mapper;

            public GetCompanyEmissionSourcesQueryHandler(ICompanyEmissionSourceRepository companyEmissionSourceRepository, IMapper mapper)
            {
                _companyEmissionSourceRepository = companyEmissionSourceRepository;
                _mapper = mapper;
            }

            public async Task<ResponseMessage<List<CompanyEmissionSourceDto>>> Handle(GetCompanyEmissionSourcesQuery request, CancellationToken cancellationToken)
            {
                var list = await _companyEmissionSourceRepository.GetCompanyEmissionSources(request.CompanyId);
                var result = _mapper.Map<List<CompanyEmissionSourceDto>>(list);
                result = result.OrderBy(s => s.EmissionSource.GroupCode).ThenBy(s => s.EmissionSource.Label).ToList();
                return ResponseMessage<List<CompanyEmissionSourceDto>>.Success(result);
            }
        }
    }
}
