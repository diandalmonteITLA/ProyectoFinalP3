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
            CreateMap<SaveStudentViewModel, CreateStudentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.studentViewModel.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.studentViewModel.LastName))
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.studentViewModel.GradeId))
                .ForMember(dest => dest.GuardianIds, opt => opt.MapFrom(src => src.studentViewModel.GuardiansId));

            CreateMap<CreateStudentDto, SaveStudentViewModel>()
                .ForMember(dest => dest.studentViewModel, opt => opt.MapFrom(src => new StudentViewModel
                {
                    Name = src.Name,
                    LastName = src.LastName,
                    GradeId = src.GradeId,
                    GuardiansId = src.GuardianIds ?? new List<Guid>()
                }));
        }
    }
}
