using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class DetailsModel : BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public Models.Performances Performances { get; set; }
        public List<Models.Categories> Categories { get; set; } = new List<Models.Categories>();
        public string returnToPage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Performances = await Context.Performances
                .Include(p => p.Theatre)
                .Include(p => p.PerformanceDates)
                .Include(p => p.PerformanceCategories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Performances == null)
            {
                return NotFound();
            }                        

            foreach (var item in Performances.PerformanceCategories)
            {
                Categories.Add(Context.Categories.FirstOrDefault(c => c.Id == item.CategoryId));
            }            

            this.returnToPage = returnToPage;
            return Page();
        }
    }
}
