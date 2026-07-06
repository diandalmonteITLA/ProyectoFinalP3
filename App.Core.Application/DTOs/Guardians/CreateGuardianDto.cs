using App.Core.Application.DTOs.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.DTOs.Guardians
{
    public class CreateGuardianDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Al menos un número de teléfono es obligatorio.")]
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
    }
}
