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
using TicketReservationSystem.Authorization;

namespace TicketReservationSystem.Pages.Performances
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IList<Models.Performances> Performances { get;set; }

        public async Task OnGetAsync()
        {   
            Performances = await Context.Performances
                .Include(p => p.Theatre).ToListAsync();

        }
    }
}
