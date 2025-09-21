using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Company.Project.MVC.Controllers
{
    [Authorize(Policy = "admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = await GetDashboardData();
            return View(dashboardData);
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = await GetDashboardData();
            return Json(stats);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersChartData()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            
            // Get orders for the last 12 months
            var last12Months = Enumerable.Range(0, 12)
                .Select(i => DateTime.Now.AddMonths(-i))
                .Reverse()
                .ToList();

            var monthlyOrders = last12Months.Select(month => new
            {
                Month = month.ToString("MMM yyyy"),
                Count = orders.Count(o => o.CreatedAt.Month == month.Month && o.CreatedAt.Year == month.Year)
            }).ToList();

            return Json(monthlyOrders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersStatusData()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            
            var statusData = orders.GroupBy(o => o.Status)
                .Select(g => new
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                }).ToList();

            return Json(statusData);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersChartData()
        {
            var users = await _userManager.Users.ToListAsync();
            
            // Get users for the last 12 months
            var last12Months = Enumerable.Range(0, 12)
                .Select(i => DateTime.Now.AddMonths(-i))
                .Reverse()
                .ToList();

            var monthlyUsers = last12Months.Select(month => new
            {
                Month = month.ToString("MMM yyyy"),
                Count = 0 // Placeholder - ApplicationUser doesn't have CreatedAt property
            }).ToList();

            return Json(monthlyUsers);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecentOrders()
        {
            var recentOrders = await _unitOfWork.OrderRepository.GetAllAsync(
                includes: new System.Linq.Expressions.Expression<Func<Order, object>>[]
                {
                    o => o.User,
                    o => o.OrderItems
                }
            );

            var result = recentOrders
                .OrderByDescending(o => o.CreatedAt)
                .Take(10)
                .Select(o => new
                {
                    Id = o.Id,
                    UserName = o.User?.UserName ?? "Unknown",
                    Status = o.Status.ToString(),
                    TotalAmount = o.Total,
                    CreatedAt = o.CreatedAt.ToString("MMM dd, yyyy HH:mm"),
                    ItemCount = o.OrderItems?.Count ?? 0
                }).ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopUsers()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(
                includes: new System.Linq.Expressions.Expression<Func<Order, object>>[]
                {
                    o => o.User
                }
            );

            var topUsers = orders
                .Where(o => o.User != null)
                .GroupBy(o => o.User.UserName)
                .Select(g => new
                {
                    UserName = g.Key,
                    OrderCount = g.Count(),
                    TotalSpent = g.Sum(o => o.Total)
                })
                .OrderByDescending(u => u.TotalSpent)
                .Take(10)
                .ToList();

            return Json(topUsers);
        }

        private async Task<DashboardViewModel> GetDashboardData()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            var users = await _userManager.Users.ToListAsync();
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var lastMonth = currentMonth == 1 ? 12 : currentMonth - 1;
            var lastMonthYear = currentMonth == 1 ? currentYear - 1 : currentYear;

            // Calculate statistics
            var totalOrders = orders.Count();
            var totalUsers = users.Count;
            var totalProducts = products.Count();
            var totalRevenue = orders.Sum(o => o.Total);

            // Current month stats
            var currentMonthOrders = orders.Count(o => o.CreatedAt.Month == currentMonth && o.CreatedAt.Year == currentYear);
            var currentMonthRevenue = orders.Where(o => o.CreatedAt.Month == currentMonth && o.CreatedAt.Year == currentYear)
                .Sum(o => o.Total);
            var currentMonthUsers = 0; // Placeholder - ApplicationUser doesn't have CreatedAt property

            // Last month stats
            var lastMonthOrders = orders.Count(o => o.CreatedAt.Month == lastMonth && o.CreatedAt.Year == lastMonthYear);
            var lastMonthRevenue = orders.Where(o => o.CreatedAt.Month == lastMonth && o.CreatedAt.Year == lastMonthYear)
                .Sum(o => o.Total);
            var lastMonthUsers = 0; // Placeholder - ApplicationUser doesn't have CreatedAt property

            // Calculate growth percentages
            var orderGrowth = lastMonthOrders > 0 ? ((currentMonthOrders - lastMonthOrders) / (double)lastMonthOrders) * 100 : 0;
            var revenueGrowth = lastMonthRevenue > 0 ? ((double)(currentMonthRevenue - lastMonthRevenue) / (double)lastMonthRevenue) * 100 : 0;
            var userGrowth = lastMonthUsers > 0 ? ((currentMonthUsers - lastMonthUsers) / (double)lastMonthUsers) * 100 : 0;

            // Order status distribution
            var orderStatuses = orders.GroupBy(o => o.Status)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            // Recent orders
            var recentOrders = orders
                .OrderByDescending(o => o.CreatedAt)
                .Take(5)
                .Select(o => new OrderSummaryViewModel
                {
                    Id = o.Id,
                    UserName = o.User?.UserName ?? "Unknown",
                    Status = o.Status.ToString(),
                    TotalAmount = o.Total,
                    CreatedAt = o.CreatedAt,
                    ItemCount = o.OrderItems?.Count ?? 0
                }).ToList();

            return new DashboardViewModel
            {
                TotalOrders = totalOrders,
                TotalUsers = totalUsers,
                TotalProducts = totalProducts,
                TotalRevenue = totalRevenue,
                CurrentMonthOrders = currentMonthOrders,
                CurrentMonthRevenue = currentMonthRevenue,
                CurrentMonthUsers = currentMonthUsers,
                OrderGrowth = orderGrowth,
                RevenueGrowth = revenueGrowth,
                UserGrowth = userGrowth,
                OrderStatuses = orderStatuses,
                RecentOrders = recentOrders
            };
        }
    }

    public class DashboardViewModel
    {
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalRevenue { get; set; }
        public int CurrentMonthOrders { get; set; }
        public decimal CurrentMonthRevenue { get; set; }
        public int CurrentMonthUsers { get; set; }
        public double OrderGrowth { get; set; }
        public double RevenueGrowth { get; set; }
        public double UserGrowth { get; set; }
        public Dictionary<string, int> OrderStatuses { get; set; } = new Dictionary<string, int>();
        public List<OrderSummaryViewModel> RecentOrders { get; set; } = new List<OrderSummaryViewModel>();
    }

    public class OrderSummaryViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ItemCount { get; set; }
    }
}
