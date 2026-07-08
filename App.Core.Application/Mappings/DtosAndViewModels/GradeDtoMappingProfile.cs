using App.Core.Application.DTOs.Grades;
using App.Core.Application.DTOs.Teacher;
using App.Core.Application.ViewModels.Grade;
using App.Core.Application.ViewModels.Teacher;
using AutoMapper;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class GradeDtoMappingProfile : Profile
    {
        public GradeDtoMappingProfile()
        {
            CreateMap<GradeDto, GradeViewModel>().ReverseMap();
        }
    }
}
