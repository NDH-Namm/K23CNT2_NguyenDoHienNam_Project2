using System.Collections.Generic;
using HeThongNhaSach.Models;

namespace HeThongNhaSach.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<SanPham> SanPhamNoiBat { get; set; }
        public List<SanPham> FlashSale { get; set; }
        public List<SanPham> HalloweenToys { get; set; }

    }
}
