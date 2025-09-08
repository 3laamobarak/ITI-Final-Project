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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
       

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Comment)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(r => r.Rating)
                   .IsRequired();
            builder.HasOne(r => r.User)
                   .WithMany( u => u.Reviews) 
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Reviews) 
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
