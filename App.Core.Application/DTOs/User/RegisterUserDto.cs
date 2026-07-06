using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.DTOs.User
{
    public class RegisterUserDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public required string Role { get; set; }
    }
}
