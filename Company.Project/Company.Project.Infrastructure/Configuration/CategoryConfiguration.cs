using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Company.Project.Domain.Models;

namespace Company.Project.Infrastructure.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table name
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties configuration
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            // Relationships
            builder.HasMany(c => c.ProductCategories)

                         .WithOne(pc => pc.Category)
                         .HasForeignKey(pc => pc.CategoryId);

            // Indexes
            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
   