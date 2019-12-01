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

namespace TicketReservationSystem.Pages.Purchases
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
        public Models.Purchases Purchases { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator) && !User.IsInRole(Constants.Cashier))  {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Purchases = await Context.Purchases
                .Include(p => p.Performance)
                .Include(p => p.PurchaseMethod)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Purchases == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator) && !User.IsInRole(Constants.Cashier))  {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Purchases = await Context.Purchases.FindAsync(id);

            if (Purchases != null)
            {
                Context.Purchases.Remove(Purchases);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
