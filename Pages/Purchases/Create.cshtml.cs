using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Purchases
{
    public class CreateModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public CreateModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PerformanceId"] = new SelectList(_context.Performances, "Id", "Name");
        ViewData["PurchaseMethodId"] = new SelectList(_context.PurchaseMethods, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
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

            _context.Purchases.Add(Purchases);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
