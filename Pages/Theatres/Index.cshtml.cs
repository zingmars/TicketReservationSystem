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
    public class IndexModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public IndexModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Theatres> Theatres { get;set; }

        public async Task OnGetAsync()
        {
            Theatres = await _context.Theatres.ToListAsync();
        }
    }
}
