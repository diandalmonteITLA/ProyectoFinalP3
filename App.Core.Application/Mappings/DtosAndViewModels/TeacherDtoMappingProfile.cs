using System.Collections.Generic;
using System.Linq;
using App.Core.Application.DTOs;
using App.Core.Application.DTOs.Grades;
using App.Core.Application.DTOs.Teacher;
using App.Core.Application.ViewModels.Teacher;
using App.Core.Domain.Entities;
using App.Core.Domain.Enums;
using AutoMapper;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class TeacherDtoMappingProfile : Profile
    {
        public TeacherDtoMappingProfile()
        {
            CreateMap<TeacherDto, TeacherViewModel>().ReverseMap();

            CreateMap<TeacherDto, TeacherViewModel>().ReverseMap();

            CreateMap<SaveTeacherViewModel, CreateTeacherDto>().ReverseMap();
        }
    }
}
