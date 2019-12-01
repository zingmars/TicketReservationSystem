using System;
using System.Collections.Generic;

namespace TicketReservationSystem.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            Purchases = new HashSet<Purchases>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public long EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public long PhoneNumberConfirmed { get; set; }
        public long TwoFactorEnabled { get; set; }
        public string LockoutEnd { get; set; }
        public long LockoutEnabled { get; set; }
        public long AccessFailedCount { get; set; }

        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
