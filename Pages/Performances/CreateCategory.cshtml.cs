using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Authorization;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Performances
{
    public class CreateCategory : BasePageModel
    {
        public CreateCategory(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public async Task<IActionResult> OnGet(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            PerformanceId = id;

            PerformanceName = Context.Performances.FirstOrDefault(p => p.Id == id).Name;

            var performanceCategoryQuery = from pc in Context.PerformanceCategories
                                           where pc.PerformanceId == id
                                           select pc.Category;

            var result = await Context.Categories.Except(performanceCategoryQuery).ToListAsync();

            ViewData["CategoryId"] = new SelectList(result, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public PerformanceCategories PerformanceCategories { get; set; }

        public string PerformanceId { get; set; }

        public string PerformanceName { get; set; }        

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!User.IsInRole(Constants.Bookkeeper) && !User.IsInRole(Constants.Administrator))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            PerformanceCategories.PerformanceId = id;

            Context.PerformanceCategories.Add(PerformanceCategories);
            await Context.SaveChangesAsync();

            return Redirect("./IndexCategories?id=" + PerformanceCategories.PerformanceId);
        }
    }
}
