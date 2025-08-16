using Company.Project.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Infrastructure.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.Id);

            // Properties
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Description)
                   .HasMaxLength(500);

            // Relationships
            builder.HasMany(b => b.Products)
                   .WithOne(p => p.Brand)
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
