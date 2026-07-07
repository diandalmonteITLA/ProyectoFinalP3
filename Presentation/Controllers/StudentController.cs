using App.Core.Application.DTOs.Students;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync(includeInactive: false);
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentDto createStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createStudentDto);
            }

            try
            {
                await _studentService.AddAsync(createStudentDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(createStudentDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateStudentDto
            {
                Id = student.Id,
                Name = student.Name,
                LastName = student.LastName,
                GradeId = student.GradeId,
                GuardianIds = student.Guardians.Select(g => g.Id).ToList()
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateStudentDto updateStudentDto)
        {
            if (id != updateStudentDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updateStudentDto);
            }

            try
            {
                await _studentService.UpdateAsync(updateStudentDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(updateStudentDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _studentService.DeactivateAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var student = await _studentService.GetByIdAsync(id);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(student);
            }
        }
    }
}
