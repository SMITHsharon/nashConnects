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
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Events
        public IQueryable<Event> GetEvents()
        {
            return db.Events;
        }

        // GET: api/Events/5
        //[Authorize]
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult GetEventById(int id)
        {
            Event thisEvent = db.Events.Find(id);

            if (thisEvent == null)
            {
                return NotFound();
            }

            return Ok(thisEvent);
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventId)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // PUT: api/Events/edit/5
        //[Authorize]
        [HttpPut, Route("edit/{id}")]
        [ResponseType(typeof(void))]
        //public IHttpActionResult PutEvent(int id, Event @event)
        public IHttpActionResult UpdateEvent(int id, Event thisEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*
            if (id != @event.EventId)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;
            */

            if (id != thisEvent.EventId)
            {
                return BadRequest();
            }

            var originalEvent = db.Events.Find(id);
            try
            {
                originalEvent.EventName = thisEvent.EventName;
                originalEvent.Description = thisEvent.Description;
                originalEvent.StartDate = originalEvent.StartDate;
                originalEvent.EndDate = originalEvent.EndDate;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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


        // POST: api/Events/5
        /*
        [Authorize]
        [HttpPost, Route("{eventId}/register/{freelancerId}")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult RegisgterFreelancerForEvent(int eventId, string freelancerId)
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
                if (!EventExists(thisEvent.EventId))
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
        */


        // POST: api/Events
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(@event);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            db.SaveChanges();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.EventId == id) > 0;
        }
    }
}