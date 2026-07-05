using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.DTOs.Grades
{
    public class UpdateGradeDto
    {
        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe asignar un docente encargado.")]
        public Guid TeacherId { get; set; }
    }
}