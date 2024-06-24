using AutoMapper;
using MediatR;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeyeTech.ZeroCarbon.Business.Handlers.EmissionSources.Queries
{
    public class GetEmissionSourcesForSelectQuery : IRequest<ResponseMessage<List<EmissionSourceDto>>>
    {
        public long CategoryId { get; set; }
        public class GetEmissionSourcesForSelectQueryHandler : IRequestHandler<GetEmissionSourcesForSelectQuery, ResponseMessage<List<EmissionSourceDto>>>
        {
            private readonly IEmissionSourceRepository _emissionSourceRepository;
            private readonly IMapper _mapper;

            public GetEmissionSourcesForSelectQueryHandler(IEmissionSourceRepository emissionSourceRepository, IMapper mapper)
            {
                _emissionSourceRepository = emissionSourceRepository;
                _mapper = mapper;
            }
            public async Task<ResponseMessage<List<EmissionSourceDto>>> Handle(GetEmissionSourcesForSelectQuery request, CancellationToken cancellationToken)
            {
                List<EmissionSourceDto> result = new List<EmissionSourceDto>();

                var emissionSource = await _emissionSourceRepository.GetListAsync(s => s.EmissionSourceScopeId == request.CategoryId);
                if (emissionSource == null)
                    return ResponseMessage<List<EmissionSourceDto>>.NoDataFound("Emission Source bulunamadı.");

                result = _mapper.Map<List<EmissionSourceDto>>(emissionSource);
                return ResponseMessage<List<EmissionSourceDto>>.Success(result);
            }
        }
    }
}
