using HeThongNhaSach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Controllers
{
    public class GioHangController : Controller
    {
        private readonly NhaSachContext _context;

        public GioHangController(NhaSachContext context)
        {
            _context = context;
        }

        // 📌 Xem giỏ hàng
        public IActionResult Index()
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefault(g => g.MaTk == maTk);

            return View(gioHang);
        }

        // 📌 Thêm sản phẩm vào giỏ
        [HttpPost]
        public IActionResult AddToCart(int maSp, int soLuong = 1)
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            // tìm giỏ hàng của user
            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .FirstOrDefault(g => g.MaTk == maTk);

            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    MaTk = maTk.Value,
                    NgayTao = DateTime.Now
                };
                _context.GioHangs.Add(gioHang);
                _context.SaveChanges();
            }

            // kiểm tra sản phẩm đã có chưa
            var ct = gioHang.ChiTietGioHangs.FirstOrDefault(x => x.MaSp == maSp);
            if (ct != null)
            {
                ct.SoLuong += soLuong;
            }
            else
            {
                var sp = _context.SanPhams.Find(maSp);
                if (sp == null) return NotFound();

                var chiTiet = new ChiTietGioHang
                {
                    MaGh = gioHang.MaGh,
                    MaSp = sp.MaSp,
                    SoLuong = soLuong,
                    DonGia = sp.GiaBan
                };
                _context.ChiTietGioHangs.Add(chiTiet);
            }

            _context.SaveChanges();
            TempData["Message"] = "Đã thêm vào giỏ hàng!";
            return RedirectToAction("Index");
        }

        // 📌 Thanh toán (chuyển giỏ → đơn hàng)
        [HttpPost]
        public IActionResult ThanhToan()
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefault(g => g.MaTk == maTk);

            if (gioHang == null || !gioHang.ChiTietGioHangs.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index");
            }

            decimal tongTien = gioHang.ChiTietGioHangs.Sum(ct => ct.SoLuong * ct.DonGia);

            var donHang = new DonHang
            {
                MaTk = maTk.Value,
                NgayDat = DateTime.Now,
                TongTien = tongTien,
                TrangThai = "Chờ xác nhận"
            };
            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

            foreach (var item in gioHang.ChiTietGioHangs)
            {
                var chiTiet = new ChiTietDonHang
                {
                    MaDh = donHang.MaDh,
                    MaSp = item.MaSp,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia
                };
                _context.ChiTietDonHangs.Add(chiTiet);
            }

            _context.ChiTietGioHangs.RemoveRange(gioHang.ChiTietGioHangs);
            _context.SaveChanges();

            TempData["Message"] = "Thanh toán thành công! Đơn hàng đã được tạo.";
            return RedirectToAction("DonHangCuaToi", "TaiKhoan");
        }
    }
}
