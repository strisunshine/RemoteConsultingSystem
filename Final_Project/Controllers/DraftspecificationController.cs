using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    public class DraftspecificationController : Controller
    {
        private Final_ProjectContext db = new Final_ProjectContext();

        // GET: /Draftspecification/
        public ActionResult Index()
        {
            var draftspecification = from t1 in db.Draftspecifications join t2 in db.Projects on t1.Project.ProjectId equals t2.ProjectId select t1;
            return View(db.Draftspecifications.ToList());
        }

        // GET: /Draftspecification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draftspecification draftspecification = db.Draftspecifications.Find(id);
            if (draftspecification == null)
            {
                return HttpNotFound();
            }
            return View(draftspecification);
        }

        // GET: /Draftspecification/Create
        public ActionResult Create()
        {

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: /Draftspecification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Title,CustomerName,Topic,Date,RequirementsList,Note,Project")] Draftspecification draftspecification, FormCollection coll)
        {
            int ProjectId1 = Convert.ToInt32(coll["ProjectId"]);
            var project1 = db.Projects.Find(ProjectId1);
            draftspecification.RequirementsList = new List<string>();
            if (ModelState.IsValid)
            {
                foreach (var key in coll.AllKeys)
                {
                    if (key.Contains("Requirementlist"))
                    {
                        //temp = collection[key];
                        draftspecification.RequirementsList.Add(coll[key]);
                    }
                }
                db.Draftspecifications.Add(draftspecification);
                db.SaveChanges();

                SerializeToXML(draftspecification); 
                return RedirectToAction("Index");
            }
            
            return View(draftspecification);
        }

        // GET: /Draftspecification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draftspecification draftspecification = db.Draftspecifications.Find(id);
            if (draftspecification == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View(draftspecification);
        }

        // POST: /Draftspecification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,CustomerName,Topic,Date,Note")] Draftspecification draftspecification, FormCollection coll)
        {
            int ProjectId1 = Convert.ToInt32(coll["ProjectId"]);
            var project1 = db.Projects.Find(ProjectId1);
            Draftspecification draftspecificationdb = db.Draftspecifications.Include(a => a.Project).Single(t => t.id == draftspecification.id);
            draftspecificationdb.Project = project1;
            if (ModelState.IsValid)
            {
                db.Entry(draftspecificationdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(draftspecification);
        }

        // GET: /Draftspecification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draftspecification draftspecification = db.Draftspecifications.Find(id);
            if (draftspecification == null)
            {
                return HttpNotFound();
            }
            return View(draftspecification);
        }

        // POST: /Draftspecification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Draftspecification draftspecification = db.Draftspecifications.Find(id);
            db.Draftspecifications.Remove(draftspecification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        static public void SerializeToXML(Models.Draftspecification specificaton)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Models.Draftspecification));
            string path = System.Web.HttpContext.Current.Server.MapPath("~\\App_Data");
            path = path + "\\Draftspecifications.xml";
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, specificaton);
            textWriter.Close();
        }
    }
}
