using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Domain.Entities;

namespace App.Core.Domain.Interfaces
{
    public interface IGradeRepository
    {
        Task<Grade?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<Grade>> GetAllAsync();
        Task UpdateAsync(Grade grade);
    }
}
