using App.Core.Application.DTOs.Grades;
using App.Core.Application.Interfaces;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using AutoMapper;

namespace App.Core.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradeService(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        public async Task<GradeDto?> GetByIdAsync(Guid id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);

            if (grade == null)
                return null;

            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<IReadOnlyCollection<GradeDto>> GetAllAsync()
        {
            var grades = await _gradeRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyCollection<GradeDto>>(grades);
        }

        public async Task UpdateAsync(Guid id, UpdateGradeDto dto)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);

            if (grade == null)
                throw new KeyNotFoundException($"No se encontró el curso con Id {id}.");

            _mapper.Map(dto, grade);

            await _gradeRepository.UpdateAsync(grade);
        }
    }
}