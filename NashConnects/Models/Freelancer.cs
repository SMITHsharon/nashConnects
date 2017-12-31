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

        // list of other Freelancers who have recommended (Liked) This Freelancer
        // < This Freelancer, POSTED LIKE TO THIS FREELANCER >
        // for current user ::
        // < myuserId / PEEPS >
        // < FAVES / myuserid >
        public virtual List<Freelancer> FLFLRecommendations { get; set; }
        
        // list of NonProfit users who have recommended (Liked) This Freelancer
        public virtual List<NonProfit> NPRecommendations { get; set; }
        public virtual List<Event> RegEvents { get; set; }

        public enum FLCategory
        {
           
            Artist,
            Florist,
            [Display(Name = "Personal Trainer")]
            PersonalTrainer,
            Photographer,
            [Display(Name = "Real Estate Agent")]
            RealEstateAgent,
            [Display(Name = "Software Developer")]
            SoftwareDeveloper,
            Therapist,
            Writer
        }
        
    }
}