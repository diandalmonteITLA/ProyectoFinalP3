using System;

namespace App.Core.Application.ViewModels.Grade
{
    public class GradeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
    }
}
