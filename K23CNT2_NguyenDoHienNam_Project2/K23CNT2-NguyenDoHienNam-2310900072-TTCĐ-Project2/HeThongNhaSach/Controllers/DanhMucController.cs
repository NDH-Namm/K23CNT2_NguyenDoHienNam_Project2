using Microsoft.AspNetCore.Mvc;

public class DanhMucController : Controller
{

    public IActionResult SachTrongNuoc()
    {
        ViewBag.Active = "TrongNuoc";
        ViewBag.TitleDanhMuc = "📚 Sách Trong Nước";

        ViewBag.SubCategories = new List<(string, List<string>)>
        {
            ("Văn học", new List<string>{ "Tiểu thuyết", "Truyện ngắn - Tản văn", "Ngôn tình", "Light Novel" }),
            ("Kinh tế", new List<string>{ "Quản trị", "Marketing", "Khởi nghiệp", "Phân tích kinh tế" }),
            ("Kỹ năng sống", new List<string>{ "Tâm lý", "Rèn luyện bản thân", "Kỹ năng mềm", "Tuổi mới lớn" })
        };

        return View("Index");
    }
    public IActionResult DanhMuc()
    {
        ViewBag.TitleDanhMuc = "Danh mục sản phẩm";
        ViewBag.Active = "TrongNuoc"; // gán mặc định menu active nếu muốn

        ViewBag.SubCategories = new List<(string, List<string>)>
        {
            ("📚 Văn học", new List<string>{ "Tiểu thuyết", "Truyện ngắn", "Thơ" }),
            ("🌍 Ngoại ngữ", new List<string>{ "Tiếng Anh", "Tiếng Nhật", "Tiếng Hàn" }),
            ("✏️ Dụng cụ", new List<string>{ "Bút", "Vở", "Thước kẻ" })
        };

        return View();
    }


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

        return View("Index");
    }

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

        return View("Index");
    }

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

        return View("Index");
    }
}
