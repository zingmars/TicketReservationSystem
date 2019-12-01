using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketReservationSystem.Authorization;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@admin.com");
                await EnsureRole(serviceProvider, adminID, Constants.Administrator);
                
                var bookkeeperID = await EnsureUser(serviceProvider, testUserPw, "bookkeeper@bookkeeper.com");
                await EnsureRole(serviceProvider, bookkeeperID, Constants.Bookkeeper);

                var cashierID = await EnsureUser(serviceProvider, testUserPw, "cashier@cashier.com");
                await EnsureRole(serviceProvider, cashierID, Constants.Cashier);

                var userID = await EnsureUser(serviceProvider, testUserPw, "user@user.com");
                await EnsureRole(serviceProvider, userID, Constants.User);

                SeedDB(context, adminID, bookkeeperID, cashierID, userID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID, string bookkeeperID, string cashierID, string userID)
        {
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            context.Categories.AddRange(
                new Categories
                {
                    Name = "Musical",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "MUSICAL",
                    Description = ""
                },
                new Categories
                {
                    Name = "Romance",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "ROMANCE",
                    Description = ""
                },
                new Categories
                {
                    Name = "Comedy",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "COMEDY",
                    Description = ""
                },
                new Categories
                {
                    Name = "Tragedy",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "TRAGEDY",
                    Description = ""
                },
                new Categories
                {
                    Name = "Horror",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "HORROR",
                    Description = ""
                },
                new Categories
                {
                    Name = "Fantasy",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "FANTASY",
                    Description = ""
                }
            );

            context.SaveChanges();
        }
    }
}
