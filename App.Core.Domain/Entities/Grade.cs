using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    public class Grade
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher? TeacherInCharge { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
