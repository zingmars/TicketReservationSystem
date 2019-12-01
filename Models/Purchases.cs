using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class Purchases
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PurchaseMethodId { get; set; }
        public string PerformanceId { get; set; }
        public long? SeatId { get; set; }
        public long? AmountPaid { get; set; }
        public string Purchased { get; set; }

        public virtual Performances Performance { get; set; }
        public virtual PurchaseMethods PurchaseMethod { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
