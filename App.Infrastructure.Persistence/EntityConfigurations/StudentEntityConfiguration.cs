using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Persistence.EntityConfigurations
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Property Configurations
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.LastName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(254);

            //Relationships
            builder.HasMany(s => s.Guardian)      
                .WithMany(g => g.Students)      
                .UsingEntity(j =>
                {
                    j.ToTable("StudentGuardians");

                    // Nombrando explicitamente los campos de los foreign keys
                    j.Property("GuardianId").HasColumnName("GuardianId");
                    j.Property("StudentId").HasColumnName("StudentId");
                });

            builder.OwnsOne(s => s.PhoneNumber, phone =>
            {
                phone.Property(p => p.Number).HasMaxLength(20);
                phone.Property(p => p.Type).HasConversion<string>();
            });

            builder.HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId);
        }
    }
}
