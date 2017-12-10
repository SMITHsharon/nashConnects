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

namespace NashConnects.Controllers
{
    /*
    public class NonProfitsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NonProfits
        public IQueryable<NonProfit> GetUsers()
        {
            return db.Users;
        }

        // GET: api/NonProfits/5
        [ResponseType(typeof(NonProfit))]
        public IHttpActionResult GetNonProfit(string id)
        {
            NonProfit nonProfit = db.Users.Find(id);
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
            NonProfit nonProfit = db.Users.Find(id);
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
    */
}