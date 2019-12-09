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
    public class DeleteDateModel : BasePageModel
    {
        public DeleteDateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Models.PerformanceDates PerformanceDate { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            PerformanceDate = await Context.PerformanceDates
                .Include(p => p.Performance)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (PerformanceDate == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            PerformanceDate = await Context.PerformanceDates.FindAsync(id);

            if (PerformanceDate != null)
            {
                Context.PerformanceDates.Remove(PerformanceDate);
                await Context.SaveChangesAsync();
            }

            return Redirect("./IndexDates?id="+PerformanceDate.PerformanceId);
        }
    }
}
