using App.Core.Application.DTOs.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Coordinador")]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        // GET: api/Student
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetAll()
        {
            // TODO: Implement logic to retrieve all students
            return Ok(new List<StudentDto>());
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetById(Guid id)
        {
            // TODO: Implement logic to retrieve a student by ID
            return Ok(new StudentDto { Name = "", LastName = "" });
        }

        // POST: api/Student
        [HttpPost]
        public ActionResult<StudentDto> Create([FromBody] CreateStudentDto createStudentDto)
        {
            // TODO: Implement logic to create a new student
            var newStudent = new StudentDto { Name = createStudentDto.Name, LastName = createStudentDto.LastName };
            return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, newStudent);
        }

        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public ActionResult<StudentDto> Update(Guid id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            // TODO: Implement logic to update a student
            return Ok(new StudentDto { Name = updateStudentDto.Name, LastName = updateStudentDto.LastName });
        }

        // PATCH: api/Student/{id}
        [HttpPatch("{id}")]
        public ActionResult<StudentDto> PartialUpdate(Guid id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            // TODO: Implement logic to partially update a student
            return Ok(new StudentDto { Name = updateStudentDto.Name, LastName = updateStudentDto.LastName });
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            // TODO: Implement logic to delete a student
            return NoContent();
        }
    }
}
