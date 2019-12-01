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
    public class DetailsModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public DetailsModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
