using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongNhaSach.Models;

[Table("GioHang")]
public partial class GioHang
{
    [Key]
    [Column("MaGH")]
    public int MaGh { get; set; }

    [Column("MaTK")]
    public int MaTk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("MaTk")]
    public virtual TaiKhoan MaTkNavigation { get; set; } = null!;

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();
}
