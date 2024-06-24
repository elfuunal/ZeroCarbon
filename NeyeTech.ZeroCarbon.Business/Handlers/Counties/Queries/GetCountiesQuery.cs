using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.Counties;
using MediatR;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Counties.Queries
{
    public class GetCountiesQuery : IRequest<ResponseMessage<List<CountyDto>>>
    {
        public long CityId { get; set; }
        public class GetCountiesQueryHandler : IRequestHandler<GetCountiesQuery, ResponseMessage<List<CountyDto>>>
        {
            private readonly ICountyRepository _countyRepository;
            readonly IMapper _mapper;

            public GetCountiesQueryHandler(ICountyRepository countyRepository, IMapper mapper)
            {
                _countyRepository = countyRepository;
                _mapper = mapper;
            }


            public async Task<ResponseMessage<List<CountyDto>>> Handle(GetCountiesQuery request, CancellationToken cancellationToken)
            {
                var cities = await _countyRepository.GetListAsync(s => s.CityId == request.CityId);
                var result = _mapper.Map<List<CountyDto>>(cities);
                return ResponseMessage<List<CountyDto>>.Success(result);
            }
        }
    }
}
