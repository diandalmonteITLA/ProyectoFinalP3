using App.Core.Application.Interfaces;
using App.Core.Application.ViewModels;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Pages
{
    [Authorize]
    public class StudentsModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly IGenericRepository<Grade> _gradeRepository;
        private readonly IGuardianService _guardianService;

        public StudentsModel(
            IStudentService studentService, 
            IGenericRepository<Grade> gradeRepository,
            IGuardianService guardianService)
        {
            _studentService = studentService;
            _gradeRepository = gradeRepository;
            _guardianService = guardianService;
        }

        public List<StudentViewModel> Students { get; set; } = new();
        public List<GradeViewModel> Grades { get; set; } = new();
        public List<GuardianViewModel> AllGuardians { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? SelectedGradeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? openGuardiansStudentId { get; set; }

        [BindProperty]
        public SaveStudentViewModel NewStudent { get; set; } = new();

        [BindProperty]
        public SaveStudentViewModel EditingStudent { get; set; } = new();

        [BindProperty]
        public string Matricula { get; set; } = string.Empty;

        // Guardian fields for student creation/linking (optional or hidden fallback)
        [BindProperty]
        public string GuardianName { get; set; } = string.Empty;

        [BindProperty]
        public string GuardianLastName { get; set; } = string.Empty;

        [BindProperty]
        public string GuardianEmail { get; set; } = string.Empty;

        [BindProperty]
        public string GuardianPhone { get; set; } = string.Empty;

        [BindProperty]
        public Guid? SelectedExistingGuardianId { get; set; }

        // General Guardian Link / Delete fields (used from modal popup)
        [BindProperty]
        public Guid GuardianStudentId { get; set; }

        [BindProperty]
        public Guid DeletingGuardianId { get; set; }

        [TempData]
        public string? ActionError { get; set; }

        [TempData]
        public string? ActionSuccess { get; set; }

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var rawGrades = await _gradeRepository.GetAllAsync(includeInactive: false);
            Grades = rawGrades.Select(g => new GradeViewModel
            {
                Id = g.Id,
                Name = g.Name,
                TeacherId = g.TeacherId,
                TeacherName = g.TeacherInCharge != null ? $"{g.TeacherInCharge.Name} {g.TeacherInCharge.LastName}" : "Sin Asignar"
            }).OrderBy(g => g.Name).ToList();

            var rawGuardians = await _guardianService.GetAllAsync(includeInactive: false);
            AllGuardians = rawGuardians.Select(g => new GuardianViewModel
            {
                Id = g.Id,
                Name = g.Name,
                LastName = g.LastName,
                Email = g.Email,
                Phone = g.PhoneNumbers != null && g.PhoneNumbers.Any() ? g.PhoneNumbers.First().Number : ""
            }).OrderBy(g => g.Name).ToList();

            var allStudents = await _studentService.GetAllAsync(includeInactive: false);
            var filtered = allStudents.AsEnumerable();

            // Search Filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var term = SearchTerm.Trim().ToLower();
                filtered = filtered.Where(s => s.Name.ToLower().Contains(term) ||
                                              s.LastName.ToLower().Contains(term));
            }

            // Grade Filter
            if (SelectedGradeId.HasValue && SelectedGradeId.Value != Guid.Empty)
            {
                filtered = filtered.Where(s => s.GradeId == SelectedGradeId.Value);
            }

            Students = filtered.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName,
                GradeId = s.GradeId,
                GradeName = s.Grade?.Name ?? "Sin Asignar",
                Guardians = s.Guardian?.Select(g => new GuardianViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    LastName = g.LastName,
                    Email = g.Email,
                    Phone = g.PhoneNumbers != null && g.PhoneNumbers.Any() ? g.PhoneNumbers.First().Number : ""
                }).ToList() ?? new List<GuardianViewModel>()
            }).OrderBy(s => s.Name).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(NewStudent.Name) || string.IsNullOrWhiteSpace(NewStudent.LastName))
            {
                ActionError = "El nombre y apellido del estudiante son obligatorios.";
                await LoadDataAsync();
                return Page();
            }

            // Validate that we have a guardian
            bool hasExistingGuardian = SelectedExistingGuardianId.HasValue && SelectedExistingGuardianId.Value != Guid.Empty;

            if (!hasExistingGuardian)
            {
                ActionError = "Debe buscar y seleccionar un acudiente registrado.";
                await LoadDataAsync();
                return Page();
            }

            try
            {
                var studentEntity = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = NewStudent.Name,
                    LastName = NewStudent.LastName,
                    GradeId = NewStudent.GradeId,
                    IsActive = true,
                    Guardian = new List<Guardian>()
                };

                var existing = await _guardianService.GetByIdAsync(SelectedExistingGuardianId!.Value);
                if (existing != null)
                {
                    studentEntity.Guardian.Add(existing);
                }

                await _studentService.AddAsync(studentEntity);
                ActionSuccess = $"El estudiante {studentEntity.Name} {studentEntity.LastName} fue registrado correctamente.";
                return RedirectToPage(new { SearchTerm, SelectedGradeId });
            }
            catch (Exception ex)
            {
                ActionError = $"Error al registrar el estudiante: {ex.Message}";
                await LoadDataAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            ModelState.Clear();

            try
            {
                var existing = await _studentService.GetByIdAsync(EditingStudent.Id);
                if (existing == null)
                {
                    ActionError = "Estudiante no encontrado.";
                    await LoadDataAsync();
                    return Page();
                }

                existing.Name = EditingStudent.Name;
                existing.LastName = EditingStudent.LastName;
                existing.GradeId = EditingStudent.GradeId;

                await _studentService.UpdateAsync(existing);
                ActionSuccess = $"El estudiante {existing.Name} {existing.LastName} fue modificado correctamente.";
                return RedirectToPage(new { SearchTerm, SelectedGradeId });
            }
            catch (Exception ex)
            {
                ActionError = $"Error al modificar el estudiante: {ex.Message}";
                await LoadDataAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _studentService.DeactivateAsync(id);
                ActionSuccess = "El estudiante fue dado de baja correctamente.";
            }
            catch (Exception ex)
            {
                ActionError = $"Error al eliminar el estudiante: {ex.Message}";
            }

            return RedirectToPage(new { SearchTerm, SelectedGradeId });
        }

        // Link Guardian via Modal Popup
        public async Task<IActionResult> OnPostAddGuardianAsync()
        {
            ModelState.Clear();

            if (GuardianStudentId == Guid.Empty)
            {
                ActionError = "Estudiante no válido.";
                await LoadDataAsync();
                return Page();
            }

            try
            {
                var student = await _studentService.GetByIdAsync(GuardianStudentId);
                if (student == null)
                {
                    ActionError = "No se encontró el estudiante.";
                    await LoadDataAsync();
                    return Page();
                }

                if (student.Guardian == null)
                {
                    student.Guardian = new List<Guardian>();
                }

                if (SelectedExistingGuardianId.HasValue && SelectedExistingGuardianId.Value != Guid.Empty)
                {
                    var existing = await _guardianService.GetByIdAsync(SelectedExistingGuardianId.Value);
                    if (existing != null)
                    {
                        if (!student.Guardian.Any(g => g.Id == existing.Id))
                        {
                            student.Guardian.Add(existing);
                            await _studentService.UpdateAsync(student);
                        }
                    }
                }

                ActionSuccess = "El acudiente fue vinculado al estudiante correctamente.";
                return RedirectToPage(new { SearchTerm, SelectedGradeId, openGuardiansStudentId = GuardianStudentId });
            }
            catch (Exception ex)
            {
                ActionError = $"Error al vincular acudiente: {ex.Message}";
                await LoadDataAsync();
                return Page();
            }
        }

        // Delete/Unlink Guardian
        public async Task<IActionResult> OnPostDeleteGuardianAsync()
        {
            if (GuardianStudentId == Guid.Empty || DeletingGuardianId == Guid.Empty)
            {
                ActionError = "Parámetros de desvinculación incorrectos.";
                return RedirectToPage(new { SearchTerm, SelectedGradeId });
            }

            try
            {
                var student = await _studentService.GetByIdAsync(GuardianStudentId);
                if (student != null && student.Guardian != null)
                {
                    var target = student.Guardian.FirstOrDefault(g => g.Id == DeletingGuardianId);
                    if (target != null)
                    {
                        student.Guardian.Remove(target);
                        await _studentService.UpdateAsync(student);
                        ActionSuccess = "El acudiente fue desvinculado correctamente del estudiante.";
                    }
                }
                return RedirectToPage(new { SearchTerm, SelectedGradeId, openGuardiansStudentId = GuardianStudentId });
            }
            catch (Exception ex)
            {
                ActionError = $"Error al desvincular acudiente: {ex.Message}";
                return RedirectToPage(new { SearchTerm, SelectedGradeId, openGuardiansStudentId = GuardianStudentId });
            }
        }

        public static string GetStudentCode(Guid id)
        {
            var clean = id.ToString().Replace("-", "");
            return "ST-" + clean.Substring(clean.Length - 6).ToUpper();
        }
    }
}
