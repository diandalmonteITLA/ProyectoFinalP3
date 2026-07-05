using App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace App.Infrastructure.Persistence.EntityConfigurations
{
    public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            //Property configurations
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(254);

            //Relationships
            builder.OwnsMany(t => t.PhoneNumbers, phone =>
            {
                phone.ToTable("TeacherPhoneNumbers");
                phone.Property(p => p.Number).HasMaxLength(20).IsRequired();
                phone.Property(p => p.Type).HasConversion<string>();
            });
        }
    }
}
