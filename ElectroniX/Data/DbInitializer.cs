using ElectroniX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectroniX.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Apply migrations
            await context.Database.MigrateAsync();

            // ========================
            // Seed Roles
            // ========================
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // ========================
            // Seed Admin User
            // ========================
            var adminEmail = "admin@electronix.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // ========================
            // Seed Products
            // ========================
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Products
                    {
                        Name = "iPhone 14",
                        Description = "Apple smartphone",
                        DeviceType = Models.Enums.DeviceType.Smartphone,
                        Price = 18000,
                        IsUsed = false,
                        Grade = "A",
                        WarrantyMonths = 12,
                        Stock = 10,
                        RecommendedUsage = Models.Enums.UsageType.Work,
                        ImageUrl = "/images/iphone14.png"
                    },
                    new Products
                    {
                        Name = "Dell XPS 15",
                        Description = "Laptop for work/study",
                        DeviceType = Models.Enums.DeviceType.Laptop,
                        Price = 25000,
                        IsUsed = false,
                        Grade = "A",
                        WarrantyMonths = 24,
                        Stock = 5,
                        RecommendedUsage = Models.Enums.UsageType.Study,
                        RecommendedCourse = Models.Enums.StudyCourse.Engineering,
                        ImageUrl = "/images/dellxps15.png"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
