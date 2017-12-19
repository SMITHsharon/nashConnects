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

            return Request.CreateResponse(HttpStatusCode.OK, nonProfitList);
        }


        // GET: api/NonProfits/current
        [Authorize]
        [HttpGet, Route("current")]
        [ResponseType(typeof(NonProfit))]
        //public IQueryable<NonProfit> GetUsers()
        public IHttpActionResult GetCurrentNonProfit()
        {
            NonProfit nonProfit = db.NonProfits.Find(User.Identity.GetUserId());
            if (nonProfit == null)
            {
                return NotFound();
            }

            return Ok(nonProfit);
        }

        // GET: api/NonProfits/5
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(NonProfit))]
        public IHttpActionResult GetNonProfitById(string id)
        {
            NonProfit nonProfit = db.NonProfits.Find(id);
            if (nonProfit == null)
            {
                return NotFound();
            }

            return Ok(nonProfit);
        }

        // PUT: api/NonProfits/5
        [Authorize]
        [HttpPut, Route("{id}")]
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

            // overwrites Password and SecurityStamp
            //db.Entry(nonProfit).State = EntityState.Modified;

            // ensures Password and SecurityStamp are retained
            var originalNonProfit = db.NonProfits.Find(id);

            try
            {
                originalNonProfit.UserName = nonProfit.UserName;
                originalNonProfit.FirstName = nonProfit.FirstName;
                originalNonProfit.LastName = nonProfit.LastName;
                originalNonProfit.Email = nonProfit.Email;
                originalNonProfit.Name = nonProfit.Name;
                originalNonProfit.WebsiteURL = nonProfit.WebsiteURL;
                originalNonProfit.CalendarLink = nonProfit.CalendarLink;
                originalNonProfit.Description = nonProfit.Description;
                originalNonProfit.Active = nonProfit.Active;
                // Id, Password, and SecurityStamp are not updated

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

        // PUT: api/NonProfits/5
        //[Authorize]//
        [HttpPut, Route("likes/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult IncrementLikes(string id)
        {
            var originalNonProfit = db.NonProfits.Find(id);

            try
            {
                originalNonProfit.RecommendCount = originalNonProfit.RecommendCount + 1;

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