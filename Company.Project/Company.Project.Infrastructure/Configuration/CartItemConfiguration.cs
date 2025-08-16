using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.Infrastructure.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.quantity)
                .IsRequired();

            builder.HasOne(ci => ci.product)
                .WithMany()
                .HasForeignKey(ci => ci.productId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.user)
                .WithMany()
                .HasForeignKey(ci => ci.userId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ci => ci.userId)
                .IsRequired();
        }
    }
}
