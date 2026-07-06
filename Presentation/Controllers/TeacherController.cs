using App.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Coordinador")]
    public class TeacherController : ControllerBase
    {
        // GET: api/Teacher
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Lista de docentes");
        }

        // GET: api/Teacher/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok($"Docente con Id: {id}");
        }

        // POST: api/Teacher
        [HttpPost]
        public IActionResult Create([FromBody] Teacher teacher)
        {
            return Ok("Docente creado correctamente");
        }

        // PUT: api/Teacher/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Teacher teacher)
        {
            return Ok($"Docente {id} actualizado");
        }

        // PATCH: api/Teacher/{id}
        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id)
        {
            return Ok($"Docente {id} actualizado parcialmente");
        }
    }
}