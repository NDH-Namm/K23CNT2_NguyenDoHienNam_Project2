using Microsoft.AspNetCore.Mvc;
using HeThongNhaSach.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class SanPhamController : Controller
{
    private readonly NhaSachContext _context;

    public SanPhamController(NhaSachContext context)
    {
        _context = context;
    }

    // 📘 Action xem chi tiết sản phẩm
    [HttpGet]
    public IActionResult ChiTiet(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        // Lấy sản phẩm theo ID và load cả danh mục (nếu có)
        var sp = _context.SanPhams
            .Include(s => s.MaDmNavigation)
            .FirstOrDefault(s => s.MaSp == id);

        if (sp == null)
        {
            return NotFound();
        }

        return View(sp); // ✅ truyền model SanPham sang view ChiTiet.cshtml
    }

    // 🔍 Action tìm kiếm (bạn đã có)
    [HttpGet]
    public IActionResult Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        var result = _context.SanPhams
            .Where(sp => sp.TenSp.ToLower().Contains(term.ToLower()))
            .Select(sp => new
            {
                maSp = sp.MaSp,
                tenSp = sp.TenSp,
                giaBan = sp.GiaBan,
                hinhAnh = sp.HinhAnh
            })
            .Take(10)
            .ToList();

        return Json(result);
    }
}
