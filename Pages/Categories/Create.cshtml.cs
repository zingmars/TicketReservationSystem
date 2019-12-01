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

namespace TicketReservationSystem.Pages.Categories
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
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator)) {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(Context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Categories Categories { get; set; }

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

            Categories.Id = Guid.NewGuid().ToString();
            Categories.NormalizedName = Categories.Name.ToUpper();
            Categories.ConcurrencyStamp = Guid.NewGuid().ToString();

            Context.Categories.Add(Categories);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
