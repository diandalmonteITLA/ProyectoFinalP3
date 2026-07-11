using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Domain.Enums;

namespace App.Core.Domain.Entities
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }
        public required string Number { get; set; }
        public NumberType Type { get; set; }
    }
}
