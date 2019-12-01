using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
        ViewData["TheatreId"] = new SelectList(Context.Theatres, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Performances Performances { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Performances.Id = Guid.NewGuid().ToString();
            Performances.NormalizedName = Performances.Name.ToUpper();
            Performances.ConcurrencyStamp = Guid.NewGuid().ToString();

            Context.Performances.Add(Performances);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
