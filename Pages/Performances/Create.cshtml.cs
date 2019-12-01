using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
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
        ViewData["TheatreId"] = new SelectList(_context.Theatres, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Models.Performances Performances { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Performances.Id = Guid.NewGuid().ToString();
            Performances.NormalizedName = Performances.Name.ToUpper();
            Performances.ConcurrencyStamp = Guid.NewGuid().ToString();

            _context.Performances.Add(Performances);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
