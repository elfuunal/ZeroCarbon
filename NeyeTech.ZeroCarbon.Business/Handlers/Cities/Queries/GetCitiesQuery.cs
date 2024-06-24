using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.Cities;
using MediatR;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Cities.Queries
{
    public class GetCitiesQuery : IRequest<ResponseMessage<List<CityDto>>>
    {
        public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, ResponseMessage<List<CityDto>>>
        {
            readonly ICityRepository _cityRepository;
            readonly IMapper _mapper;

            public GetCitiesQueryHandler(ICityRepository cityRepository, IMapper mapper)
            {
                _cityRepository = cityRepository;
                _mapper = mapper;
            }

            public async Task<ResponseMessage<List<CityDto>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
            {
                var cities = await _cityRepository.GetListAsync();

                var result = _mapper.Map<List<CityDto>>(cities);
                return ResponseMessage<List<CityDto>>.Success(result);
            }
        }
    }
}
