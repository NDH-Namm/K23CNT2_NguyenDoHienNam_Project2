using HeThongNhaSach.Models;
using HeThongNhaSach.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly NhaSachContext _context;

        public TaiKhoanController(NhaSachContext context)
        {
            _context = context;
        }

        // GET: /TaiKhoan/DangKy
        public IActionResult DangKy()
        {
            return View();
        }

        // POST: /TaiKhoan/DangKy
        // POST: /TaiKhoan/DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(TaiKhoan model, string XacNhanMatKhau)
        {
            if (ModelState.IsValid)
            {
                // check xác nhận mật khẩu
                if (model.MatKhau != XacNhanMatKhau)
                {
                    ViewBag.Error = "Mật khẩu xác nhận không khớp!";
                    return View(model);
                }

                // check email trùng
                if (_context.TaiKhoans.Any(x => x.Email == model.Email))
                {
                    ViewBag.Error = "Email đã tồn tại!";
                    return View(model);
                }

                model.NgayTao = DateTime.Now;
                model.VaiTro = "KhachHang";

                _context.TaiKhoans.Add(model);
                _context.SaveChanges();

                // ✅ set session tự login luôn
                HttpContext.Session.SetInt32("MaTK", model.MaTk);
                HttpContext.Session.SetString("HoTen", model.HoTen);
                HttpContext.Session.SetString("Email", model.Email);

                // ✅ Thêm thông báo
                TempData["Message"] = "Đăng ký thành công!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        // GET: /TaiKhoan/DangNhap
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: /TaiKhoan/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(string email, string matkhau)
        {
            var user = _context.TaiKhoans
                        .FirstOrDefault(u => u.Email == email && u.MatKhau == matkhau);

            if (user != null)
            {
                HttpContext.Session.SetInt32("MaTK", user.MaTk);
                HttpContext.Session.SetString("HoTen", user.HoTen);
                HttpContext.Session.SetString("Email", user.Email);
                TempData["Message"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai email hoặc mật khẩu!";
            return View();
        }
        // ✅ Hiển thị thông tin cá nhân
        public IActionResult ThongTinCaNhan()
        {
            int? maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
                return RedirectToAction("DangNhap");

            var tk = _context.TaiKhoans.FirstOrDefault(t => t.MaTk == maTk);
            if (tk == null) return NotFound();

            var model = new ThongTinKhachHangViewModel
            {
                HoTen = tk.HoTen,
                Email = tk.Email,
                SoDienThoai = tk.SoDienThoai,
                DiaChi = tk.DiaChi
            };

            return View(model);
        }

        // ✅ Cập nhật thông tin
        [HttpPost]
        public IActionResult ThongTinCaNhan(ThongTinKhachHangViewModel model)
        {
            int? maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
                return RedirectToAction("DangNhap");

            var tk = _context.TaiKhoans.FirstOrDefault(t => t.MaTk == maTk);
            if (tk == null) return NotFound();

            tk.HoTen = model.HoTen;
            tk.Email = model.Email;
            tk.SoDienThoai = model.SoDienThoai;
            tk.DiaChi = model.DiaChi;

            _context.Update(tk);
            _context.SaveChanges();

            ViewBag.ThongBao = "Cập nhật thông tin thành công!";
            return View(model);
        }
    

        // GET: /TaiKhoan/DangXuat
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Bạn đã đăng xuất!";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DonHangCuaToi()
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
            {
                return RedirectToAction("DangNhap");
            }

            var donHangs = _context.DonHangs
                .Where(d => d.MaTk == maTk)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(donHangs);
        }

    }
}
