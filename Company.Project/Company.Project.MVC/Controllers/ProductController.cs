using Company.Project.theDbcontext;
using Company.Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ProductController : Controller
{
    private readonly Context _context;
    public ProductController(Context context)
    {
        _context = context;
    }

    // GET: /Product/Index
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Brand) // احتياطيًا لو محتاجة اسم البراند
            .Select(p => new Company.Project.DTO.DTO.Product.ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                StockQuantity = p.StockQuantity,
                AverageRating = p.AverageRating,
                ReviewCount = p.ReviewCount,
                BrandName = p.Brand != null ? p.Brand.Name : null,
                ImageUrl = p.ImageUrl
            })
            .ToListAsync();

        return View(products);
    }

    // GET: /Product/Add
    [HttpGet]
    public IActionResult Add()
    {
        PopulateBrands();
        return View(new ProductCreateVm());
    }

    // POST: /Product/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ProductCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            PopulateBrands();
            return View(vm);
        }

        var product = new Product
        {
            Name = vm.Name,
            Description = vm.Description,
            Price = vm.Price,
            StockQuantity = vm.StockQuantity,
            BrandId = vm.BrandId,
            ImageUrl = vm.ImageUrl
            // CreatedAt/UpdatedAt handled by Context.SaveChangesAsync override if implemented
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private void PopulateBrands()
    {
        var brands = _context.Brands
            .OrderBy(b => b.Name)
            .Select(b => new { b.Id, b.Name })
            .ToList();

        ViewBag.Brands = new SelectList(brands, "Id", "Name");
    }
}
