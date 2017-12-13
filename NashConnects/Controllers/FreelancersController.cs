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

namespace NashConnects.Controllers
{
    
    public class FreelancersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Freelancers
        public IQueryable<Freelancer> GetUsers()
        {
            return db.Freelancers;
        }

        // GET: api/Freelancers/current
        [HttpGet, Route("api/Freelancers/current")]
        [ResponseType(typeof(Freelancer))]
        public IHttpActionResult GetFreelancer()
        {
            Freelancer freelancer = db.Freelancers.Find(User.Identity.GetUserId());
            if (freelancer == null)
            {
                return NotFound();
            }

            return Ok(freelancer);
        }

        // PUT: api/Freelancers/5
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

            db.Entry(freelancer).State = EntityState.Modified;

            try
            {
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
    
}