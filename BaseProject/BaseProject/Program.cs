using BaseProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDBContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            identityDbContext.Database.Migrate();

            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(new AppUser() { UserName = "User1", Email = "user1@gmail.com" }, "Sing123*+").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "User2", Email = "user2@gmail.com" }, "Sing123*+").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "User3", Email = "user3@gmail.com" }, "Sing123*+").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "User4", Email = "user4@gmail.com" }, "Sing123*+").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "User5", Email = "user5@gmail.com" }, "Sing123*+").Wait();

            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
