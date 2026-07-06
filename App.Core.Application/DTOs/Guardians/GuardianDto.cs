using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOS.Guardians
{
    public class GuardianDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public List<DTOS.Students.PhoneNumberDto> PhoneNumbers { get; set; } = new();
    }
}
