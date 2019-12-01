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
    public class IndexModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public IndexModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Purchases> Purchases { get;set; }

        public async Task OnGetAsync()
        {
            Purchases = await _context.Purchases
                .Include(p => p.Performance)
                .Include(p => p.PurchaseMethod)
                .Include(p => p.User).ToListAsync();
        }
    }
}
