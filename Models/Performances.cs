using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketReservationSystem.Models
{
    public partial class Performances
    {
        public Performances()
        {
            PerformanceCategories = new HashSet<PerformanceCategories>();
            PerformanceDates = new HashSet<PerformanceDates>();
            Purchases = new HashSet<Purchases>();
        }

        [Key]
        public string Id { get; set; }
        public string TheatreId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public long? Price { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual Theatres Theatre { get; set; }
        public virtual ICollection<PerformanceCategories> PerformanceCategories { get; set; }
        public virtual ICollection<PerformanceDates> PerformanceDates { get; set; }
        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
