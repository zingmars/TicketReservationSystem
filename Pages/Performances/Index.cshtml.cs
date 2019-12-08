using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketReservationSystem.Pages.Performances
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) : base(context, authorizationService, userManager)
        {
        }

        public IList<Models.Performances> Performances { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PerformanceCategory { get; set; }


        public async Task OnGetAsync()
        {
            IQueryable<IEnumerable<string>> categoryQuery = from p in Context.Performances
                                                                   orderby p.Name
                                                                   select (from c in p.PerformanceCategories
                                                                           select c.Category.Name);
            var performances = Context.Performances
                .Include(p => p.PerformanceDates)
                .Include(p => p.Theatre).AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                performances = performances.Where(t => t.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(PerformanceCategory))
            {
                performances = performances.Where(t => t.PerformanceCategories.Any(p => p.Category.Name == PerformanceCategory));
            }

            List<string> result = new List<string>();

            await categoryQuery.Distinct().ForEachAsync(c => result.AddRange(c));

            Categories = new SelectList(result.Distinct());

            Performances = await performances.ToListAsync();
        }
    }
}
