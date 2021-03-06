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
    public class ListModel : BasePageModel
    {
        public ListModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IList<Models.Purchases> Purchases { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {   
            if (!User.Identity.IsAuthenticated) {
                return Redirect("/Identity/Account/Login?returnUrl=/Purchases/List");
            }

            Purchases = await Context.Purchases
            .Where(p => p.UserId == UserManager.GetUserId(User))
            .Include(p => p.Performance)
            .Include(p => p.PerformanceDate)
            .Include(p => p.PurchaseMethod)
            .ToListAsync();

            return Page();
        }
    }
}
