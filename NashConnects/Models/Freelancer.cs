using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashConnects.Models
{
    public class Freelancer : ApplicationUser
    {
        /*
        [Key]
        public int FreelancerId { get; set; }
        */

        public bool Newsletter { get; set; }

        public bool PublicReveal { get; set; }

        public FLCategory? Category { get; set; }

        public virtual List<Freelancer> FLFLRecommendations { get; set; }
        public virtual List<NonProfit> NPRecommendations { get; set; }
        public virtual List<Event> RegEvents { get; set; }

        public enum FLCategory
        {
            Artist,
            Florist,
            PersonalTrainer,
            Photographer,
            RealEstateAgent,
            SoftwareDeveloper,
            Therapist,
            Writer
        }
        
    }
}