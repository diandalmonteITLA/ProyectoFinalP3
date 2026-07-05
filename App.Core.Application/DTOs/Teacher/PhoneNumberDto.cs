using App.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOs.Teacher
{
    public class PhoneNumberDto
    {
        [Required]
        [Phone]
        public string Number { get; set; } = string.Empty;

        [Required]
        public NumberType Type { get; set; }

    }
}
