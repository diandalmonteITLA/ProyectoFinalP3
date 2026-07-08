using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.ViewModels.Student
{
    public class SaveStudentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido del estudiante es obligatorio.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un grado escolar.")]
        public Guid GradeId { get; set; }

        // Selected existing guardian to associate
        public Guid? SelectedExistingGuardianId { get; set; }
    }
}
