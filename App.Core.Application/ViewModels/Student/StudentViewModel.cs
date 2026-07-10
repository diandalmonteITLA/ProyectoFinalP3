using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Application.ViewModels.Guardian;

namespace App.Core.Application.ViewModels.Student
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        public required string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido del estudiante es obligatorio.")]
        public required string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Debe seleccionar un grado escolar.")]
        public required Guid GradeId { get; set; }
        [Required(ErrorMessage = "Debe haber al menos un acudiente.")]
        public required List<Guid> GuardiansId { get; set; } = new();
    }
}
