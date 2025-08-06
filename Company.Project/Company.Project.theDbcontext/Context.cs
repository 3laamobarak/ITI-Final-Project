using Company.Project.Domain.Enums;
using Company.Project.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
          
            base.OnModelCreating(modelBuilder);

            #region Seeding Data

            modelBuilder.Entity<ExampleClass>().HasData(
                new ExampleClass
                {
                    Id = 1,
                    Name = "Example 1",
                },
                new ExampleClass
                {
                    Id = 2,
                    Name = "Example 2",
                }
            );
            //modelBuilder.Entity<Brand>().HasData(
            //new Brand { Id = 1, Name = "Brand1" ,Description= "Description1" },
            //new Brand { Id = 2, Name = "Brand2" ,Description = "Description2" });



            modelBuilder.Entity<Brand>().HasData(
                                        new Brand { Id = 1, Name = "Apple", Description = "Electronics Brand" },
                                        new Brand { Id = 2, Name = "Samsung", Description = "Korean Electronics Brand" },
                                        new Brand { Id = 3, Name = "California Gold Nutrition", Description = "Health Supplements Brand" }
                                           );


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Vitamins", Description = "Vitamins and multivitamins", CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Category { Id = 2, Name = "Supplements", Description = "Dietary and herbal supplements", CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Category { Id = 3, Name = "Personal Care", Description = "Skincare and personal hygiene", CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Category { Id = 4, Name = "Sports Nutrition", Description = "Protein & performance nutrition", CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Category { Id = 5, Name = "Baby", Description = "Baby health and care", CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Vitamin C 1000mg",
                    Description = "High potency vitamin C tablets",
                    Price = 299.00m,
                    StockQuantity = 120,
                    ExpiryDate = new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 1, // Vitamins
                    BrandId = 2,    // Solgar
                    CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Omega-3 Fish Oil",
                    Description = "EPA/DHA fish oil softgels",
                    Price = 450.00m,
                    StockQuantity = 80,
                    ExpiryDate = new DateTime(2027, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 2, // Supplements
                    BrandId = 1,    // Now Foods
                    CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Vitamin D3 5000 IU",
                    Description = "Vitamin D3 softgels for bone health",
                    Price = 220.00m,
                    StockQuantity = 200,
                    ExpiryDate = new DateTime(2026, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 1, // Vitamins
                    BrandId = 3,    // California Gold Nutrition
                    CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "Whey Protein 2lb",
                    Description = "Whey protein concentrate",
                    Price = 1250.00m,
                    StockQuantity = 35,
                    ExpiryDate = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 4, // Sports Nutrition
                    BrandId = 1,    // Now Foods
                    CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Hyaluronic Acid Serum",
                    Description = "Hydrating face serum",
                    Price = 320.00m,
                    StockQuantity = 60,
                    ExpiryDate = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 3, // Personal Care
                    BrandId = 3,    // California Gold Nutrition
                    CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );


            #endregion

            #region Filters

            modelBuilder.Entity<ExampleClass>(entity =>
                {
                    entity.HasQueryFilter(c => !c.IsDeleted);
                });
            modelBuilder.Entity<Product>(entity =>
                {
                    entity.HasQueryFilter(c => !c.IsDeleted);
                });
            modelBuilder.Entity<Category>(entity =>
                {
                    entity.HasQueryFilter(c => !c.IsDeleted);
                });


            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExampleClass>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });



            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
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

        public DbSet<CartItem> CartItems { get; set; }
        

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }




        #endregion

    }
}
