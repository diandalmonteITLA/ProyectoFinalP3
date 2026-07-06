using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Grade?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Grade>().FindAsync(id);
        }

        public async Task<IReadOnlyCollection<Grade>> GetAllAsync()
        {
            var query = _context.Set<Grade>().AsQueryable();

            return await query.ToListAsync();
        }
        public async Task UpdateAsync(Grade grade)
        {
            _context.Set<Grade>().Update(grade);
            await _context.SaveChangesAsync();
        }
    }
}
