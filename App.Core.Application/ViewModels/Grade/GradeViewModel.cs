using System;

namespace App.Core.Application.ViewModels.Grade
{
    public class GradeViewModel
    {
        public Guid Id { get; set; }
        public  required string Name { get; set; }
        public Guid TeacherId { get; set; } = Guid.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public int StudentCount { get; set; } = 0;
    }
}
