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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> SearchAsync(string query)
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(query) || c.Description.Contains(query))
                .ToListAsync();
        }
    }
}
