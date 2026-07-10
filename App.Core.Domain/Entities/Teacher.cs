using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public  required string Name { get; set; }
        public required string LastName { get; set; }
        public required ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Grade>? ManagedGrades { get; set; }


    }
}
