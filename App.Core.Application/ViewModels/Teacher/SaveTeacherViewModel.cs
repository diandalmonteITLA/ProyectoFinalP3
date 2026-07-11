using App.Core.Application.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.ViewModels.Teacher
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

        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
    }
}
