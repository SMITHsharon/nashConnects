namespace NashConnects.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using NashConnects.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NashConnects.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NashConnects.Models.ApplicationDbContext context)
        {
            //SEED WITH ADMIN USER
            var adminRole = new IdentityRole("Admin");
            context.Roles.AddOrUpdate(r => r.Name, adminRole);
            context.SaveChanges();

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser
            {
                UserName = "Sharon",
                Email = "sharon@gmail.com",
                FirstName = "Sharon",
                LastName = "Smith",
                WebsiteURL = "xxx.com",
            };
            userManager.CreateAsync(user, "password").Wait();
            var addedUser = context.Users.First(x => x.UserName == user.UserName);
            userManager.AddToRoleAsync(addedUser.Id, "Admin").Wait();
            

            //SEED WITH FREELANCE USERS
            var user2 = new Freelancer
            {
                UserName = "cFrazier",
                Email = "carol@gmail.com",
                FirstName = "Carol",
                LastName = "Frazier",
                WebsiteURL = "http://www.bestuwellness.com/"
            };
            userManager.CreateAsync(user2, "password").Wait();

            var user3 = new Freelancer
            {
                UserName = "tPetillo",
                Email = "tom@gmail.com",
                FirstName = "Thomas",
                LastName = "Petillo",
                WebsiteURL = "http://www.thomaspetillo.com/"
            };
            userManager.CreateAsync(user3, "password").Wait();
            
            var user4 = new Freelancer
            {
                UserName = "pCarr",
                Email = "patrick@gmail.com",
                FirstName = "Patrick",
                LastName = "Carr",
                WebsiteURL = "http://www.patrickwcarr.com/"
            };
            userManager.CreateAsync(user4, "password").Wait();

            var user5 = new Freelancer
            {
                UserName = "iCron",
                Email = "ian@gmail.com",
                FirstName = "Ian",
                LastName = "Cron",
                WebsiteURL = "http://iancron.com/"
            };
            userManager.CreateAsync(user5, "password").Wait();

            var user6 = new Freelancer
            {
                UserName = "nAndres",
                Email = "nita@gmail.com",
                FirstName = "Nita",
                LastName = "Andrews",
                WebsiteURL = "http://creativelectio.com/"
            };
            userManager.CreateAsync(user6, "password").Wait();

            var user7 = new Freelancer
            {
                UserName = "aAndrews",
                Email = "al@gmail.com",
                FirstName = "Al",
                LastName = "Andrews",
                WebsiteURL = "http://creativelectio.com/"
            };
            userManager.CreateAsync(user7, "password").Wait();

            var user8 = new Freelancer
            {
                UserName = "bSanders",
                Email = "barbara@gmail.com",
                FirstName = "Barbara",
                LastName = "Sanders",
                WebsiteURL = "http://www.barbarasanderslcsw.com/"
            };
            userManager.CreateAsync(user8, "password").Wait();

            var user9 = new Freelancer
            {
                UserName = "pNitch",
                Email = "pat@gmail.com",
                FirstName = "Patrick",
                LastName = "Nitch",
                WebsiteURL = "http://www.mindfultherapynashville.com/neighborhood/"
            };
            userManager.CreateAsync(user9, "password").Wait();

            var user10 = new Freelancer
            {
                UserName = "rKochtitzky",
                Email = "rod@gmail.com",
                FirstName = "Rod",
                LastName = "Kochtitzky",
                WebsiteURL = "https://rodk.net/"
            };
            userManager.CreateAsync(user10, "password").Wait();

            var user11 = new Freelancer
            {
                UserName = "mBlom",
                Email = "mitchell@gmail.com",
                FirstName = "Mitchell",
                LastName = "Blom",
                WebsiteURL = "http://mitchellblom.com/#!/about"
            };
            userManager.CreateAsync(user11, "password").Wait();

            var user12 = new Freelancer
            {
                UserName = "bGreaves",
                Email = "ben@gmail.com",
                FirstName = "Ben",
                LastName = "Greaves",
                WebsiteURL = "https://bsgreaves.github.io/portfolio/"
            };
            userManager.CreateAsync(user12, "password").Wait();

            var user17 = new Freelancer
            {
                UserName = "tVillager",
                Email = "tim@gmail.com",
                FirstName = "Tim",
                LastName = "Villager",
                WebsiteURL = "https://fridrichandclark.com/agents/tim-villager/"
            };
            userManager.CreateAsync(user12, "password").Wait();

            var user18 = new Freelancer
            {
                UserName = "trVillager",
                Email = "troy@gmail.com",
                FirstName = "Troy",
                LastName = "Villager",
                WebsiteURL = "https://www.tvillagerdesigns.com/"
            };
            userManager.CreateAsync(user12, "password").Wait();


            //SEED WITH NON-PROFIT USERS
            var user13 = new NonProfit
            {
                UserName = "lWilliams",
                Email = "liz@gmail.com",
                FirstName = "Liz",
                LastName = "Williams",
                WebsiteURL = "http://makenashville.org/"
            };
            userManager.CreateAsync(user13, "password").Wait();
            
            var user14 = new NonProfit
            {
                UserName = "sCarroll",
                Email = "sarah@gmail.com",
                FirstName = "Sarah",
                LastName = "Carroll",
                WebsiteURL = "https://www.ec.co/"

            };
            userManager.CreateAsync(user14, "password").Wait();

            var user15 = new NonProfit
            {
                UserName = "sFarley",
                Email = "sue@gmail.com",
                FirstName = "Sue",
                LastName = "Farley",
                WebsiteURL = "https://creativemornings.com/"
            };
            userManager.CreateAsync(user15, "password").Wait();

            var user16 = new NonProfit
            {
                UserName = "eSappenfield",
                Email = "essie@gmail.com",
                FirstName = "Essie",
                LastName = "Sappenfield",
                WebsiteURL = "http://www.theskillery.com/"
            };
            userManager.CreateAsync(user16, "password").Wait();
            
            

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
