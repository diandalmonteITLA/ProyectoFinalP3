using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.ViewModels.Guardian
{
    public class SaveGuardianViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del acudiente es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido del acudiente es obligatorio.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar una dirección de correo válida.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número telefónico es obligatorio.")]
        public string Phone { get; set; } = string.Empty;
    }
}
