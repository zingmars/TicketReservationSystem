using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Authorization;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class EditDateModel : BasePageModel
    {
        public EditDateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
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

            PerformanceDate = await Context.PerformanceDates.FirstOrDefaultAsync(m => m.Id == id);

            if (PerformanceDate == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Attach(PerformanceDate).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformancesExists(PerformanceDate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect("./IndexDates?id="+PerformanceDate.PerformanceId);
        }

        private bool PerformancesExists(string id)
        {
            return Context.PerformanceDates.Any(e => e.Id == id);
        }
    }
}
