using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using App.Infrastructure.Persistence.Contexts;

namespace App.Infrastructure.Persistence.Seeds
{
    public class GradeSeeding
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var grades = new List<Grade>
            {
                new Grade { Name = "1ro de Secundaria" },
                new Grade { Name = "2do de Secundaria" },
                new Grade { Name = "3ro de Secundaria" }
            };

            foreach (Grade grade in grades)
            {
                await context.Set<Grade>().AddAsync(grade);
            }

            await context.SaveChangesAsync();
        }
    }
}
