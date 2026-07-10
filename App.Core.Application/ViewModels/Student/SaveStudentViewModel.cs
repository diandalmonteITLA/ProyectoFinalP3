using App.Core.Application.ViewModels.Guardian;
using App.Core.Application.ViewModels.Grade;

namespace App.Core.Application.ViewModels.Student
{
    public class SaveStudentViewModel
    {
        public StudentViewModel studentViewModel { get; set; }
        public List<GradeViewModel> gradeViewModels { get; set; } = new();
        public List<GuardianViewModel> guardianViewModels { get; set; } = new();
    }
}
