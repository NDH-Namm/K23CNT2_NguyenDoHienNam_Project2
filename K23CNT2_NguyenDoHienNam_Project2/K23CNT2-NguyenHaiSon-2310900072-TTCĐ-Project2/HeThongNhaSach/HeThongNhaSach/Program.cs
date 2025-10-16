using Microsoft.EntityFrameworkCore;
using HeThongNhaSach.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Thêm DbContext (kết nối CSDL)
builder.Services.AddDbContext<NhaSachContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NhaSachDb")));

// ✅ Thêm Session (chỉ cần 1 lần thôi)
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Kích hoạt Session (chỉ 1 lần thôi)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
