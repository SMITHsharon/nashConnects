using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashConnects.Models
{
    public class Event
    {
        
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(50)]
        public string EventName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public virtual List<Freelancer> Freelancers { get; set; }
        
    }
}