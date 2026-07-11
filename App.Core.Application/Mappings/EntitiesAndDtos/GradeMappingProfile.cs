using App.Core.Application.DTOs.Grades;
using App.Core.Domain.Entities;
using AutoMapper;

namespace App.Core.Application.Mappings.EntitiesAndDtos
{
    public class GradeMappingProfile : Profile
    {
        public GradeMappingProfile()
        {
            CreateMap<Grade, GradeDto>()
        .ForMember(dest => dest.StudentCount,
                   opt => opt.MapFrom(src => src.Students != null ? src.Students.Count : 0))

        .ForMember(dest => dest.TeacherName,
                   opt => opt.MapFrom(src => src.TeacherInCharge != null
                       ? $"{src.TeacherInCharge.Name} {src.TeacherInCharge.LastName}"
                       : "No Teacher Assigned"));

            CreateMap<UpdateGradeDto, Grade>();
        }
    }
}