using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Company.Project.Domain.Interfaces;
using Company.Project.theDbcontext;
using Company.Project.Domain.Models;

namespace Company.Project.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> SearchAsync(string query)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            // First check if the category exists and has any products
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == categoryId);
                
            if (!categoryExists)
            {
                return new List<Product>(); // Return empty list if category doesn't exist
            }
            
            // Get products with explicit join to ensure we get the right relationships
            var products = await _context.productCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.Product)
                .ToListAsync();
                
            return products;
        }
    }
}
