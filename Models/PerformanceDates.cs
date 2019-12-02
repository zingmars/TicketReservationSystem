using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class PerformanceDates
    {
        public string Id { get; set; }
        public string PerformanceId { get; set; }
        public DateTime Begins { get; set; }
        public DateTime Ends { get; set; }

        public virtual Performances Performance { get; set; }
        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
