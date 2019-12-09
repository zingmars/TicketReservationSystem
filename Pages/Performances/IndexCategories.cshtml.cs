using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class IndexCategoriesModel : BasePageModel
    {
        public IndexCategoriesModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IList<PerformanceCategories> PerformanceCategories { get; set; }
        public string PerformanceName { get; set; }
        public string PerformanceId { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
            {
                return Redirect("/Error");
            }

            PerformanceId = id;            

            PerformanceCategories = await Context.PerformanceCategories
                .Where(p => p.PerformanceId == id)
                .Include(p => p.Performance)
                .Include(p => p.Category).ToListAsync();

            PerformanceName = PerformanceCategories.FirstOrDefault().Performance.Name;

            return Page();
        }
    }
}