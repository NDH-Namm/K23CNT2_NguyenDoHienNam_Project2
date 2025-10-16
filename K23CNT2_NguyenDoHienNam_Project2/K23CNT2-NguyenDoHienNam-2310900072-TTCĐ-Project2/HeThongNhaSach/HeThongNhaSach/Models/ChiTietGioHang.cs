using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongNhaSach.Models;

[Table("ChiTietGioHang")]
public partial class ChiTietGioHang
{
    [Key]
    public int MaCtgh { get; set; }

    [Column("MaGH")]
    public int MaGh { get; set; }

    [Column("MaSP")]
    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(12, 0)")]
    public decimal DonGia { get; set; }

    [ForeignKey("MaGh")]
    public virtual GioHang MaGhNavigation { get; set; } = null!;

    [ForeignKey("MaSp")]
    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
