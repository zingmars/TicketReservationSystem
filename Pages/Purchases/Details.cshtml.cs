using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Purchases
{
    public class DetailsModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public DetailsModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
