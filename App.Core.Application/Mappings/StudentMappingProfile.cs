using AutoMapper;
using App.Core.Application.DTOS.Students;
using App.Core.Domain.Entities;

namespace App.Core.Application.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            // Entity to DTO
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber != null ? 
                    new PhoneNumberDto { Number = src.PhoneNumber.Number, Type = src.PhoneNumber.Type } : null))
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade != null ? src.Grade.Name : null));

            // DTO to Entity (Create)
            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber != null ? 
                    new PhoneNumber { Number = src.PhoneNumber.Number, Type = src.PhoneNumber.Type } : null));

            // DTO to Entity (Update)
            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber != null ? 
                    new PhoneNumber { Number = src.PhoneNumber.Number, Type = src.PhoneNumber.Type } : null));

            // PhoneNumber mapping
            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<PhoneNumberDto, PhoneNumber>();
        }
    }
}
