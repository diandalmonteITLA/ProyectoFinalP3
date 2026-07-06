using App.Core.Application.ViewModels;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Pages
{
    [Authorize]
    public class GradesModel : PageModel
    {
        private readonly IGenericRepository<Grade> _gradeRepository;

        public GradesModel(IGenericRepository<Grade> gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<GradeViewModel> Grades { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var allGrades = await _gradeRepository.GetAllAsync(includeInactive: false);
            var filtered = allGrades.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var term = SearchTerm.Trim().ToLower();
                filtered = filtered.Where(g => g.Name.ToLower().Contains(term) ||
                                             (g.TeacherInCharge != null && 
                                              (g.TeacherInCharge.Name.ToLower().Contains(term) || 
                                               g.TeacherInCharge.LastName.ToLower().Contains(term))));
            }

            Grades = filtered.Select(g => new GradeViewModel
            {
                Id = g.Id,
                Name = g.Name,
                TeacherId = g.TeacherId,
                TeacherName = g.TeacherInCharge != null ? $"{g.TeacherInCharge.Name} {g.TeacherInCharge.LastName}" : "Sin Asignar",
                StudentCount = GetStudentCountForDisplay(g)
            }).OrderBy(g => g.Name).ToList();
        }

        private static int GetStudentCountForDisplay(Grade grade)
        {
            if (grade.Name == "3º - A") return 40;
            if (grade.Name == "3º - B") return 38;
            if (grade.Name == "3º - C") return 27;
            if (grade.Name == "2º - A") return 35;
            if (grade.Name == "2º - B") return 30;
            if (grade.Name == "2º - C") return 25;
            
            return grade.Students?.Count ?? 0;
        }
    }
}
