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
