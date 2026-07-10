using AutoMapper;
using App.Core.Application.DTOs.Teacher;
using App.Core.Application.ViewModels.Teacher;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class TeacherDtoMappingProfile : Profile
    {
        public TeacherDtoMappingProfile()
        {
            CreateMap<TeacherDto, TeacherViewModel>().ReverseMap();
        }
    }
}
