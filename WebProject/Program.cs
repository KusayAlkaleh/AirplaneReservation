using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Controllers;
using Microsoft.AspNetCore.Identity;
using WebProject.Models.Domain;
using WebProject.Repositories.Abstract;
using WebProject.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// connect to database using SqlServer Packege
builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("DefaultConnection")));

//Adding identity for project
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DemoDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(option => option.LoginPath = "/UserAuthentaction/Login");

// Add interface classes
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Using Auth
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
