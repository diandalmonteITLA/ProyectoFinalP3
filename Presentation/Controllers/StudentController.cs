using App.Core.Application.DTOs.Students;
using App.Core.Application.Interfaces;
using App.Core.Application.ViewModels.Grade;
using App.Core.Application.ViewModels.Student;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Core.Application.Mappings.DtosAndViewModels;
using App.Core.Application.ViewModels.Guardian;
using App.Core.Application.DTOs.Grades;
using System.Collections.ObjectModel;
using App.Core.Application.DTOs.Guardians;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class StudentController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IGuardianService _guardianService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper, IGradeService gradeService, IGuardianService guardianService)
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _mapper = mapper;
            _guardianService = guardianService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _studentService.GetAllAsync(includeInactive: false);
            var StudentViewModel = _mapper.Map<List<ShowStudentViewModel>>(dtos);
            return View(StudentViewModel);
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
        public async Task<IActionResult> Create()
        {
            SaveStudentViewModel viewModel = new SaveStudentViewModel();
            await PopulateDropdownListsAsync(viewModel);
            ViewBag.editMode = false;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveStudentViewModel saveStudentVm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownListsAsync(saveStudentVm);
                return View(saveStudentVm);
            }

            try
            {
                var dto = _mapper.Map<CreateStudentDto>(saveStudentVm);
                await _studentService.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {   
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateDropdownListsAsync(saveStudentVm);
                return View(saveStudentVm);
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

        private async Task PopulateDropdownListsAsync(SaveStudentViewModel viewModel)
        {
            IReadOnlyCollection<GradeDto> grades = await _gradeService.GetAllAsync();
            viewModel.gradeViewModels = _mapper.Map<List<GradeViewModel>>(grades);

            IReadOnlyCollection<GuardianDto> guardians = await _guardianService.GetAllAsync(true);
            viewModel.guardianViewModels = _mapper.Map<List<GuardianViewModel>>(guardians);
        }
    }
}
