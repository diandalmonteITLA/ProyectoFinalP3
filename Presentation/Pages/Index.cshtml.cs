using App.Core.Application.Interfaces;
using App.Core.Application.ViewModels;
using App.Core.Application.DTOs.Teacher;
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
    public class IndexModel : PageModel
    {
        private readonly ITeacherService _teacherService;

        public IndexModel(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public List<TeacherViewModel> Teachers { get; set; } = new();
        public List<string> Specialties { get; set; } = new() { "Todas", "Matemáticas", "Ciencias", "Historia", "Lengua Española", "Inglés" };

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedSpecialty { get; set; }

        [BindProperty]
        public SaveTeacherViewModel NewTeacher { get; set; } = new();

        [BindProperty]
        public SaveTeacherViewModel EditingTeacher { get; set; } = new();

        [TempData]
        public string? ActionSuccess { get; set; }

        [TempData]
        public string? ActionError { get; set; }

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var rawTeachers = await _teacherService.GetAllAsync(includeInactive: false);
            var filtered = rawTeachers.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var term = SearchTerm.Trim().ToLower();
                filtered = filtered.Where(t => t.Name.ToLower().Contains(term) ||
                                              t.LastName.ToLower().Contains(term) ||
                                              t.Email.ToLower().Contains(term));
            }

            var mapped = filtered.Select((t, index) => new TeacherViewModel
            {
                Id = t.Id,
                Name = t.Name,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.PhoneNumbers != null && t.PhoneNumbers.Any() ? t.PhoneNumbers.First().Number : "",
                Specialty = Specialties[(index % (Specialties.Count - 1)) + 1] 
            });

            if (!string.IsNullOrWhiteSpace(SelectedSpecialty) && SelectedSpecialty != "Todas")
            {
                mapped = mapped.Where(t => t.Specialty == SelectedSpecialty);
            }

            Teachers = mapped.OrderBy(t => t.Name).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(NewTeacher.Name) || string.IsNullOrWhiteSpace(NewTeacher.LastName) || string.IsNullOrWhiteSpace(NewTeacher.Email))
            {
                ActionError = "El nombre, apellido y correo electrónico son obligatorios.";
                await LoadDataAsync();
                return Page();
            }

            try
            {
                var dto = new CreateTeacherDto
                {
                    Name = NewTeacher.Name,
                    LastName = NewTeacher.LastName,
                    Email = NewTeacher.Email,
                    Password = string.IsNullOrWhiteSpace(NewTeacher.Password) ? "Docente.2026" : NewTeacher.Password,
                    PhoneNumber = new App.Core.Application.DTOs.PhoneNumberDto
                    {
                        Number = string.IsNullOrWhiteSpace(NewTeacher.PhoneNumber.Number) ? "809-555-0100" : NewTeacher.PhoneNumber.Number,
                        Type = NewTeacher.PhoneNumber.Type
                    }
                };

                await _teacherService.AddAsync(dto);
                ActionSuccess = $"El docente {NewTeacher.Name} {NewTeacher.LastName} fue registrado correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ActionError = $"Error al registrar el docente: {ex.Message}";
                await LoadDataAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            ModelState.Clear();

            if (EditingTeacher.Id == Guid.Empty || string.IsNullOrWhiteSpace(EditingTeacher.Name) || string.IsNullOrWhiteSpace(EditingTeacher.LastName))
            {
                ActionError = "Datos de modificación incorrectos.";
                await LoadDataAsync();
                return Page();
            }

            try
            {
                var dto = new UpdateTeacherDto
                {
                    Id = EditingTeacher.Id,
                    Name = EditingTeacher.Name,
                    LastName = EditingTeacher.LastName,
                    Email = EditingTeacher.Email,
                    PhoneNumbers = new List<App.Core.Application.DTOs.PhoneNumberDto>
                    {
                        new()
                        {
                            Number = string.IsNullOrWhiteSpace(EditingTeacher.PhoneNumber.Number) ? "809-555-0100" : EditingTeacher.PhoneNumber.Number,
                            Type = EditingTeacher.PhoneNumber.Type
                        }
                    }
                };

                await _teacherService.UpdateAsync(dto);
                ActionSuccess = $"El docente {EditingTeacher.Name} {EditingTeacher.LastName} fue modificado correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ActionError = $"Error al modificar el docente: {ex.Message}";
                await LoadDataAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _teacherService.DeactivateAsync(id);
                ActionSuccess = "El docente fue dado de baja correctamente.";
            }
            catch (Exception ex)
            {
                ActionError = $"Error al desactivar el docente: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}
