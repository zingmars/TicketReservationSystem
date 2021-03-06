﻿using System;
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
    public class IndexModel : BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IList<Models.Purchases> Purchases { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {   
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator) && !User.IsInRole(Constants.Cashier))  {
                return NotFound();
            }

            Purchases = await Context.Purchases
                .Include(p => p.Performance)
                .Include(p => p.PerformanceDate)
                .Include(p => p.PurchaseMethod)
                .Include(p => p.User).ToListAsync();

            return Page();
        }
    }
}
