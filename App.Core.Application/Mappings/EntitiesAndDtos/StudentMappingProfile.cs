using AutoMapper;
using App.Core.Application.DTOs.Students;
using App.Core.Domain.Entities;
using App.Core.Application.DTOs;

namespace App.Core.Application.Mappings.EntitiesAndDtos
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            // Entity to DTO
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade != null ? src.Grade.Name : null));

            // DTO to Entity (Create)
            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            // DTO to Entity (Update)
            CreateMap<UpdateStudentDto, Student>().ReverseMap();

            // PhoneNumber mapping
            CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();
        }
    }
}
