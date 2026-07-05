using App.Core.Application.DTOS.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOs.Teacher
{
    public class TeacherDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public PhoneNumberDto PhoneNumber { get; set; } = new();

    }
}
