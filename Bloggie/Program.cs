using Bloggie.Data;
using Bloggie.Repositories;
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}