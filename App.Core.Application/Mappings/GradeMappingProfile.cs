using App.Core.Application.DTOs;
using App.Core.Domain.Entities;
using AutoMapper;

namespace App.Core.Application.Mappings
{
    public class GradeMappingProfile : Profile
    {
        public GradeMappingProfile()
        {
            CreateMap<Grade, GradeDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src =>
                    src.TeacherInCharge != null
                        ? $"{src.TeacherInCharge.Name} {src.TeacherInCharge.LastName}"
                        : null))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));

            CreateMap<Student, StudentSummaryDto>();

            
            CreateMap<UpdateGradeDto, Grade>();
        }
    }
}