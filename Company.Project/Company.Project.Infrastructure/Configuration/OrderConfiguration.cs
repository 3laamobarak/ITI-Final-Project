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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.OrderType)
                   .IsRequired();

            builder.Property(o => o.Status)
                   .IsRequired();

            builder.Property(o => o.ShippingAddress)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(o => o.Subtotal)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Tax)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Discount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.ShippingCost)
                   .HasColumnType("decimal(18,2)");

            builder.Ignore(o => o.Total);

            builder.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict); 

            // OrderItems relationship
            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Refunds relationship
            builder.HasMany(o => o.Refunds)
                   .WithOne(r => r.Order)
                   .HasForeignKey(r => r.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
