using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOs.Grades
{
    public class GradeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }

        public int StudentCount { get; set; }
    }
}
