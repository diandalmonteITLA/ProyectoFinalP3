using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Application.DTOS.Guardians;

namespace App.Core.Application.Interfaces
{
    public interface IGuardianService
    {
        Task<GuardianDto?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<GuardianDto>> GetAllAsync(bool includeInactive = false);
        Task AddAsync(CreateGuardianDto createGuardianDto);
        Task UpdateAsync(UpdateGuardianDto updateGuardianDto);
        Task DeactivateAsync(Guid id);
    }
}
