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
    public class GuardianEntityConfiguration :IEntityTypeConfiguration<Guardian>
    {
        public void Configure(EntityTypeBuilder<Guardian> builder)
        {
            //Property Configurations
            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
            builder.Property(g => g.LastName).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Email).IsRequired().HasMaxLength(254);

            //Relationships
            builder.OwnsMany(g => g.PhoneNumbers, phone =>
            {
                phone.ToTable("GuardianPhoneNumbers");
                phone.Property(p => p.Number).HasMaxLength(20).IsRequired();
                phone.Property(p => p.Type).HasConversion<string>();
            });
        }
        
    }
}
