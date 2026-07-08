using System;
using System.Collections.Generic;
using App.Core.Application.ViewModels.Guardian;

namespace App.Core.Application.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{Name} {LastName}";
        public Guid GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public List<GuardianViewModel> Guardians { get; set; } = new();
    }
}
