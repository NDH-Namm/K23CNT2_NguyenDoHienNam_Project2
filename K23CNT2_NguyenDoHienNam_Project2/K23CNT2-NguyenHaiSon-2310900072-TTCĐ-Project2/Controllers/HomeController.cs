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
        private readonly NhaSachContext _context;

        public HomeController(ILogger<HomeController> logger, NhaSachContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // ✅ Sản phẩm nổi bật
            var spNoiBat = _context.SanPhams
                .Where(sp => sp.NoiBat == true)
                .Take(8)
                .ToList();

            // ✅ Flash Sale
            var flashSale = _context.SanPhams
                .Where(sp => sp.FlashSale == true)
                .Take(4)
                .ToList();

            // ✅ Đồ chơi Halloween trong danh mục Đồ chơi trẻ em (MaDM = 4)
            var halloweenToys = _context.SanPhams
     .Where(sp => sp.MaDm == 4 && sp.TenSp.Contains("Halloween"))
     .Take(4)
     .ToList();



            // ✅ Gộp tất cả vào ViewModel
            var vm = new HomeIndexViewModel
            {
                SanPhamNoiBat = spNoiBat,
                FlashSale = flashSale,
                HalloweenToys = halloweenToys
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
