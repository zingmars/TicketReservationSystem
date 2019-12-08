using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

            var catId1 = Guid.NewGuid().ToString();
            var catId2 = Guid.NewGuid().ToString();
            var catId3 = Guid.NewGuid().ToString();
            var catId4 = Guid.NewGuid().ToString();

            context.Categories.AddRange(
                new Categories
                {
                    Id = catId1,
                    Name = "Pasaka",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "PASAKA",
                    Description = ""
                },
                new Categories
                {
                    Id = catId2,
                    Name = "Koncerts",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "KONCERTS",
                    Description = ""
                },
                new Categories
                {
                    Id = catId3,
                    Name = "Komēdija",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "KOMEDIJA",
                    Description = ""
                },
                new Categories
                {
                    Id = catId4,
                    Name = "Drāma",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "DRAMA",
                    Description = ""
                }
            );

            context.PurchaseMethods.AddRange(
                new PurchaseMethods
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Description = "",
                    Name = "Method 1",
                    NormalizedName = "METHOD 1"
                },
                new PurchaseMethods
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Description = "",
                    Name = "Method 2",
                    NormalizedName = "METHOD 2"
                },
                new PurchaseMethods
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Description = "",
                    Name = "Method 3",
                    NormalizedName = "METHOD 3"
                }
            );

            var thId1 = Guid.NewGuid().ToString();

            var bh1 = new List<BusinessHours> {
                new BusinessHours
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Day = 1,
                    Begins = "10",
                    Ends = "19",
                    TheatreId = thId1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Day = 2,
                    Begins = "10",
                    Ends = "19",
                    TheatreId = thId1
                },
                new BusinessHours
                {
                    Day = 3,
                    Begins = "10",
                    Ends = "19",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    TheatreId = thId1
                },
                new BusinessHours
                {
                    Day = 4,
                    Begins = "10",
                    Ends = "19",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    TheatreId = thId1
                },
                new BusinessHours
                {
                    Day = 5,
                    Begins = "10",
                    Ends = "19",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    TheatreId = thId1
                },
                new BusinessHours
                {
                    Day = 6,
                    Begins = "11",
                    Ends = "18",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    TheatreId = thId1
                },
                new BusinessHours
                {
                    Day = 7,
                    Begins = "11",
                    Ends = "18",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    TheatreId = thId1
                }
            };

            context.Theatres.AddRange(
                new Theatres
                {
                    Id = thId1,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Latvijas Nacionālais teātris",
                    NormalizedName = "LATVIJAS NACIONALAIS TEATRIS",
                    Address = "Kronvalda bulvāris 2, Centra rajons, Rīga, LV-1010",
                    Seats = 750,
                    Description = "Latvijas Nacionālais teātris ir teātris Rīgā, Latvijā, kas tika atklāts 1919.g. 30.novembrī. " +
                    "Teātra ēka celta 1902. gadā kā Rīgas 2. pilsētas teātris par Rīgas pilsētas valdes līdzekļiem. " +
                    "Latvijas Nacionālā teātra uzturēšanā piedalās Latvijas valsts, Latvijas Kultūras fonds un Rīgas pilsēta.",                    
                }            
            );

            context.BusinessHours.AddRange(bh1);

            var pfId1 = Guid.NewGuid().ToString();

            context.Performances.AddRange(
                new Performances
                {
                    Id = pfId1,                    
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Baltā pasaka",
                    NormalizedName = "BALTA PASAKA",
                    Description = "Ja nu dabas apstākļi mūs decembrī vēl nelutinās ar baltu sniegu un vieglu salu Rīgas ielās, " +
                    "tad Nacionālā teātra Ziemassvētku koncertā tā visa būs atliku likām – balts mirdzums uz skatuves, aktieru tērpos, " +
                    "viņu runātajos tekstos un dziesmās.",
                    Price = 25.00,
                    TheatreId = thId1
                }
            );

            context.PerformanceCategories.AddRange(
                new PerformanceCategories
                {
                    CategoryId = catId1,
                    PerformanceId = pfId1
                },
                new PerformanceCategories
                {
                    CategoryId = catId2,
                    PerformanceId = pfId1
                }
            );

            context.PerformanceDates.AddRange(
                new PerformanceDates
                {
                    Id = Guid.NewGuid().ToString(),
                    PerformanceId = pfId1,
                    Begins = DateTime.Parse("02.01.2019 19:00"),
                    Ends = DateTime.Parse("02.01.2019 22:00"),
                }
            );

            context.SaveChanges();
        }
    }
}
