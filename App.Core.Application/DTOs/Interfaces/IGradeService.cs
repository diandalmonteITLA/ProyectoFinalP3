using App.Core.Application.DTOs;

namespace App.Core.Application.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDto?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<GradeDto>> GetAllAsync();
        Task UpdateAsync(Guid id, UpdateGradeDto dto);
    }
}