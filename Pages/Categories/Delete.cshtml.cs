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

namespace TicketReservationSystem.Pages.Categories
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
        public Models.Categories Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Categories = await Context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (Categories == null)
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

            Categories = await Context.Categories.FindAsync(id);

            if (Categories != null)
            {
                Context.Categories.Remove(Categories);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
