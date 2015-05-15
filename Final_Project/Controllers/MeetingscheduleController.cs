using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Final_Project.Controllers
{
    [Authorize(Roles = "admin, secretary, client")]
    public class MeetingscheduleController : Controller
    {
        private Final_ProjectContext db = new Final_ProjectContext();

        // GET: /Meetingschedule/
        public ActionResult Index()
        {
            ViewBag.Jsonlist = convertoJson();
            return View(db.Meetingschedules.ToList());
        }

        // GET: /Meetingschedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetingschedule meetingschedule = db.Meetingschedules.Find(id);
            if (meetingschedule == null)
            {
                return HttpNotFound();
            }
            return View(meetingschedule);
        }

        // GET: /Meetingschedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Meetingschedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,start,end,allDay,url,type")] Meetingschedule meetingschedule)
        {
            if (ModelState.IsValid)
            {
                db.Meetingschedules.Add(meetingschedule);
                db.SaveChanges();
                ViewBag.Jsonlist = convertoJson();

                return RedirectToAction("Index");
            }

            return View(meetingschedule);
        }

        // GET: /Meetingschedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetingschedule meetingschedule = db.Meetingschedules.Find(id);
            if (meetingschedule == null)
            {
                return HttpNotFound();
            }
            return View(meetingschedule);
        }

        // POST: /Meetingschedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,start,end,allDay,url,type")] Meetingschedule meetingschedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingschedule).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(meetingschedule);
        }

        // GET: /Meetingschedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meetingschedule meetingschedule = db.Meetingschedules.Find(id);
            if (meetingschedule == null)
            {
                return HttpNotFound();
            }
            return View(meetingschedule);
        }

        // POST: /Meetingschedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meetingschedule meetingschedule = db.Meetingschedules.Find(id);
            db.Meetingschedules.Remove(meetingschedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<string> convertoJson()
        {
            List<string> jsonlist = new List<string>();
            foreach (Meetingschedule meetingschedule in db.Meetingschedules)
            {
                var obj = new Meetingschedule
                {
                    id = meetingschedule.id,
                    title = meetingschedule.title,
                    start = meetingschedule.start,
                    end = meetingschedule.end,
                    allDay = meetingschedule.allDay,
                    url = meetingschedule.url,
                    type = meetingschedule.type

                };

                string json = new JavaScriptSerializer().Serialize(obj);
                jsonlist.Add(json);

            }
            string jsontest = new JavaScriptSerializer().Serialize(db.Meetingschedules);


            return jsonlist;
        }
        protected void OnCalendarDayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.Day % 2 == 0)
            {
                e.Cell.CssClass = "importantdatecell";
                var cellText = new System.Text.StringBuilder(300);
                cellText.AppendFormat("<a href='#'><span class='importantdate'>{0}</span></a>",
                 e.Day.Date.Day);
                cellText.AppendFormat("<span class='{0}'>{1}</span>",
                  "meetingdateevent", "Meeting Today");
                if (e.Day.Date.Day % 4 == 0)
                {
                    cellText.AppendFormat("<span class='{0}'>{1}</span>",
                     "lockdateevent", "Droid Rocks!");
                }
                e.Cell.Text = cellText.ToString();
            }
            else
            {
                e.Cell.CssClass = "regulardatecell";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
