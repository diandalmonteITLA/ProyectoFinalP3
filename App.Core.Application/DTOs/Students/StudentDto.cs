using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOS.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public PhoneNumberDto? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid GradeId { get; set; }
        public string? GradeName { get; set; }
        public List<GuardianSummaryDto> Guardians { get; set; } = new();
    }
}
