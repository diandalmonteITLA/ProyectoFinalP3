using App.Core.Application.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.ViewModels
{
    public class SaveTeacherViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; } = string.Empty;

        public PhoneNumberDto PhoneNumber { get; set; } = new() { Number = string.Empty };

        public string? Specialty { get; set; }
        public string? Department { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Status { get; set; }
    }
}
