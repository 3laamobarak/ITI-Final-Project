using Company.Project.Domain.Enums;
using Company.Project.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.theDbcontext
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seeding Data
            modelBuilder.Entity<ExampleClass>().HasData(
                new ExampleClass
                {
                    Id= 1,
                    Name = "Example 1",
                },
                new ExampleClass
                {
                    Id = 2,
                    Name = "Example 2",
                }
            );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            // Seeding
            var testUserId = "test-user-id";
            var productId = 1;

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = testUserId,
                    UserName = "testuser",
                    FirstName = "Bassel",
                    LastName = "Ahmed",
                    Gender = "Male",
                    NID ="sadsadaf",
                    MaritalStatus ="Married", 
                    NormalizedUserName = "TESTUSER",
                    Email = "testuser@example.com",
                    NormalizedEmail = "TESTUSER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Test@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            );

            modelBuilder.Entity<Brand>().HasData(
          new Brand { Id = 1, Name = "Default Brand" ,Description="afasf"}
      );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Default Category" , Description = "afasf" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Sample Product",
                    Description = "Seeded product",
                    Price = 49.99m,
                    StockQuantity = 0,
                    BrandId = 1,
                    CategoryId = 1,
                    CreatedAt = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddYears(1),
                    IsDeleted = false
                }
            ); modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    Comment = "Excellent product!",
                    Rating = 4.5M,
                    UserId = testUserId,
                    ProductId = productId
                },
                new Review
                {
                    Id = 2,
                    Comment = "Not bad at all.",
                    Rating = 3.8M,
                    UserId = testUserId,
                    ProductId = productId
                }
            );
          

            // Fake order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderType = Enums.OrderType.Online,
                    Status = Enums.OrderStatus.Pending,
                    ShippingAddress = "123 Test Street",
                    Subtotal = 200,
                    Tax = 20,
                    Discount = 10,
                    ShippingCost = 15,
                    UserId = testUserId
                }
            );

            // OrderItem for the above order
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 2
                    // SubTotal is calculated, so don't seed it directly.
                }
            );

            #endregion

            #region Filters
            modelBuilder.Entity<ExampleClass>(entity =>
            {
                entity.HasQueryFilter(c => !c.IsDeleted);
            });

            #endregion
            
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
        
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        
        #region Dbsets
        public DbSet<ExampleClass> ExClass { get; set; }

        public DbSet<Order> orders { get; set; }        

        public DbSet<Review> reviews    { get; set; }

        public DbSet<Product> products { get; set; }


        #endregion
        
    }
}
