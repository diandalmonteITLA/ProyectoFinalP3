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
        private readonly IPhoneNumberValidator _phoneNumberValidator;

        public StudentService(IGenericRepository<Student> studentRepository, IPhoneNumberValidator phoneNumberValidator)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _phoneNumberValidator = phoneNumberValidator ?? throw new ArgumentNullException(nameof(phoneNumberValidator));
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

            if (student.PhoneNumber is not null && !_phoneNumberValidator.ValidateNumber(student.PhoneNumber.Number))
            {
                throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(student));
            }

            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateAsync(Student student)
        {
            if (student is null)
            {
                throw new ArgumentException("El estudiante no puede estar vacío.", nameof(student));
            }

            if (student.PhoneNumber is not null && !_phoneNumberValidator.ValidateNumber(student.PhoneNumber.Number))
            {
                throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(student));
            }

            var existingStudent = await _studentRepository.GetByIdAsync(student.Id);
            if (existingStudent is null)
            {
                throw new KeyNotFoundException("No se encontró el estudiante que se desea actualizar.");
            }

            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent is null)
            {
                throw new KeyNotFoundException("No se encontró el estudiante que se desea desactivar.");
            }

            await _studentRepository.DeactiveAsync(id);
        }
    }
}