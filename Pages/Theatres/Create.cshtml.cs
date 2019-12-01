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

namespace TicketReservationSystem.Pages.Theatres
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
            return Page();
        }

        [BindProperty]
        public Models.Theatres Theatres { get; set; }

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

            Theatres.Id = Guid.NewGuid().ToString();
            Theatres.NormalizedName = Theatres.Name.ToUpper();
            Theatres.ConcurrencyStamp = Guid.NewGuid().ToString();

            Context.Theatres.Add(Theatres);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
