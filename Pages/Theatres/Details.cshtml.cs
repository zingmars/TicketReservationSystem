using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Theatres
{
    public class DetailsModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public DetailsModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
