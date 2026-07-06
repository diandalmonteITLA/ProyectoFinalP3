using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Application.DTOS.Students;

namespace App.Core.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<StudentDto>> GetAllAsync(bool includeInactive = false);
        Task AddAsync(CreateStudentDto createStudentDto);
        Task UpdateAsync(UpdateStudentDto updateStudentDto);
        Task DeactivateAsync(Guid id);
    }
}
