using AutoMapper;
using App.Core.Application.DTOs.Teacher;
using App.Core.Application.ViewModels.Teacher;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _teacherService.GetAllAsync(includeInactive: false);
            var TeacherViewModel = _mapper.Map<List<TeacherViewModel>>(dtos);
            return View(TeacherViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveTeacherViewModel saveTeacher)
        {
            if (!ModelState.IsValid)
            {
                return View(saveTeacher);
            }

            try
            {
                var dto = _mapper.Map<CreateTeacherDto>(saveTeacher);
                await _teacherService.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(saveTeacher);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateTeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                LastName = teacher.LastName,
                Email = teacher.Email
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateTeacherDto updateTeacherDto)
        {
            if (id != updateTeacherDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updateTeacherDto);
            }

            try
            {
                await _teacherService.UpdateAsync(updateTeacherDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(updateTeacherDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _teacherService.DeactivateAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var teacher = await _teacherService.GetByIdAsync(id);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(teacher);
            }
        }
    }
}