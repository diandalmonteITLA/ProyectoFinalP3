using App.Core.Application.DTOs.Teacher;
using App.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Application.DTOs;

namespace App.Core.Application.Mappings
{
    public class TeacherProfile: Profile
    {
        public TeacherProfile()
        {
            CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();

            // Entity -> DTO
            CreateMap<Teacher, TeacherDto>();

            // DTO -> Entity
            CreateMap<CreateTeacherDto, Teacher>()
                .ForMember(dest => dest.HashedPassword,
                    opt => opt.MapFrom(src => src.Password));

            // DTO -> Entity
            CreateMap<UpdateTeacherDto, Teacher>()
           .ForMember(dest => dest.HashedPassword,
               opt => opt.Ignore());

            // Entity -> DTO
            CreateMap<Teacher, UpdateTeacherDto>();

            // DTO -> Entity
            CreateMap<TeacherDto, Teacher>();
        }
    }
}
