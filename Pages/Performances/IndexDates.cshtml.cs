using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;
using TicketReservationSystem.Authorization;


namespace TicketReservationSystem.Pages.Performances
{
    public class IndexDatesModel : BasePageModel
    {
        public IndexDatesModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IList<Models.PerformanceDates> PerformanceDates { get;set; }
        public string PerformanceName { get;set;}
        public string id { get; set; } //TODO: Rename

        public async Task<IActionResult> OnGetAsync(string id)
        {   
            if (id == null) {
                return Redirect("/Error");
            }

            PerformanceName = Context.Performances
                .First(p => p.Id == id).Name;
            PerformanceDates = await Context.PerformanceDates
                .Where(p => p.PerformanceId == id)
                .ToListAsync();
            this.id = id;

            return Page();
        }
    }
}
