using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Authorization;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class DeleteCategoryModel : BasePageModel
    {
        public DeleteCategoryModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public PerformanceCategories PerformanceCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(string performanceId, string categoryId)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator))
            {
                return NotFound();
            }

            if (performanceId == null)
            {
                return NotFound();
            }

            if (categoryId == null)
            {
                return NotFound();
            }

            PerformanceCategories = await Context.PerformanceCategories
                .Include(p => p.Category)
                .Include(p => p.Performance).FirstOrDefaultAsync(m => m.PerformanceId == performanceId && m.CategoryId == categoryId);

            if (PerformanceCategories == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string performanceId, string categoryId)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator))
            {
                return NotFound();
            }

            if (performanceId == null)
            {
                return NotFound();
            }

            if (categoryId == null)
            {
                return NotFound();
            }

            PerformanceCategories = await Context.PerformanceCategories.FindAsync(performanceId, categoryId);

            if (PerformanceCategories != null)
            {
                Context.PerformanceCategories.Remove(PerformanceCategories);
                await Context.SaveChangesAsync();
            }

            return Redirect("./IndexCategories?id=" + PerformanceCategories.PerformanceId);
        }
    }
}
