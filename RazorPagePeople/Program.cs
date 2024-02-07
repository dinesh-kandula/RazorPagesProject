using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RazorPagePeople.Data;
using RazorPagePeople.Services;
using System.Security.Claims;

namespace RazorPagePeople
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.WebHost.UseStaticWebAssets();

            builder.Services.AddDbContext<PersonDatabaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection") 
                ?? throw new InvalidOperationException("Connection string 'PersonDatabaseContext' not found.")));

            builder.Services.AddTransient<IFileService, FileService>();
            builder.Services.AddRazorPages();
            // Authentication
            builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            //adding policy for authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ADMIN", 
                    policy => policy.RequireClaim(ClaimTypes.Role, "ADMIN"));
                
                options.AddPolicy("HERO ADMIN", 
                    policy => policy.RequireClaim(ClaimTypes.Role, "HERO", "ADMIN"));
                
                options.AddPolicy("HERO", policy => policy
                    .RequireClaim(ClaimTypes.Role, builder.Configuration.GetValue<string>("Period:ProbitionPeriod"))
                );
                
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
