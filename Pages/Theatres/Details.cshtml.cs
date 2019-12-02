using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Theatres
{
    public class DetailsModel : BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public Models.Theatres Theatres { get; set; }
        public IList<Models.Performances> Performances { get;set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Theatres = await Context.Theatres
                .FirstOrDefaultAsync(m => m.Id == id);
            Performances = await Context.Performances
                .Where(p => p.TheatreId == Theatres.Id)
                .ToListAsync();

            if (Theatres == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
