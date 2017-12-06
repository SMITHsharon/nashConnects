using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashConnects.Models
{
    public class Freelancer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int FLId { get; set; }

        [Required]
        [StringLength(50)]
        public string WebsiteURL { get; set; }

        [Required]
        [StringLength(300)]
        public string Bio { get; set; }

        public bool Newsletter { get; set; }
        public bool Public { get; set; }
        public int RecommondCount { get; set; }
        public bool Active { get; set; }

        public virtual List<Freelancer> FLRecommendations { get; set; }
        public virtual List<NonProfit> NPRecommendations { get; set; }
        public virtual List<Event> RegisteredEvents { get; set; }
        public virtual ApplicationUser Users { get; set; }
    }
}