using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketReservationSystem.Models
{
    public partial class Theatres
    {
        public Theatres()
        {
            BusinessHours = new HashSet<BusinessHours>();
            Performances = new HashSet<Performances>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long Seats { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<BusinessHours> BusinessHours { get; set; }
        public virtual ICollection<Performances> Performances { get; set; }
    }
}
