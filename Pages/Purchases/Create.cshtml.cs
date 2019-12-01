﻿using System;
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

namespace TicketReservationSystem.Pages.Purchases
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
        ViewData["PerformanceId"] = new SelectList(Context.Performances, "Id", "Name");
        ViewData["PurchaseMethodId"] = new SelectList(Context.PurchaseMethods, "Id", "Name");
        ViewData["UserId"] = new SelectList(Context.AspNetUsers, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Models.Purchases Purchases { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Purchases.Add(Purchases);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}