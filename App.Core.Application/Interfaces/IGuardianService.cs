using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Domain.Entities;

namespace App.Core.Application.Interfaces
{
    public interface IGuardianService
    {
        Task<Guardian?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<Guardian>> GetAllAsync(bool includeInactive = false);
        Task AddAsync(Guardian guardian);
        Task UpdateAsync(Guardian guardian);
        Task DeactivateAsync(Guid id);
    }
}
