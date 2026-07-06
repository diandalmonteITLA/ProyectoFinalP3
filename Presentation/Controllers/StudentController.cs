using App.Core.Application.DTOS.Students;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Coordinador")]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentDto createStudentDto)
        {
            await _studentService.AddAsync(createStudentDto);
            return CreatedAtAction(nameof(GetById), new { id = createStudentDto }, createStudentDto);
        }

        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> Update(Guid id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (id != updateStudentDto.Id)
            {
                return BadRequest();
            }

            await _studentService.UpdateAsync(updateStudentDto);
            return NoContent();
        }

        // PATCH: api/Student/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<StudentDto>> PartialUpdate(Guid id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (id != updateStudentDto.Id)
            {
                return BadRequest();
            }

            await _studentService.UpdateAsync(updateStudentDto);
            return NoContent();
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _studentService.DeactivateAsync(id);
            return NoContent();
        }
    }
}
