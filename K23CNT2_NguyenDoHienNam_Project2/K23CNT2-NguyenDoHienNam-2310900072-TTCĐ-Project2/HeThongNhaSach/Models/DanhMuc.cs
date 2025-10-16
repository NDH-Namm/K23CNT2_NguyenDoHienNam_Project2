using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Models;

[Table("DanhMuc")]
public partial class DanhMuc
{
    [Key]
    [Column("MaDM")]
    public int MaDm { get; set; }

    [Column("TenDM")]
    [StringLength(100)]
    public string TenDm { get; set; } = null!;

    [InverseProperty("MaDmNavigation")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
