using System;
using System.ComponentModel.DataAnnotations;
using App.Core.Application.ViewModels.Grade;

namespace App.Core.Application.ViewModels.Student
{
    public class SaveStudentViewModel
    {
        public StudentViewModel studentViewModel { get; set; }
        public List<GradeViewModel> gradeViewModels { get; set; }
        public List<GuardianViewModel> guardianViewModels { get; set; }
    }
}
