using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class PerformanceDates
    {
        public string Id { get; set; }
        public string PerformanceId { get; set; }
        public string Date { get; set; }

        public virtual Performances Performance { get; set; }
    }
}
