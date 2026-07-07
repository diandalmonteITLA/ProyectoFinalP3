using AutoMapper;
using App.Core.Application.DTOs.Guardians;
using App.Core.Domain.Entities;

namespace App.Core.Application.Mappings
{
    public class GuardianMappingProfile : Profile
    {
        public GuardianMappingProfile()
        {
            // Entity to DTO
            CreateMap<Guardian, GuardianDto>();

            // DTO to Entity (Create)
            CreateMap<CreateGuardianDto, Guardian>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers.Select(pn => 
                    new PhoneNumber { Number = pn.Number, Type = pn.Type }).ToList()));

            // DTO to Entity (Update)
            CreateMap<UpdateGuardianDto, Guardian>()
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers.Select(pn => 
                    new PhoneNumber { Number = pn.Number, Type = pn.Type }).ToList()));
        }
    }
}
