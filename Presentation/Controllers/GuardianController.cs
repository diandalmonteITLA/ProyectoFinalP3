using App.Core.Application.DTOs.Guardians;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class GuardianController : Controller
    {
        private readonly IGuardianService _guardianService;

        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var guardians = await _guardianService.GetAllAsync(includeInactive: false);
            return View(guardians);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var guardian = await _guardianService.GetByIdAsync(id);
            if (guardian == null)
            {
                return NotFound();
            }
            return View(guardian);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGuardianDto createGuardianDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createGuardianDto);
            }

            try
            {
                await _guardianService.AddAsync(createGuardianDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(createGuardianDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var guardian = await _guardianService.GetByIdAsync(id);
            if (guardian == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateGuardianDto
            {
                Id = guardian.Id,
                Name = guardian.Name,
                LastName = guardian.LastName,
                Email = guardian.Email,
                PhoneNumbers = guardian.PhoneNumbers
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateGuardianDto updateGuardianDto)
        {
            if (id != updateGuardianDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updateGuardianDto);
            }

            try
            {
                await _guardianService.UpdateAsync(updateGuardianDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(updateGuardianDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var guardian = await _guardianService.GetByIdAsync(id);
            if (guardian == null)
            {
                return NotFound();
            }
            return View(guardian);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _guardianService.DeactivateAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var guardian = await _guardianService.GetByIdAsync(id);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(guardian);
            }
        }
    }
}
