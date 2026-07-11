using App.Core.Application.DTOs;
using App.Core.Application.DTOs.Guardians;
using App.Core.Application.ViewModels.Guardian;
using App.Core.Domain.Enums;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class GuardianDtoMappingProfile : Profile
    {
        public GuardianDtoMappingProfile()
        {
            CreateMap<GuardianDto, GuardianViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => 
                    src.PhoneNumbers != null && src.PhoneNumbers.Any() 
                    ? src.PhoneNumbers.First().Number 
                    : string.Empty));

            CreateMap<GuardianViewModel, GuardianDto>()
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => 
                    !string.IsNullOrWhiteSpace(src.Phone) 
                    ? new List<PhoneNumberDto> 
                      { 
                          new PhoneNumberDto 
                          { 
                              Number = src.Phone, 
                              Type = NumberType.Mobile 
                          } 
                      } 
                    : new List<PhoneNumberDto>()));
        }
    }
}
