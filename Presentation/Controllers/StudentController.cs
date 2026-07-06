using App.Core.Application.DTOs.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Web.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class StudentController : Controller
    {
        // GET: Student
        [HttpGet]
        public IActionResult Index()
        {
            // TODO: Implement logic to retrieve all students
            var students = new List<StudentDto>();
            return View(students);
        }

        // GET: Student/Details/{id}
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            // TODO: Implement logic to retrieve a student by ID
            var student = new StudentDto { Name = "", LastName = "" };
            return View(student);
        }

        // GET: Student/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Displays the creation form
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentDto createStudentDto)
        {
            if (!ModelState.IsValid)
                return View(createStudentDto); // Return to form with validation errors

            // TODO: Implement logic to create a new student
            var newStudent = new StudentDto { Name = createStudentDto.Name, LastName = createStudentDto.LastName };

            return RedirectToAction(nameof(Index)); // Redirect to the list after success
        }

        // GET: Student/Edit/{id}
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // TODO: Implement logic to retrieve a student to populate the edit form
            var studentToEdit = new UpdateStudentDto();
            return View(studentToEdit);
        }

        // POST: Student/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Guid id, UpdateStudentDto updateStudentDto)
        {
            if (!ModelState.IsValid)
                return View(updateStudentDto);

            // TODO: Implement logic to update a student
            // (In MVC, this typically handles both the full and partial update logic)

            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/{id}
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            // TODO: Implement logic to retrieve a student to confirm deletion
            var student = new StudentDto { Name = "", LastName = "" };
            return View(student); // Shows a "Are you sure you want to delete this?" page
        }

        // POST: Student/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            // TODO: Implement logic to delete a student

            return RedirectToAction(nameof(Index));
        }
    }
}
