using AutoMapper;
using MediatR;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSourceScopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeyeTech.ZeroCarbon.Business.Handlers.EmissionSourceScopes.Queries
{
    public class GetEmissionSourceScopesQuery : IRequest<ResponseMessage<List<EmissionSourceScopeDto>>>
    {
        public class GetEmissionSourceScopesQueryHandler : IRequestHandler<GetEmissionSourceScopesQuery, ResponseMessage<List<EmissionSourceScopeDto>>>
        {
            private readonly IEmissionSourceScopeRepository _emissionSourceScopeRepository;
            private readonly IMapper _mapper;

            public GetEmissionSourceScopesQueryHandler(IEmissionSourceScopeRepository emissionSourceScopeRepository, IMapper mapper)
            {
                _emissionSourceScopeRepository = emissionSourceScopeRepository;
                _mapper = mapper;
            }
            public async Task<ResponseMessage<List<EmissionSourceScopeDto>>> Handle(GetEmissionSourceScopesQuery request, CancellationToken cancellationToken)
            {
                var result = await _emissionSourceScopeRepository.GetListAsync(s => s.Status);
                return ResponseMessage<List<EmissionSourceScopeDto>>.Success(_mapper.Map<List<EmissionSourceScopeDto>>(result));
            }
        }
    }
}
