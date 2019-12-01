using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Purchases
{
    public class EditModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public EditModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Purchases Purchases { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Purchases = await _context.Purchases
                .Include(p => p.Performance)
                .Include(p => p.PurchaseMethod)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Purchases == null)
            {
                return NotFound();
            }
           ViewData["PerformanceId"] = new SelectList(_context.Performances, "Id", "Id");
           ViewData["PurchaseMethodId"] = new SelectList(_context.PurchaseMethods, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Purchases).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasesExists(Purchases.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PurchasesExists(string id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
