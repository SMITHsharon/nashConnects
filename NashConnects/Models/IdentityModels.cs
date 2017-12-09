﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace NashConnects.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        
        [Required]
        [StringLength(25)]
        public string FName { get; set; }
        [Required]
        [StringLength(25)]
        public string LName { get; set; }
        [Required]
        [StringLength(50)]
        public string WebsiteURL { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int RecommendCount { get; set; }
        public bool Active { get; set; }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public System.Data.Entity.DbSet<NashConnects.Models.Freelancer> Freelancers { get; set; }

        public System.Data.Entity.DbSet<NashConnects.Models.NonProfit> NonProfits { get; set; }

        public System.Data.Entity.DbSet<NashConnects.Models.Event> Events { get; set; }
        

        // define the many-to-mamy for Freelancers Registered for Events
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                        .HasMany(e => e.Freelancers)
                        .WithMany(f => f.RegEvents)
                        .Map(ef =>
                            {
                                ef.MapLeftKey("FLRefId");
                                ef.MapRightKey("EventRefId");
                                ef.ToTable("FLRegEvent");
                            });
        }
        
    }
}