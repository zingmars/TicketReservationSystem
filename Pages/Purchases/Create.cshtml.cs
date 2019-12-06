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

namespace TicketReservationSystem.Pages.Purchases
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator) && !User.IsInRole(Constants.Cashier))  {
                return NotFound();
            }

            ViewData["PerformanceId"] = new SelectList(Context.Performances, "Id", "Name");
            ViewData["PurchaseMethodId"] = new SelectList(Context.PurchaseMethods, "Id", "Name");
            // TODO: Dynamically filter dates as you change performances.
            ViewData["PerformanceDateId"] = new SelectList(Context.PerformanceDates, "Id", "Begins");
            ViewData["UserId"] = new SelectList(Context.AspNetUsers, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Models.Purchases Purchases { get; set; }

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
            
            Purchases.PerformanceDate = Context.PerformanceDates.First(x => x.Id ==  Purchases.PerformanceDate.Id);
            if (Purchases.PerformanceDate.PerformanceId != Purchases.PerformanceId) {
                return Page();
            }

            Purchases.Id = Guid.NewGuid().ToString();
            Purchases.ConcurrencyStamp = Guid.NewGuid().ToString();
            Purchases.Purchased = DateTime.Now;
            Purchases.Edited = DateTime.Now;

            Context.Purchases.Add(Purchases);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
