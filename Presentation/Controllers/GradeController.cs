using App.Core.Application.DTOs.Grades;
using App.Core.Application.Interfaces;
using App.Core.Application.ViewModels.Grade;
using App.Core.Application.ViewModels.Teacher;
using App.Core.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public GradeController(IGradeService gradeService, IMapper mapper, ITeacherService teacherService)
        {
            _gradeService = gradeService;
            _mapper = mapper;
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var grades = await _gradeService.GetAllAsync();
            var mappedGrades = _mapper.Map<List<GradeViewModel>>(grades);
            return View(mappedGrades);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var gradeDto = await _gradeService.GetByIdAsync(id);

            if (gradeDto == null)
                return NotFound($"No se encontró el curso con Id {id}.");

            var gradeVm = _mapper.Map<GradeViewModel>(gradeDto);

            return View(gradeVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var gradeDto = await _gradeService.GetByIdAsync(id);

            if (gradeDto == null)
                return NotFound($"No se encontró el curso con Id {id}.");

            var gradeVm = _mapper.Map<GradeViewModel>(gradeDto);
            EditGradeViewModel editGradeVm = new EditGradeViewModel() { Grade = gradeVm};
            var teacherList = await _teacherService.GetAllAsync();
            editGradeVm.ActiveTeachers = _mapper.Map<List<TeacherViewModel>>(teacherList);

            return View(editGradeVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind(Prefix = "Grade")] GradeViewModel gradeViewModel)
        {
            ModelState.Remove("Grade.Name");
            ModelState.Remove("Name");
            if (!ModelState.IsValid)
            {
                EditGradeViewModel editGradeVm = new EditGradeViewModel() { Grade = gradeViewModel };
                var teacherList = await _teacherService.GetAllAsync();
                editGradeVm.ActiveTeachers = _mapper.Map<List<TeacherViewModel>>(teacherList);
                return View(editGradeVm);
            }

            try
            {
                var gradeDto = _mapper.Map<UpdateGradeDto>(gradeViewModel);
                await _gradeService.UpdateAsync(id, gradeDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                EditGradeViewModel editGradeVm = new EditGradeViewModel() { Grade = gradeViewModel };
                var teacherList = await _teacherService.GetAllAsync();
                editGradeVm.ActiveTeachers = _mapper.Map<List<TeacherViewModel>>(teacherList);
                return View(editGradeVm);
            }
        }
    }
}