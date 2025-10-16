using HeThongNhaSach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminDonHangsController : Controller
{
    private readonly NhaSachContext _context;

    public AdminDonHangsController(NhaSachContext context)
    {
        _context = context;
    }

    // GET: AdminDonHangs
    public async Task<IActionResult> Index()
    {
        var donHangs = await _context.DonHangs
            .Include(d => d.MaTkNavigation)
            .ToListAsync();
        return View(donHangs);
    }

    // GET: AdminDonHangs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var donHang = await _context.DonHangs.FindAsync(id);
        if (donHang == null) return NotFound();

        return View(donHang);
    }

    // POST: AdminDonHangs/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DonHang donHang)
    {
        if (id != donHang.MaDh) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(donHang);
                await _context.SaveChangesAsync();
                TempData["Message"] = "✔️ Cập nhật đơn hàng thành công!";
                return RedirectToAction(nameof(Index)); // 🔑 tránh resubmit
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DonHangs.Any(e => e.MaDh == donHang.MaDh))
                    return NotFound();
                else
                    throw;
            }
        }

        TempData["Message"] = "⚠️ Có lỗi khi cập nhật đơn hàng!";
        return View(donHang);
    }
}
