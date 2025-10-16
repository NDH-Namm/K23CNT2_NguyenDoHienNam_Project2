using Microsoft.AspNetCore.Mvc;

namespace HeThongNhaSach.Controllers
{
    public class ThongBaoController : Controller
    {
        public IActionResult Index()
        {
            // Sau này có thể lấy từ DB, giờ cho tạm danh sách mẫu
            var thongBao = new List<string>
            {
                "🎉 Giảm giá 20% tất cả sách thiếu nhi tuần này!",
                "🚚 Miễn phí vận chuyển cho đơn hàng trên 250.000đ.",
                "📚 Sách mới: 'Nhà Giả Kim - Tái bản 2025' đã có mặt.",
                "🔔 Bạn có 1 đơn hàng đang chờ xử lý."
            };

            return View(thongBao);
        }
    }
}
