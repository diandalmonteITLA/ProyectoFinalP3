using App.Core.Application.DTOs;
using App.Core.Application.DTOs.Teacher;
using App.Core.Application.ViewModels.Teacher;
using App.Core.Domain.Entities;
using App.Core.Domain.Enums;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class TeacherDtoMappingProfile : Profile
    {
        public TeacherDtoMappingProfile()
        {
            CreateMap<TeacherDto, TeacherViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => 
                    src.PhoneNumbers != null && src.PhoneNumbers.Any() 
                    ? src.PhoneNumbers.First().Number 
                    : string.Empty));

            CreateMap<TeacherViewModel, TeacherDto>()
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

            CreateMap<CreateTeacherDto, Teacher>();
        }
    }
}
