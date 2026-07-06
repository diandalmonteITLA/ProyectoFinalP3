using App.Core.Application.DTOs.Grades;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var grades = await _gradeService.GetAllAsync();
            return View(grades);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var grade = await _gradeService.GetByIdAsync(id);

            if (grade == null)
                return NotFound($"No se encontró el curso con Id {id}.");

            return View(grade);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var grade = await _gradeService.GetByIdAsync(id);

            if (grade == null)
                return NotFound();

            // Note: If your View uses UpdateGradeDto, you should map 'grade' to 'UpdateGradeDto' here before passing it to the view.
            return View(grade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateGradeDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto); 

            try
            {
                await _gradeService.UpdateAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }
    }
}