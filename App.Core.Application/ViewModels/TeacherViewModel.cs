using System;
using System.Collections.Generic;

namespace App.Core.Application.ViewModels
{
    public class TeacherViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{Name} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<string> GradeNames { get; set; } = new();
        public string Specialty { get; set; } = string.Empty;
    }
}
