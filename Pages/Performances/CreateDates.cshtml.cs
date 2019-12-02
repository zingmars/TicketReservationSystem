using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReservationSystem.Authorization;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class CreateDatesModel : BasePageModel
    {
        public CreateDatesModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }

            if (id == null) {
                return NotFound();
            }
            this.id = id;

            return Page();
        }

        [BindProperty]
        public Models.PerformanceDates PerformanceDates { get; set; }
        public string id { get; set; } //TODO: Rename

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

            PerformanceDates.Id = Guid.NewGuid().ToString();
            PerformanceDates.PerformanceId = id;

            Context.PerformanceDates.Add(PerformanceDates);
            await Context.SaveChangesAsync();

            return RedirectToPage("./IndexDates");
        }
    }
}
