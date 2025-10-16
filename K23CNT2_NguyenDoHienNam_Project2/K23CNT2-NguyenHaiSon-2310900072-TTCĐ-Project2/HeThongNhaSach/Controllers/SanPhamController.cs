using Microsoft.AspNetCore.Mvc;
using HeThongNhaSach.Models;
using System.Linq;

public class SanPhamController : Controller
{
    private readonly NhaSachContext _context;

    public SanPhamController(NhaSachContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        var result = _context.SanPhams
            .Where(sp => sp.TenSp.ToLower().Contains(term.ToLower()))
            .Select(sp => new {
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
