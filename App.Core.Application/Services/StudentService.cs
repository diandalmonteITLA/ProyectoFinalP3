using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Application.Interfaces;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;

namespace App.Core.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IAuditService _auditService;

        public StudentService(
            IGenericRepository<Student> studentRepository,
            IAuditService auditService)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyCollection<Student>> GetAllAsync(bool includeInactive = false)
        {
            return await _studentRepository.GetAllAsync(includeInactive);
        }

        public async Task AddAsync(Student student)
        {
            if (student is null)
            {
                throw new ArgumentException("El estudiante no puede estar vacío.", nameof(student));
            }

            // Guardar el estudiante
            await _studentRepository.AddAsync(student);

            // Registrar auditoría
            await _auditService.RegisterAsync(
                "Student",
                "CREATE",
                $"Se creó el estudiante {student.Name} {student.LastName}");
        }

        public async Task UpdateAsync(Student student)
        {
            if (student is null)
            {
                throw new ArgumentException("El estudiante no puede estar vacío.", nameof(student));
            }

            var existingStudent = await _studentRepository.GetByIdAsync(student.Id);

            if (existingStudent is null)
            {
                throw new KeyNotFoundException("No se encontró el estudiante que se desea actualizar.");
            }

            // Actualizar el estudiante
            await _studentRepository.UpdateAsync(student);

            // Registrar auditoría
            await _auditService.RegisterAsync(
                "Student",
                "UPDATE",
                $"Se actualizó el estudiante {student.Name} {student.LastName}");
        }

        public async Task DeactivateAsync(Guid id)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);

            if (existingStudent is null)
            {
                throw new KeyNotFoundException("No se encontró el estudiante que se desea desactivar.");
            }

            // Desactivar el estudiante
            await _studentRepository.DeactiveAsync(id);

            // Registrar auditoría
            await _auditService.RegisterAsync(
                "Student",
                "DEACTIVATE",
                $"Se desactivó el estudiante con Id {id}");
        }
    }
}