using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult DangNhap()
        {
            return View();
        }

        public IActionResult DangKy()
        {
            return View();
        }
    }
}
