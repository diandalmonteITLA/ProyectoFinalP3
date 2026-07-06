using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required bool IsActive { get; set; }
        public Guid GradeId { get; set; }
        public Grade? Grade { get; set; }


        public ICollection<Guardian>? Guardian { get; set; }
    }
}
