using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Models;

[Table("SanPham")]
public partial class SanPham
{
    [Key]
    [Column("MaSP")]
    public int MaSp { get; set; }

    [Column("TenSP")]
    [StringLength(255)]
    public string TenSp { get; set; } = null!;

    [StringLength(100)]
    public string? TacGia { get; set; }

    [StringLength(100)]
    public string? NhaXuatBan { get; set; }

    [Column(TypeName = "decimal(12, 0)")]
    public decimal GiaGoc { get; set; }

    [Column(TypeName = "decimal(12, 0)")]
    public decimal GiaBan { get; set; }

    public int? GiamGia { get; set; }

    public int? SoDanhGia { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? SoSao { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    [StringLength(500)]
    public string? MoTa { get; set; }
    public bool? NoiBat { get; set; }      // ✅ thêm
    public bool? FlashSale { get; set; }   // ✅ thêm

    [Column("MaDM")]
    public int? MaDm { get; set; }

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();
    [ForeignKey("MaDm")]
    [InverseProperty("SanPhams")]
    public virtual DanhMuc? MaDmNavigation { get; set; }
}
