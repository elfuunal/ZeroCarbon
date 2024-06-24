using AutoMapper;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.Cities;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyAddresses;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources;
using NeyeTech.ZeroCarbon.Entities.DTOs.Counties;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSourceScopes;
using NeyeTech.ZeroCarbon.Entities.DTOs.User;

namespace NeyeTech.ZeroCarbon.Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, AddCompanyDto>().ReverseMap();
            CreateMap<CompanyEmissionSource, AddCompanyEmissionSourceDto>().ReverseMap();
            CreateMap<City, CityDto>()
                .ForMember(s=>s.Label, v=> v.MapFrom(u=> u.Name))
                .ForMember(s=>s.Value, v=> v.MapFrom(u=> u.Id.ToString()))
                .ReverseMap();

            CreateMap<County, CountyDto>()
                .ForMember(s => s.Label, v => v.MapFrom(u => u.Name))
                .ForMember(s => s.Value, v => v.MapFrom(u => u.Id.ToString()))
                .ReverseMap();

            CreateMap<CompanyEmissionSource, CompanyEmissionSourceDto>().ReverseMap();

            CreateMap<EmissionSource, EmissionSourceDto>()
                .ForMember(s => s.Label, v => v.MapFrom(u => u.Name))
                .ForMember(s => s.Value, v => v.MapFrom(u => u.Id.ToString()))
                .ReverseMap();

            CreateMap<EmissionSourceScope, EmissionSourceScopeDto>().ForMember(s => s.Label, v => v.MapFrom(u => u.Name))
                .ForMember(s => s.Value, v => v.MapFrom(u => u.Id.ToString()))
                .ReverseMap();
        }
    }
}
