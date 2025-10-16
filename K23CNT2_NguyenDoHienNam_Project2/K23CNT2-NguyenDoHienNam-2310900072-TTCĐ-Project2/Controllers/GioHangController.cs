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
                .ThenInclude(ct => ct.MaSpNavigation)  // ✅ BẮT BUỘC
                .FirstOrDefault(g => g.MaTk == maTk);

            return View(gioHang);
        }



        // 📌 Thêm sản phẩm vào giỏ
        [HttpPost]
        public IActionResult AddToCart(int maSp, int soLuong = 1)
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            // 🔹 Lấy giỏ hàng hiện tại của tài khoản
            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .FirstOrDefault(g => g.MaTk == maTk);

            // 🔹 Nếu chưa có giỏ hàng => tạo mới
            if (gioHang == null)
            {
                gioHang = new GioHang { MaTk = maTk.Value };
                _context.GioHangs.Add(gioHang);
                _context.SaveChanges(); // Cần để có MaGh (ID tự tăng)
            }

            // 🔹 Kiểm tra sản phẩm đã tồn tại trong giỏ chưa
            var chiTiet = gioHang.ChiTietGioHangs.FirstOrDefault(c => c.MaSp == maSp);
            if (chiTiet == null)
            {
                chiTiet = new ChiTietGioHang
                {
                    MaGh = gioHang.MaGh,  // ✅ Dùng đúng property FK là MaGh
                    MaSp = maSp,
                    SoLuong = soLuong,
                    DonGia = _context.SanPhams
                        .Where(s => s.MaSp == maSp)
                        .Select(s => s.GiaBan)
                        .FirstOrDefault()
                };
                _context.ChiTietGioHangs.Add(chiTiet);
            }
            else
            {
                chiTiet.SoLuong += soLuong;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult XoaSanPham(int maSp)
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .FirstOrDefault(g => g.MaTk == maTk);

            if (gioHang != null)
            {
                var chiTiet = gioHang.ChiTietGioHangs.FirstOrDefault(ct => ct.MaSp == maSp);
                if (chiTiet != null)
                {
                    _context.ChiTietGioHangs.Remove(chiTiet);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CapNhatSoLuong(int maSp, string actionType)
        {
            var maTk = HttpContext.Session.GetInt32("MaTK");
            if (maTk == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .FirstOrDefault(g => g.MaTk == maTk);

            if (gioHang != null)
            {
                var chiTiet = gioHang.ChiTietGioHangs.FirstOrDefault(c => c.MaSp == maSp);
                if (chiTiet != null)
                {
                    if (actionType == "tang") chiTiet.SoLuong++;
                    else if (actionType == "giam" && chiTiet.SoLuong > 1) chiTiet.SoLuong--;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ApDungMaGiamGia(string maGiamGia)
        {
            if (string.IsNullOrWhiteSpace(maGiamGia))
            {
                ViewBag.Loi = "Vui lòng nhập mã giảm giá.";
            }
            else if (maGiamGia.Trim().ToUpper() == "GIAM40K")
            {
                ViewBag.GiamGia = 40000;
                ViewBag.ThongBao = "Áp dụng mã giảm giá thành công! 🎉";
            }
            else
            {
                ViewBag.Loi = "Mã giảm giá không hợp lệ.";
            }

            var maTk = HttpContext.Session.GetInt32("MaTK");
            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .FirstOrDefault(g => g.MaTk == maTk);

            return View("Index", gioHang);
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
