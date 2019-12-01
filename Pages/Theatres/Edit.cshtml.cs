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

namespace TicketReservationSystem.Pages.Theatres
{
    public class EditModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public EditModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Theatres Theatres { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Theatres = await _context.Theatres.FirstOrDefaultAsync(m => m.Id == id);

            if (Theatres == null)
            {
                return NotFound();
            }
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

            _context.Attach(Theatres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheatresExists(Theatres.Id))
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

        private bool TheatresExists(string id)
        {
            return _context.Theatres.Any(e => e.Id == id);
        }
    }
}
