using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOs.Students
{
    public class GuardianSummaryDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
