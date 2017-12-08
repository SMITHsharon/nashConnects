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

        [StringLength(50)]
        public string CalendarLink { get; set; }

        public List<Event> Events { get; set; }
    }
}