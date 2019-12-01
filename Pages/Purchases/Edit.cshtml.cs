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

namespace TicketReservationSystem.Pages.Purchases
{
    public class EditModel : BasePageModel
    {
        public EditModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
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

            ViewData["PerformanceId"] = new SelectList(Context.Performances, "Id", "Name");
            ViewData["PurchaseMethodId"] = new SelectList(Context.PurchaseMethods, "Id", "Name");
            ViewData["UserId"] = new SelectList(Context.AspNetUsers, "Id", "UserName");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator) && !User.IsInRole(Constants.Cashier))  {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Purchases.ConcurrencyStamp = Guid.NewGuid().ToString();
            Purchases.Edited = DateTime.Now;
            Context.Attach(Purchases).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasesExists(Purchases.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PurchasesExists(string id)
        {
            return Context.Purchases.Any(e => e.Id == id);
        }
    }
}
