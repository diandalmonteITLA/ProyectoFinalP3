using App.Core.Application.DTOs.Teacher;
using App.Core.Application.Interfaces;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IGenericRepository<Teacher> _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(IGenericRepository<Teacher> teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TeacherDto?> GetByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
                return null;

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<IReadOnlyCollection<TeacherDto>> GetAllAsync(bool includeInactive = false)
        {
            var teachers = await _teacherRepository.GetAllAsync(includeInactive);

            return _mapper.Map<IReadOnlyCollection<TeacherDto>>(teachers);
        }

        public async Task AddAsync(CreateTeacherDto dto)
        {
            var teacher = _mapper.Map<Teacher>(dto);

            await _teacherRepository.AddAsync(teacher);
        }

        public async Task UpdateAsync(UpdateTeacherDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentException("El docente no puede estar vacío.", nameof(dto));
            }

            var existingTeacher = await _teacherRepository.GetByIdAsync(dto.Id);
            if (existingTeacher is null)
            {
                throw new KeyNotFoundException("No se encontró el docente que se desea actualizar.");
            }

            _mapper.Map(dto, existingTeacher);
            await _teacherRepository.UpdateAsync(existingTeacher);
        }

        public async Task DeactivateAsync(Guid id)
        {
            await _teacherRepository.DeactiveAsync(id);
        }
    }
}
