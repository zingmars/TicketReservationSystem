using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Theatres
{
    public class CreateModel : PageModel
    {
        private readonly TicketReservationSystem.Data.ApplicationDbContext _context;

        public CreateModel(TicketReservationSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Theatres Theatres { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Theatres.Id = Guid.NewGuid().ToString();
            Theatres.NormalizedName = Theatres.Name.ToUpper();
            Theatres.ConcurrencyStamp = Guid.NewGuid().ToString();

            _context.Theatres.Add(Theatres);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
