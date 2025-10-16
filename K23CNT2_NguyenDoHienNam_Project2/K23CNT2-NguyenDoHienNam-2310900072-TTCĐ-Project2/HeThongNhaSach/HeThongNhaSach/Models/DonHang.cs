using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongNhaSach.Models;

[Table("DonHang")]
public partial class DonHang
{
    [Key]
    [Column("MaDH")]
    public int MaDh { get; set; }

    [Column("MaTK")]
    public int MaTk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayDat { get; set; }

    [Column(TypeName = "decimal(12, 0)")]
    public decimal? TongTien { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    public int? MaPttt { get; set; }
    public int? MaPtvc { get; set; }
    public int? MaKm { get; set; }

    // Liên kết với Tài Khoản
    [ForeignKey("MaTk")]
    public virtual TaiKhoan MaTkNavigation { get; set; } = null!;

    // Liên kết chi tiết đơn hàng
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
