using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashConnects.Models
{
    public class NonProfit : ApplicationUser
    {
        /*
        [Key]
        public int NonProfitId { get; set; }
        */

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string CalendarLink { get; set; }

        public virtual List<Event> Events { get; set; }
        public virtual List<Freelancer> FLRecommendations { get; set; }
        public virtual List<NonProfit> NPNPRecommndations { get; set; }
    }
}