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

namespace TicketReservationSystem.Pages.Performances
{
    public class EditModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public EditModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Performances Performances { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Performances = await _context.Performances
                .Include(p => p.Theatre).FirstOrDefaultAsync(m => m.Id == id);

            if (Performances == null)
            {
                return NotFound();
            }
           ViewData["TheatreId"] = new SelectList(_context.Theatres, "Id", "Name");
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

            _context.Attach(Performances).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformancesExists(Performances.Id))
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

        private bool PerformancesExists(string id)
        {
            return _context.Performances.Any(e => e.Id == id);
        }
    }
}
