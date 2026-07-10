using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Application.ViewModels.Teacher;

namespace App.Core.Application.ViewModels.Grade
{
    public class EditGradeViewModel
    {
        public required GradeViewModel Grade { get; set; }
        public List<TeacherViewModel> ActiveTeachers { get; set; }

    }
}
