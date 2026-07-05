using App.Core.Application.DTOs.Teacher;
using App.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.Interfaces
{
    public  interface ITeacherService
    {
        Task<TeacherDto?> GetByIdAsync(Guid id);

        Task<IReadOnlyCollection<TeacherDto>> GetAllAsync(bool includeInactive = false);

        Task AddAsync(CreateTeacherDto dto);

        Task UpdateAsync(UpdateTeacherDto dto);

        Task DeactivateAsync(Guid id);
    }
}
