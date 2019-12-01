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

namespace TicketReservationSystem.Pages.Theatres
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Models.Theatres Theatres { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Theatres = await Context.Theatres.FirstOrDefaultAsync(m => m.Id == id);

            if (Theatres == null)
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

            Theatres = await Context.Theatres.FindAsync(id);

            if (Theatres != null)
            {
                Context.Theatres.Remove(Theatres);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
