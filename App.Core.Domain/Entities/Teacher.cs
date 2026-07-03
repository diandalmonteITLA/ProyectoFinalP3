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
        public required PhoneNumber PhoneNumber { get; set; }
        public required string Email { get; set; } = string.Empty;

        public ICollection<Grade>? ManagedGrades { get; set; }


    }
}
