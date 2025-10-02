using System.Numerics;
using Bloggie.Data;
using Bloggie.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;  // Add this using statement
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Add this using statement


namespace Bloggie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Get the connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("BloggieDbConnectionString");
            var connectionStringAuth = builder.Configuration.GetConnectionString("BloggieAuthDbConnectionString");

            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

            // Configure MySQL with Pomelo.EntityFrameworkCore.MySql
            builder.Services.AddDbContext<BloggieDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                // Alternatively, be explicit with the version:
                // options.UseMySql(connectionString, ServerVersion.Parse("8.0.32-mysql")); // Example MySQL 8.0.32

            });

            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseMySql(connectionStringAuth, ServerVersion.AutoDetect(connectionStringAuth));
                // Alternatively, be explicit with the version:
                // options.UseMySql(connectionString, ServerVersion.Parse("8.0.32-mysql")); // Example MySQL 8.0.32

            });

            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}