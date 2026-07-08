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

            //Relationships
            builder.HasMany(s => s.Guardian)      
                .WithMany(g => g.Students)      
                .UsingEntity(j =>
                {
                    j.ToTable("StudentGuardians");

                    // Nombrando explicitamente los campos de los foreign keys
                    j.Property<Guid>("GuardianId").HasColumnName("GuardianId");
                    j.Property<Guid>("StudentId").HasColumnName("StudentId");
                });

            builder.HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId);
        }
    }
}
