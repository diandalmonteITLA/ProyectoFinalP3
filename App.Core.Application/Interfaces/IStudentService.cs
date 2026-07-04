using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Domain.Entities;

namespace App.Core.Application.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<Student>> GetAllAsync(bool includeInactive = false);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeactivateAsync(Guid id);
    }
}
