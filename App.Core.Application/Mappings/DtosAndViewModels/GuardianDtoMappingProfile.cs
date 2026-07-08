using App.Core.Application.DTOs.Guardians;
using App.Core.Application.ViewModels.Guardian;
using AutoMapper;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class GuardianDtoMappingProfile : Profile
    {
        public GuardianDtoMappingProfile()
        {
            CreateMap<GuardianDto, GuardianViewModel>().ReverseMap();
        }
    }
}
