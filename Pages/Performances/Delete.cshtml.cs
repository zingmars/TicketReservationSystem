using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class DeleteModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public DeleteModel(TicketReservationSystem.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Performances = await _context.Performances.FindAsync(id);

            if (Performances != null)
            {
                _context.Performances.Remove(Performances);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
