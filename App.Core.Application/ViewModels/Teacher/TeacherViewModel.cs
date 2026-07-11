using System;
using System.Collections.Generic;
using App.Core.Application.DTOs;

namespace App.Core.Application.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{Name} {LastName}";
        public string Email { get; set; } = string.Empty;
        public required List<PhoneNumberDto> PhoneNumbers { get; set; }
        public List<string> ManagedGradeNames { get; set; } = new();
    }
}
