using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class PerformanceCategories
    {
        public string PerformanceId { get; set; }
        public string CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Performances Performance { get; set; }
    }
}
