using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Data;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Pages.Theatres
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

        public IList<Models.Theatres> Theatres { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }        
        public SelectList Performances { get; set; }
        [BindProperty(SupportsGet = true)]
        public string TheatrePerformance { get; set; }


        public async Task OnGetAsync()
        {
            IQueryable<IEnumerable<string>> performanceNameQuery = from t in Context.Theatres
                                                                            orderby t.Name
                                                                            select (from p in t.Performances 
                                                                                    select p.Name);
            var theatres = from t in Context.Theatres
                         select t;

            if (!string.IsNullOrEmpty(SearchString))
            {
                theatres = theatres.Where(t => t.Address.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(TheatrePerformance))
            {
                theatres = theatres.Where(t => t.Performances.Any(p => p.Name == TheatrePerformance));
            }

            List<string> result = new List<string>();

            await performanceNameQuery.Distinct().ForEachAsync(pn => result.AddRange(pn));            

            Performances = new SelectList(result.Distinct());

            Theatres = await theatres.ToListAsync();
        }
    }
}
