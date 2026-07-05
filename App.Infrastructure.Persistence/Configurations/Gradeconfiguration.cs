using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Core.Domain.Entities;

namespace App.Infrastructure.Persistence.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
   
            builder.HasOne(g => g.TeacherInCharge)
                .WithMany(t => t.ManagedGrades)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Students)
                .WithOne(s => s.Grade)
                .HasForeignKey(s => s.GradeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}