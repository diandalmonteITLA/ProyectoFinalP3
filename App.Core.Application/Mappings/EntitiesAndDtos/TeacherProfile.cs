using App.Core.Application.DTOs.Teacher;
using App.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Application.DTOs;

namespace App.Core.Application.Mappings.EntitiesAndDtos
{
    public class TeacherProfile: Profile
    {
        public TeacherProfile()
        {
            CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();

            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();

            CreateMap<CreateTeacherDto, Teacher>().ReverseMap();
        }
    }
}
