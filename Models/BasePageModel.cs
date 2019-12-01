using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketReservationSystem.Data;

namespace TicketReservationSystem.Models
{
    public class BasePageModel : PageModel
    {
        public ApplicationDbContext Context { get; }
        public IAuthorizationService AuthorizationService { get; }
        public UserManager<IdentityUser> UserManager { get; }

        public BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}
