using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NashConnects.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NashConnects.ViewModels;

namespace NashConnects.Controllers
{
    [RoutePrefix("api/Freelancers")]
    public class FreelancersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Freelancers
        [HttpGet, Route("list")]
        //public IQueryable<Freelancer> GetUsers()
        public HttpResponseMessage GetAllFreelancersGroupByCat()
        { 
            var db = new ApplicationDbContext();

            //var freelancersByCategory = db.Freelancers.ToList();

            // THIS GROUPS BY CATEGORY, BUT ONLY RETURNS FREELANCE IDs
            /*
            var freelancersByCategory = from f in db.Freelancers
                                        group f.Id by f.Category into g
                                        //select new { Category = g.Key, Id = g.ToList() };
                                        select new { Category = g.Key, Freelancers = g.ToList() };
            */

            // RETURNS FREELANCERS GROUPED BY CATEGORY
            var freelancersByCategory = db.Freelancers
                .GroupBy(freelancer => freelancer.Category)
                .Select(freelancerGroup => 
                new {
                        CategoryName = freelancerGroup.Key.ToString(),
                        //Freelancers = freelancerGroup.Select(freelancer => freelancer).ToList()
                    
                        Freelancers = freelancerGroup.Select(freelancer =>
                            new {
                                    freelancer.Id,
                                    freelancer.FirstName,
                                    freelancer.LastName,
                                    freelancer.WebsiteURL,
                                    freelancer.Description,
                                    freelancer.RecommendCount,
                                    freelancer.PublicReveal
                                })
                    })
                    .ToList();
            

            return Request.CreateResponse(HttpStatusCode.OK, freelancersByCategory);
        }


        // GET: api/Freelancers
        [HttpGet, Route("list/newsletter")]
        //public IQueryable<Freelancer> GetUsers()
        public HttpResponseMessage GetAllFreelancersByIdForNewsletter()
        {
            var db = new ApplicationDbContext();

            //var freelancersById = db.Freelancers.ToList();
            var freelancersListById = db.Freelancers.Select(freelancer =>
                new {
                        freelancer.Id,
                        freelancer.FirstName,
                        freelancer.LastName,
                        freelancer.Email,
                        freelancer.WebsiteURL
                    })
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, freelancersListById);
        }


        // GET: api/Freelancers/current
        [Authorize]
        [HttpGet, Route("current")]
        [ResponseType(typeof(Freelancer))]
        public IHttpActionResult GetCurrentFreelancer()
        {
            Freelancer freelancer = db.Freelancers.Find(User.Identity.GetUserId());
            if (freelancer == null)
            {
                return NotFound();
            }

            return Ok(freelancer);
        }


        // GET: api/Freelancers/5
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Freelancer))]
        public IHttpActionResult GetFreelancerById(string id)
        {
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return Ok(freelancer);
        }


        // PUT: api/Freelancers/5
        [Authorize]
        [HttpPut, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFreelancer(string id, Freelancer freelancer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != freelancer.Id)
            {
                return BadRequest();
            }

            // overwrites Password and SecurityStamp
            //db.Entry(freelancer).State = EntityState.Modified;

            // ensures Password and SecurityStamp are retained
            var originalFreelancer = db.Freelancers.Find(id);

            try
            {
                originalFreelancer.UserName = freelancer.UserName;
                originalFreelancer.FirstName = freelancer.FirstName;
                originalFreelancer.LastName = freelancer.LastName;
                originalFreelancer.Email = freelancer.Email;
                originalFreelancer.WebsiteURL = freelancer.WebsiteURL;
                originalFreelancer.Category = freelancer.Category;
                originalFreelancer.Description = freelancer.Description;
                originalFreelancer.Newsletter = freelancer.Newsletter;
                originalFreelancer.PublicReveal = freelancer.PublicReveal;
                originalFreelancer.Active = freelancer.Active;
                // Id, Password, and SecurityStamp are not updated

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreelancerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // PUT: api/Freelancers/5
        //[Authorize]//
        [HttpPut, Route("likes/{id}")]
        [ResponseType(typeof(void))]
        //public IHttpActionResult IncrementLikes(string id, Freelancer freelancer)
        public IHttpActionResult IncrementLikes(string id)
        {
            var originalFreelancer = db.Freelancers.Find(id);

            try
            {
                originalFreelancer.RecommendCount = originalFreelancer.RecommendCount + 1;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreelancerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Freelancers
        [ResponseType(typeof(Freelancer))]
        public IHttpActionResult PostFreelancer(Freelancer freelancer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(freelancer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FreelancerExists(freelancer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = freelancer.Id }, freelancer);
        }


        
        // POST: api/Freelancers/5
        [Authorize]
        [HttpPost, Route("{freelancerId}/register/{eventId}")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult RegisgterFreelancerForEvent(string freelancerId, int eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var freelancer = db.Freelancers.Find(freelancerId);
            var thisEvent = db.Events.Find(eventId);
            freelancer.RegEvents.Add(thisEvent);
            //thisEvent.Freelancers.Add(freelancer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!FreelancerExists(freelancer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        


        // DELETE: api/Freelancers/5
        [ResponseType(typeof(Freelancer))]
        public IHttpActionResult DeleteFreelancer(string id)
        {
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return NotFound();
            }

            db.Users.Remove(freelancer);
            db.SaveChanges();

            return Ok(freelancer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FreelancerExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    
    }

    internal class flRepository
    {
        internal static IQueryable<Freelancer> GetQueryableFreelancers()
        {
            throw new NotImplementedException();
        }
    }
}