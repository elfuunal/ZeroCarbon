using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;

using MediatR;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;

namespace NeyeTech.ZeroCarbon.Business.Handlers.CompanyDatas.Queries
{
    public class GetCompanyDataListQuery : IRequest<ResponseMessage<CompanyDataListDto>>
    {
        public CompanyDataDto Model { get; set; }
        public class GetLoginCompanyDataListHandler : IRequestHandler<GetCompanyDataListQuery, ResponseMessage<CompanyDataListDto>>
        {
            private readonly ICompanyDataRepository _companyDataRepository;
            private readonly IEmissionSourceRepository _emissionSourceRepository;
            private readonly IMapper _mapper;
            private readonly ICompanyEmissionSourceRepository _companyEmissionSourceRepository;

            public GetLoginCompanyDataListHandler(
                ICompanyDataRepository companyDataRepository,
                IEmissionSourceRepository emissionSourceRepository,
                IMapper mapper,
                ICompanyEmissionSourceRepository companyEmissionSourceRepository)
            {
                _companyDataRepository = companyDataRepository;
                _emissionSourceRepository = emissionSourceRepository;
                _mapper = mapper;
                _companyEmissionSourceRepository = companyEmissionSourceRepository;
            }

            public async Task<ResponseMessage<CompanyDataListDto>> Handle(GetCompanyDataListQuery request, CancellationToken cancellationToken)
            {
                CompanyDataListDto result = new CompanyDataListDto();

                var companyEmissionList =
                    await _companyEmissionSourceRepository.GetCompanyEmissionSourcesWithCompanyId(request.Model.CompanyId);

                var companyDatas = await _companyDataRepository
                    .GetListAsync(s => s.CompanyId == request.Model.CompanyId);

                List<CompanyData> tempList = new List<CompanyData>();
                List<EmissionSource> tempListEmissionSource = new List<EmissionSource>();

                foreach (CompanyData source in companyDatas)
                {
                    if (companyEmissionList.Any(s => s.EmissionSourceId == source.EmissionSourceId))
                    {
                        tempList.Add(source);
                    }
                }

                foreach (var item in companyEmissionList)
                {
                    tempListEmissionSource.Add(item.EmissionSource);
                }

                result.CompanyDatas = _mapper.Map<List<CompanyDataDto>>(tempList);
                result.EmissionSourceDtos = _mapper.Map<List<EmissionSourceDto>>(tempListEmissionSource);
                result.CompanyId = request.Model.CompanyId;

                return ResponseMessage<CompanyDataListDto>.Success(result);
            }
        }
    }
}
