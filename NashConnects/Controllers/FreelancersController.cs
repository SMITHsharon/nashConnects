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
                                    freelancer.PublicReveal,
                                    freelancer.Active
                                })
                    })
                    .ToList();
            

            return Request.CreateResponse(HttpStatusCode.OK, freelancersByCategory);
        }


        // GET: api/Freelancers/5/peeps
        [HttpGet, Route("{userid}/peeps")]
        //public IQueryable<Freelancer> GetUsers()
        public HttpResponseMessage GetFreelancersPeepsList(string userid)
        {
            var db = new ApplicationDbContext();

            var freelancer = db.Freelancers.Find(userid);

            var freelancerPeepsList = freelancer.FLFLRecommendations;

            return Request.CreateResponse(HttpStatusCode.OK, freelancerPeepsList);
        }


        // GET: api/Freelancers/5/faves
        [HttpGet, Route("{userid}/faves")]
        //public IQueryable<Freelancer> GetUsers()
        public HttpResponseMessage GetFreelancersFavesList(string userid)
        {
            var db = new ApplicationDbContext();

            var freelancer = db.Freelancers.Find(userid);

            // see ListEventsForNonProfit

            //var freelancersById = db.Freelancers.ToList();

            /*
            var faveFreelancers = db.Freelancers.Select(fl =>
                new {
                    fl.Id,
                    fl.FLFLRecommendations
                })
                .Where (fl => fl.Id == userid)
                .Where(fl => fl.Freelancer_Id1 == userid) // how to get these records using second column as primary key ???
                .ToList();
            */


            return Request.CreateResponse(HttpStatusCode.OK, freelancer);
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
        [Authorize]
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


        // GET: api/Freelancers/5/registeredEvents
        //[Authorize]
        [HttpGet, Route("{id}/registeredEvents")]
        [ResponseType(typeof(Freelancer))]
        public IHttpActionResult GetRegisteredEventsForFreelancerS()
        {
            Freelancer freelancer = db.Freelancers.Find(User.Identity.GetUserId());

            /*
            var freelancerRegisteredEvents = 
                (
                from fl in db.Freelancers
                //from np in db.NonProfits
                select new 
                {
                    fl.Id,
                    fl.FirstName,
                    fl.LastName,
                    fl.RegEvents,
                    //np.Name
                })
                .Where(fl => fl.Id == freelancer.Id)
                .ToList();
            */

            // HOW TO IDENTIFY THE NONPROFIT FOR EACH EVENT ???
            /*
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
            */

            /* RETURNS EVENTS GROUPS BY NONPROFIT
            var eventGroupingsDTO = db.NonProfits
               .Select(np => new
               {
                   NonProfitId = np.Id,
                   NonProfitName = np.Name,
                   Events = np.Events
               })
               .GroupBy(dto => dto.NonProfitId)
               .ToList();
            */

            if (freelancer == null)
            {
                return NotFound();
            }

            return Ok(freelancer);
            //return Ok(freelancerRegisteredEvents);
        }


        // PUT: api/Freelancers/edit/5
        [Authorize]
        [HttpPut, Route("edit/{id}")]
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


        // PUT: api/Freelancers/likes/5
        [Authorize]
        [HttpPut, Route("likes/{likedId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult IncrementLikes(string likedId)
        {
            var likedFreelancer = db.Freelancers.Find(likedId);

            try
            {
                likedFreelancer.RecommendCount = likedFreelancer.RecommendCount + 1;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreelancerExists(likedFreelancer.Id))
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


        // PUT: api/Freelancers/delete/5
        [Authorize]
        [HttpPut, Route("delete/{likedId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SetCurrentUserAsInactive(string likedId)
        {
            var activeFreelancer = db.Freelancers.Find(likedId);

            try
            {
                activeFreelancer.Active = false;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreelancerExists(activeFreelancer.Id))
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


        // POST: api/Freelancers/likes/5/3
        [Authorize]
        [HttpPost, Route("likes/{likedId}/{userId}")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult AddLikesRelationship(string likedId, string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var likedFreelancer = db.Freelancers.Find(likedId);
            var userFreelancer = db.Freelancers.Find(userId);

            try
            {
                likedFreelancer.FLFLRecommendations.Add(userFreelancer);
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if ( (!FreelancerExists(likedFreelancer.Id)) || (!FreelancerExists(userFreelancer.Id)))
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
        


        // DELETS are handled by posting the record as Inactive
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