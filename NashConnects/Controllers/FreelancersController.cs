﻿using System;
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
        public HttpResponseMessage GetAllFreelancersById()
        {

            var db = new ApplicationDbContext();
            var freelanceList = db.Freelancers.ToList();
            /*
            var result = (from f in new ApplicationDbContext().Freelancers
                          
                          select new FreelancerListView { f.LastName, f.WebsiteURL }).ToList();
            */
            return Request.CreateResponse(HttpStatusCode.OK, freelanceList);
            


            //IQueryable<Freelancer> freelancers = flRepository.GetQueryableFreelancers();
            //var listOfFreelancers = freelancers;
            //return listOfFreelancers;


            //var userId = User.Identity.GetUserId();
            //userId.ToString();

            //var db = new ApplicationDbContext();
            //var fl = db.Freelancers;
            //var listOfFreelancers = db.Freelancers.Where(fl => fl.Id.Contains(userId));

            //return Request.CreateResponse(HttpStatusCode.OK, listOfFreelancers);
        }


        // GET: api/Freelancers/current
        [Authorize]
        [HttpGet, Route("current")]
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

            db.Entry(freelancer).State = EntityState.Modified;

            try
            {
                //Freelancer user = db.Freelancer.Find(id));
                //var user = (Freelancer)UserManagerExtensions.FindById(id);
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

    internal class flRepository
    {
        internal static IQueryable<Freelancer> GetQueryableFreelancers()
        {
            throw new NotImplementedException();
        }
    }
}