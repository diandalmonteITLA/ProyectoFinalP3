using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Application.DTOs.Students;
using App.Core.Application.Interfaces;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using AutoMapper;

namespace App.Core.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IPhoneNumberValidator _phoneNumberValidator;
        private readonly IMapper _mapper;

        public StudentService(IGenericRepository<Student> studentRepository, IPhoneNumberValidator phoneNumberValidator, IMapper mapper)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _phoneNumberValidator = phoneNumberValidator ?? throw new ArgumentNullException(nameof(phoneNumberValidator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StudentDto?> GetByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student != null ? _mapper.Map<StudentDto>(student) : null;
        }

        public async Task<IReadOnlyCollection<StudentDto>> GetAllAsync(bool includeInactive = false)
        {
            var students = await _studentRepository.GetAllAsync(includeInactive);
            return _mapper.Map<IReadOnlyCollection<StudentDto>>(students);
        }

        public async Task AddAsync(CreateStudentDto createStudentDto)
        {
            if (createStudentDto is null)
            {
                throw new ArgumentException("El estudiante no puede estar vacío.", nameof(createStudentDto));
            }

            var student = _mapper.Map<Student>(createStudentDto);
            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateAsync(UpdateStudentDto updateStudentDto)
        {
            if (updateStudentDto is null)
            {
                throw new ArgumentException("El estudiante no puede estar vacío.", nameof(updateStudentDto));
            }


            var existingStudent = await _studentRepository.GetByIdAsync(updateStudentDto.Id);
            if (existingStudent is null)
            {
                throw new KeyNotFoundException("No se encontró el estudiante que se desea actualizar.");
            }

            _mapper.Map(updateStudentDto, existingStudent);
            await _studentRepository.UpdateAsync(existingStudent);
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