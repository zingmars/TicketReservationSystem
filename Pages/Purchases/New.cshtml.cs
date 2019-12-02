﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;
using System.Collections.Generic;

namespace TicketReservationSystem.Pages.Purchases
{
    public class NewModel : BasePageModel
    {
        public NewModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet(string PerformanceId)
        {
            if (!User.Identity.IsAuthenticated) {
                return Redirect("/Identity/Account/Login?returnUrl=/Purchases/New?PerformanceId="+PerformanceId);
            }

            if (PerformanceId == null) {
                return Redirect("/Error");
            }

            this.Purchases = new Models.Purchases();
            this.Purchases.PerformanceId = PerformanceId;
            this.Purchases.Performance = Context.Performances.First(x => x.Id == PerformanceId);

            ViewData["PurchaseMethodId"] = new SelectList(Context.PurchaseMethods, "Id", "Name");
            ViewData["PerformanceDateId"] = new SelectList(Context.PerformanceDates.Where(x => x.PerformanceId == PerformanceId).ToList(), "Id", "Begins");
            return Page();                                               
        }

        [BindProperty]
        public Models.Purchases Purchases { get; set; }
        public string PerformanceId { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated) {
                return Redirect("/Identity/Account/Login?returnUrl=/Purchases/New");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Purchases.Id = Guid.NewGuid().ToString();
            Purchases.UserId = UserManager.GetUserId(User);
            Purchases.ConcurrencyStamp = Guid.NewGuid().ToString();
            Purchases.Purchased = DateTime.Now;
            Purchases.Edited = DateTime.Now;
            Purchases.AmountPaid = Context.Performances.First(x => x.Id == Purchases.PerformanceId).Price;

            Context.Purchases.Add(Purchases);
            await Context.SaveChangesAsync();

            return Redirect("/Purchases/List");
        }
    }
}
