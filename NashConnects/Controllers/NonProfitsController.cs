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
    [RoutePrefix("api/NonProfits")]
    public class NonProfitsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/NonProfits
        [HttpGet, Route("list")]
        //public IQueryable<NonProfit> GetUsers()
        public HttpResponseMessage GetAllNonProfitsById()
        {

            var db = new ApplicationDbContext();
            var nonProfitList = db.NonProfits.ToList();
            /*
            var result = (from f in new ApplicationDbContext().Freelancers
                          
                          select new FreelancerListView { f.LastName, f.WebsiteURL }).ToList();
            */
            return Request.CreateResponse(HttpStatusCode.OK, nonProfitList);



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


        // GET: api/NonProfits/current
        [Authorize]
        [HttpGet, Route("current")]
        [ResponseType(typeof(NonProfit))]
        //public IQueryable<NonProfit> GetUsers()
        public IHttpActionResult GetFreelancer()
        {
            NonProfit nonProfit = db.NonProfits.Find(User.Identity.GetUserId());
            if (nonProfit == null)
            {
                return NotFound();
            }

            return Ok(nonProfit);
        }

        // GET: api/NonProfits/5
        [ResponseType(typeof(NonProfit))]
        public IHttpActionResult GetNonProfit(string id)
        {
            NonProfit nonProfit = db.NonProfits.Find(id);
            if (nonProfit == null)
            {
                return NotFound();
            }

            return Ok(nonProfit);
        }

        // PUT: api/NonProfits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNonProfit(string id, NonProfit nonProfit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nonProfit.Id)
            {
                return BadRequest();
            }

            db.Entry(nonProfit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NonProfitExists(id))
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

        // POST: api/NonProfits
        [ResponseType(typeof(NonProfit))]
        public IHttpActionResult PostNonProfit(NonProfit nonProfit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(nonProfit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NonProfitExists(nonProfit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nonProfit.Id }, nonProfit);
        }

        // DELETE: api/NonProfits/5
        [ResponseType(typeof(NonProfit))]
        public IHttpActionResult DeleteNonProfit(string id)
        {
            NonProfit nonProfit = db.NonProfits.Find(id);
            if (nonProfit == null)
            {
                return NotFound();
            }

            db.Users.Remove(nonProfit);
            db.SaveChanges();

            return Ok(nonProfit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NonProfitExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}