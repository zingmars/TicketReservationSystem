using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class BusinessHours
    {
        public string Id { get; set; }
        public string TheatreId { get; set; }
        public long Day { get; set; }
        public string Begins { get; set; }
        public string Ends { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual Theatres Theatre { get; set; }
    }
}
