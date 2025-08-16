using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        private readonly Context _context;

        public BrandRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync(int skip, int take)
        {
            return await _context.Brands
                .OrderBy(b => b.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Brand>> SearchAsync(string query)
        {
            return await _context.Brands
                .Where(b => b.Name.Contains(query) || b.Description.Contains(query))
                .ToListAsync();
        }

        public async Task<Brand?> GetBrandWithProductsAsync(int id)
        {
            return await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
