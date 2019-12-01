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
    public class IndexModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public IndexModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Performances> Performances { get;set; }

        public async Task OnGetAsync()
        {
            Performances = await _context.Performances
                .Include(p => p.Theatre).ToListAsync();
        }
    }
}
