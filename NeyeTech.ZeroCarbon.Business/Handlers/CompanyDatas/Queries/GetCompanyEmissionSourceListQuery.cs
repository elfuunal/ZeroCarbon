using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;
using MediatR;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Queries
{
    public class GetCompanyEmissionSourceListQuery : IRequest<ResponseMessage<List<EmissionSourceDto>>>
    {
        public long CompanyAddressId { get; set; }
        public class GetCompanyEmissionSourceListQueryHandler : IRequestHandler<GetCompanyEmissionSourceListQuery, ResponseMessage<List<EmissionSourceDto>>>
        {
            #region Properties
            private readonly ICompanyDataRepository _companyDataRepository;
            private readonly IMapper _mapper;
            #endregion

            #region Constructor
            public GetCompanyEmissionSourceListQueryHandler(
                ICompanyDataRepository companyDataRepository,
                IMapper mapper)
            {
                _companyDataRepository = companyDataRepository;
                _mapper = mapper;
            }
            #endregion


            public async Task<ResponseMessage<List<EmissionSourceDto>>> Handle(GetCompanyEmissionSourceListQuery request, CancellationToken cancellationToken)
            {
                List<EmissionSourceDto> result = new List<EmissionSourceDto>();

                var companyDatas = await _companyDataRepository.GetCompanyEmissionSourcesAsync(request.CompanyAddressId);
                foreach (CompanyData data in companyDatas)
                {
                    if (result.All(s => s.Id != data.EmissionSourceId))
                    {
                        EmissionSourceDto item = new EmissionSourceDto()
                        {
                            Label = data.EmissionSource.Name,
                            Id = data.EmissionSourceId
                        };

                        result.Add(item);
                    }
                }

                return ResponseMessage<List<EmissionSourceDto>>.Success(result);
            }
        }
    }
}
