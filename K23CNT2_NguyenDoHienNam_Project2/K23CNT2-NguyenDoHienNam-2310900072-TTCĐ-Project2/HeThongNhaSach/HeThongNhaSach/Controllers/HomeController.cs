using System.Diagnostics;
using HeThongNhaSach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeThongNhaSach.ViewModels;
namespace HeThongNhaSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NhaSachContext _context; // ✅ thêm context

        // ✅ constructor nhận context từ DI
        public HomeController(ILogger<HomeController> logger, NhaSachContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var spNoiBat = _context.SanPhams
                .Where(sp => sp.NoiBat == true)
                .Take(8)
                .ToList();

            var flashSale = _context.SanPhams
                .Where(sp => sp.FlashSale == true)
                .Take(4)
                .ToList();

            var vm = new HomeIndexViewModel
            {
                SanPhamNoiBat = spNoiBat,
                FlashSale = flashSale
            };

            return View(vm);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
