using App.Core.Application.Interfaces;
using App.Core.Application.DTOs.Guardians;
using App.Core.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Application.ViewModels.Guardian;

namespace Presentation.Pages
{
    [Authorize]
    public class GuardiansModel : PageModel
    {
        private readonly IGuardianService _guardianService;

        public GuardiansModel(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        public List<GuardianViewModel> Guardians { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty]
        public SaveGuardianViewModel NewGuardian { get; set; } = new();

        [BindProperty]
        public SaveGuardianViewModel EditingGuardian { get; set; } = new();

        [BindProperty]
        public Guid DeletingGuardianId { get; set; }

        public string? ActionSuccess { get; set; }
        public string? ActionError { get; set; }

        public async Task OnGetAsync()
        {
            var list = await _guardianService.GetAllAsync(includeInactive: false);
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var query = SearchTerm.ToLower();
                list = list.Where(g => 
                    g.Name.ToLower().Contains(query) || 
                    g.LastName.ToLower().Contains(query) || 
                    (g.Email != null && g.Email.ToLower().Contains(query))
                ).ToList();
            }
            
            Guardians = list.Select(g => new GuardianViewModel
            {
                Id = g.Id,
                Name = g.Name,
                LastName = g.LastName,
                Email = g.Email,
                Phone = g.PhoneNumbers != null && g.PhoneNumbers.Any() ? g.PhoneNumbers.First().Number : ""
            }).OrderBy(g => g.Name).ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                ActionError = "Por favor, rellene todos los campos obligatorios con un formato válido.";
                await OnGetAsync();
                return Page();
            }

            var dto = new CreateGuardianDto
            {
                Name = NewGuardian.Name,
                LastName = NewGuardian.LastName,
                Email = NewGuardian.Email,
                PhoneNumbers = new List<PhoneNumberDto>
                {
                    new() { Number = NewGuardian.Phone, Type = App.Core.Domain.Enums.NumberType.Mobile }
                }
            };

            await _guardianService.AddAsync(dto);
            ActionSuccess = $"El acudiente {dto.Name} {dto.LastName} fue registrado correctamente.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                ActionError = "Por favor, rellene todos los campos requeridos con un formato válido.";
                await OnGetAsync();
                return Page();
            }

            var existing = await _guardianService.GetByIdAsync(EditingGuardian.Id);
            if (existing == null)
            {
                ActionError = "No se encontró el acudiente a modificar.";
                return RedirectToPage();
            }

            var dto = new UpdateGuardianDto
            {
                Id = EditingGuardian.Id,
                Name = EditingGuardian.Name,
                LastName = EditingGuardian.LastName,
                Email = EditingGuardian.Email,
                PhoneNumbers = new List<PhoneNumberDto>
                {
                    new() { Number = EditingGuardian.Phone, Type = App.Core.Domain.Enums.NumberType.Mobile }
                }
            };

            await _guardianService.UpdateAsync(dto);
            ActionSuccess = $"El acudiente {dto.Name} {dto.LastName} fue modificado correctamente.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (DeletingGuardianId != Guid.Empty)
            {
                await _guardianService.DeactivateAsync(DeletingGuardianId);
                ActionSuccess = "El acudiente fue eliminado/desactivado correctamente del sistema.";
            }
            return RedirectToPage();
        }
    }
}
