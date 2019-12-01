using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class PurchaseMethods
    {
        public PurchaseMethods()
        {
            Purchases = new HashSet<Purchases>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
