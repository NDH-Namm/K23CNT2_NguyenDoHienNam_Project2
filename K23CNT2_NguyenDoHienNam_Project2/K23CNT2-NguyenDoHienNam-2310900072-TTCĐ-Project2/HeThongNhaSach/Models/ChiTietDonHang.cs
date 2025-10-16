using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Models;

[Table("ChiTietDonHang")]
public partial class ChiTietDonHang
{
    [Key]
    [Column("MaCTDH")]
    public int MaCtdh { get; set; }

    [Column("MaDH")]
    public int? MaDh { get; set; }

    [Column("MaSP")]
    public int? MaSp { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(12, 0)")]
    public decimal DonGia { get; set; }

    [ForeignKey("MaDh")]
    [InverseProperty("ChiTietDonHangs")]
    public virtual DonHang? MaDhNavigation { get; set; }

    [ForeignKey("MaSp")]
    [InverseProperty("ChiTietDonHangs")]
    public virtual SanPham? MaSpNavigation { get; set; }
}
