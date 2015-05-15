using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace Final_Project.Controllers1
{
    [Authorize(Roles = "admin")]
    public class DraftspecificationController : Controller
    {

        //[XmlElement(ElementName = "Requirement")]
        //public string temp;
        //
        // GET: /Draftspecification/
        //public ActionResult Index()
        //{

        //    return View();
        //}
        public ActionResult Index(Models.Draftspecification drs)
        {

            return View(drs);
        }


        //
        // GET: /Draftspecification/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Draftspecification/Create
        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Draftspecification/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.Draftspecification Drafspecification = new Models.Draftspecification();
                  Drafspecification.Title = collection["Title"];
                  Drafspecification.CustomerName = collection["CustomerName"];
                  Drafspecification.Topic = collection["Topic"];
                  Drafspecification.Date = collection["Date"];
                  Drafspecification.RequirementsList = new List<string>();
                  foreach (var key in collection.AllKeys)
                  {
                      if(key.Contains("Requirementlist"))
                      {
                          //temp = collection[key];
                          Drafspecification.RequirementsList.Add(collection[key]);
                      }
                  }
                  Drafspecification.Note = collection["Note"];
              
                 SerializeToXML(Drafspecification); 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Draftspecification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Draftspecification/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Draftspecification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Draftspecification/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        static public void SerializeToXML(Models.Draftspecification specificaton)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Models.Draftspecification));
            string path = System.Web.HttpContext.Current.Server.MapPath("~\\Content");
            path = path + "\\Draftspecifications.xml";
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, specificaton);
            textWriter.Close();
        }
    }
}
