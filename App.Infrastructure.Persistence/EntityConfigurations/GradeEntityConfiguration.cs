using App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Persistence.EntityConfigurations
{
    public class GradeEntityConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {

            //Relationships
            builder.HasOne(g => g.TeacherInCharge)
                .WithMany(t => t.ManagedGrades)
                .HasForeignKey(g => g.TeacherId);

            builder.Navigation(g => g.TeacherInCharge).AutoInclude();
            builder.Navigation(g => g.Students).AutoInclude();
        }
    }
}
