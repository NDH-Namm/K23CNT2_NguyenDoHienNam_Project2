using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HeThongNhaSach.Models;

public partial class NhaSachContext : DbContext
{
    public NhaSachContext()
    {
    }

    public NhaSachContext(DbContextOptions<NhaSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }
    public virtual DbSet<GioHang> GioHangs { get; set; }
    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-SMN1035\\SQLEXPRESS05;Database=NhaSachTrucTuyen;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {
        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGh).HasName("PK__GioHang__...");
            entity.HasOne(d => d.MaTkNavigation)
                .WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__GioHang__MaTK__...");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaCtgh).HasName("PK__ChiTietGH__...");
            entity.HasOne(d => d.MaGhNavigation)
                .WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGh);

            entity.HasOne(d => d.MaSpNavigation)
                .WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSp);
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaCtdh).HasName("PK__ChiTietD__1E4E40F0FA9A5C98");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs).HasConstraintName("FK__ChiTietDon__MaDH__656C112C");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs).HasConstraintName("FK__ChiTietDon__MaSP__66603565");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMuc__2725866EF6F3D02E");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__272586612801CCE0");

            entity.Property(e => e.NgayDat).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue("Chờ xử lý");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.DonHangs).HasConstraintName("FK__DonHang__MaTK__60A75C0F");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1EDE862225");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.KhachHangs).HasConstraintName("FK__KhachHang__MaTK__6A30C649");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C88F41661");

            entity.Property(e => e.SoDanhGia).HasDefaultValue(0);
            entity.Property(e => e.SoSao).HasDefaultValue(0.0m);

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.SanPhams).HasConstraintName("FK__SanPham__MaDM__59FA5E80");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TaiKhoan__2725007062D737FC");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.VaiTro).HasDefaultValue("KhachHang");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
