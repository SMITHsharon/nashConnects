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
            var nonProfitList = db.NonProfits.Select(nonProfit => 
                new {
                        Id = nonProfit.Id,
                        Name = nonProfit.Name,
                        WebsiteURL = nonProfit.WebsiteURL,
                        Description = nonProfit.Description,
                        RecommendCount = nonProfit.RecommendCount,
                        Active = nonProfit.Active,
                        //Events = nonProfit.Events.
                        Events = nonProfit.Events.Select(thisEvent => 
                            new { thisEvent.EventId,
                                  thisEvent.EventName,
                                  thisEvent.StartDate,
                                  thisEvent.EndDate,
                                  thisEvent.Description
                                })
                 }).ToList();

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

        // PUT: api/NonProfits/likes/5
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


        // POST: api/NonProfits
        //[Authorize]//
        [HttpPost, Route("{nonprofitid}/addEvent")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult AddEventForNonProfit(string nonprofitid, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nonProfit = db.NonProfits.Find(nonprofitid);

            try
            {
                nonProfit.Events.Add(@event);
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

            return Ok();
        }
        
        
        // GET: api/NonProfits
        [HttpGet, Route("{nonprofitid}/events/list")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult ListEventsForNonProfit(string nonprofitid)
        {
            var db = new ApplicationDbContext();
            
            NonProfit nonProfit = db.NonProfits.Find(nonprofitid);

            if (nonProfit == null)
            {
                return NotFound();
            }

            var eventList = new
            {
                nonProfitId = nonProfit.Id,
                nonProfitName = nonProfit.Name,
                nonProfitURL = nonProfit.WebsiteURL,
                nonProfitActive = nonProfit.Active,
                //Events = nonProfit.Events
                Events = nonProfit.Events.Select(thisEvent =>
                    new {
                            thisEvent.EventId,
                            thisEvent.EventName,
                            //thisEvent.StartDate,
                            //thisEvent.EndDate,
                            StartDate = thisEvent.StartDate.ToLocalTime().ToString("MM/dd/yyyy @ h:mm"),
                            EndDate = thisEvent.EndDate.ToLocalTime().ToString("MM/dd/yyyy @ h:mm"),
                            thisEvent.Description
                        })
            };

            return Ok(eventList);
            //return Ok(nonProfit.Events);
        }


        // GET: api/NonProfits
        [HttpGet, Route("{nonprofitId}/events/{eventId}")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult GetEventById(string nonprofitId, int eventId)
        {
            var db = new ApplicationDbContext();

            NonProfit nonProfit = db.NonProfits.Find(nonprofitId);
            Event thisEvent = db.Events.Find(eventId);

            if (nonProfit == null)
            {
                return NotFound();
            }

            var eventList = new
            {
                nonProfitName = nonProfit.Name,
                Events = nonProfit.Events
            };

            return Ok(thisEvent);
            //return Ok(nonProfit.Events);
        }


        // GET: api/NonProfits
        [HttpGet, Route("events/list")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult GetAllEventsGroupedByNonProfit()
        {
            var db = new ApplicationDbContext();

            var eventGroupingsDTO = db.NonProfits.ToList()
                .Select(np => new
                {
                    NonProfitId = np.Id,
                    NonProfitName = np.Name,
                    NonProfitURL = np.WebsiteURL,
                    NonProfitActive = np.Active,
                    Events = np.Events.Select(x =>
                    new {
                        x.Description,
                        x.EventName,
                        x.EventId,
                        //StartDate = x.StartDate.ToString("MM/dd/yyyy @ H:mm"),
                        StartDate = x.StartDate.ToLocalTime().ToString("MM/dd/yyyy @ h:mm"),
                        EndDate = x.EndDate.ToLocalTime().ToString("MM/dd/yyyy @ h:mm"),
                        //EndDate = x.EndDate.ToString("MM/dd/yyyy @ H:mm"),
                    })
                })
                .GroupBy(dto => dto.NonProfitId)
                .ToList();

            return Ok(eventGroupingsDTO);
        }


        // DELETES are handled by posting the record as Inactive
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


        // PUT: api/NonProfits/delete/5
        [Authorize]
        [HttpPut, Route("delete/{likedId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SetCurrentUserAsInactive(string likedId)
        {
            var activeNonProfit = db.NonProfits.Find(likedId);

            try
            {
                activeNonProfit.Active = false;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NonProfitExists(activeNonProfit.Id))
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