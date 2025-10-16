using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeThongNhaSach.Models; // namespace chứa NhaSachContext
public class DanhMucController : Controller
{
    private readonly NhaSachContext _context; // ✅ biến context dùng toàn controller

    // Trang tổng hợp danh mục
    public DanhMucController(NhaSachContext context)
    {
        _context = context;
    }
    public IActionResult DanhMuc()
    {

        ViewBag.Active = ""; // không highlight mặc định
        ViewBag.TitleDanhMuc = "Danh mục sản phẩm";

        ViewBag.SubCategories = new List<(string, List<string>)>
        {
            ("📚 Sách Trong Nước", new List<string>{ "Văn học", "Kinh tế", "Kỹ năng sống", "Sách thiếu nhi" }),
            ("🌍 Sách Nước Ngoài", new List<string>{ "Tiếng Anh", "Tiếng Nhật", "Tiếng Hàn", "Tiếng Trung" }),
            ("✏️ Dụng Cụ Học Sinh", new List<string>{ "Bút", "Vở", "Thước kẻ", "Balo" }),
            ("🧸 Đồ Chơi", new List<string>{ "Xếp hình", "Lego", "Đồ chơi trí tuệ", "Đồ chơi vận động" })
        };

        return View("DanhMuc");
    }

    // Sách trong nước
    public IActionResult SachTrongNuoc()
    {
        ViewBag.Active = "TrongNuoc";
        ViewBag.TitleDanhMuc = "📚 Sách Trong Nước";

        // Danh mục con (luôn hiện)
        ViewBag.SubCategories = new List<(string, List<string>)>
    {
        ("Văn học", new List<string>{ "Tiểu thuyết", "Truyện ngắn - Tản văn", "Ngôn tình", "Light Novel" }),
        ("Kinh tế", new List<string>{ "Quản trị", "Marketing", "Khởi nghiệp", "Phân tích kinh tế" }),
        ("Kỹ năng sống", new List<string>{ "Tâm lý", "Rèn luyện bản thân", "Kỹ năng mềm", "Tuổi mới lớn" })
    };

        // 🔥 Thêm sản phẩm nổi bật
        var sanPhamNoiBat = _context.SanPhams
            .Where(sp => sp.MaDm == 1) // giả sử MaDM=1 là Sách Trong Nước
            .OrderByDescending(sp => sp.GiamGia)
            .Take(4)
            .ToList();

        ViewBag.SanPhamNoiBat = sanPhamNoiBat;

        return View("SachTrongNuoc");
    }


    // Sách nước ngoài
    public IActionResult SachNgoaiNuoc()
    {
        ViewBag.Active = "NgoaiNuoc";
        ViewBag.TitleDanhMuc = "🌍 Sách Nước Ngoài";

        ViewBag.SubCategories = new List<(string, List<string>)>
    {
        ("Tiểu thuyết dịch", new List<string>{ "Văn học Mỹ", "Văn học Anh", "Văn học Nhật", "Light Novel quốc tế" }),
        ("Kinh tế quốc tế", new List<string>{ "Tài chính", "Quản trị toàn cầu", "Đầu tư", "Marketing quốc tế" }),
        ("Học thuật", new List<string>{ "Sách giáo khoa", "Nghiên cứu", "Tham khảo", "Từ điển" })
    };

        var sanPhamNoiBat = _context.SanPhams
            .Where(sp => sp.MaDm == 2) // giả sử MaDm=2: Sách Nước Ngoài
            .OrderByDescending(sp => sp.GiamGia)
            .Take(4)
            .ToList();

        ViewBag.SanPhamNoiBat = sanPhamNoiBat;

        return View("DanhMuc");
    }
    // Dụng cụ học sinh
    public IActionResult DungCuHocSinh()
    {
        ViewBag.Active = "DungCu";
        ViewBag.TitleDanhMuc = "✏️ Dụng Cụ Học Sinh";

        ViewBag.SubCategories = new List<(string, List<string>)>
        {
            ("Đồ dùng học tập", new List<string>{ "Bút", "Thước", "Gôm", "Compa" }),
            ("Tập - vở", new List<string>{ "Vở kẻ ngang", "Vở kẻ ô", "Sổ tay", "Giấy vẽ" }),
            ("Phụ kiện", new List<string>{ "Balo", "Hộp bút", "Bìa kẹp", "Sticker" })
        };
        var sanPhamNoiBat = _context.SanPhams
            .Where(sp => sp.MaDm == 3) 
            .OrderByDescending(sp => sp.GiamGia)
            .Take(4)
            .ToList();

        ViewBag.SanPhamNoiBat = sanPhamNoiBat;

       
        return View("DanhMuc");
    }

    // Đồ chơi
    public IActionResult DoChoiTreEm()
    {
        ViewBag.Active = "DoChoi";
        ViewBag.TitleDanhMuc = "🧸 Đồ Chơi";

        ViewBag.SubCategories = new List<(string, List<string>)>
        {
            ("Đồ chơi giáo dục", new List<string>{ "Lego", "Ghép hình", "Đồ chơi khoa học", "Đồ chơi logic" }),
            ("Đồ chơi vận động", new List<string>{ "Xe đồ chơi", "Banh", "Cầu trượt mini", "Thú nhún" }),
            ("Đồ chơi sáng tạo", new List<string>{ "Tô màu", "Nặn đất sét", "Xếp khối", "Thủ công" })
        };
        var sanPhamNoiBat = _context.SanPhams
            .Where(sp => sp.MaDm == 4) 
            .OrderByDescending(sp => sp.GiamGia)
            .Take(4)
            .ToList();

        ViewBag.SanPhamNoiBat = sanPhamNoiBat;

       
        return View("DanhMuc");
    }
}
