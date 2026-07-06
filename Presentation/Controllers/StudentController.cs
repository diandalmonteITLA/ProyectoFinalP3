using App.Core.Application.DTOs.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var students = new List<StudentDto>();
            return View(students);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var student = new StudentDto { Name = "", LastName = "" };
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentDto createStudentDto)
        {
            if (!ModelState.IsValid)
                return View(createStudentDto); 

            var newStudent = new StudentDto { Name = createStudentDto.Name, LastName = createStudentDto.LastName };

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var studentToEdit = new UpdateStudentDto
            {
                Id = id,
                Name = "",
                LastName = ""
            };
            return View(studentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, UpdateStudentDto updateStudentDto)
        {
            if (!ModelState.IsValid)
                return View(updateStudentDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var student = new StudentDto { Name = "", LastName = "" };
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {

            return RedirectToAction(nameof(Index));
        }
    }
}
