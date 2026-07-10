using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Application.DTOs.Students;
using App.Core.Application.ViewModels;
using App.Core.Application.ViewModels.Student;
using AutoMapper;

namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class StudentDtoMappingProfile : Profile
    {
        public StudentDtoMappingProfile()
        {
            CreateMap<StudentDto, ShowStudentViewModel>().ReverseMap();
            CreateMap<StudentDto, StudentViewModel>().ReverseMap();
            CreateMap<CreateStudentDto, SaveStudentViewModel>().ReverseMap();
        }
    }
}
