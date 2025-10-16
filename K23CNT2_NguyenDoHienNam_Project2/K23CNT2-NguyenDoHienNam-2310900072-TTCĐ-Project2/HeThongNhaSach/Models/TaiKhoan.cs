using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Models;

[Table("TaiKhoan")]
[Index("Email", Name = "UQ__TaiKhoan__A9D10534BB2ACE8C", IsUnique = true)]
public partial class TaiKhoan
{
    [Key]
    [Column("MaTK")]
    public int MaTk { get; set; }

    [StringLength(100)]
    public string HoTen { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string MatKhau { get; set; } = null!;

    [StringLength(255)]
    public string? DiaChi { get; set; }

    [StringLength(20)]
    public string? SoDienThoai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [StringLength(20)]
    public string? VaiTro { get; set; }

    [InverseProperty("MaTkNavigation")]
    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    [InverseProperty("MaTkNavigation")]
    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
}
