using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashConnects.Models
{
    public class NonProfit : ApplicationUser
    {
        [Key]
        public int NonProfitId { get; set; }

        [Required]
        [StringLength(50)]
        public string WebsiteURL { get; set; }

        [StringLength(50)]
        public string CalendarLink { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        public int NPRecommendCount { get; set; }
        public bool Active { get; set; }

        public virtual List<Freelancer> FLRecommendations { get; set; }
        public virtual List<NonProfit> NPRecommendations { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}