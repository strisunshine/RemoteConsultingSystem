using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Project.Controllers
{
    [Authorize(Roles = "admin")]
    public class BusinessnotesController : Controller
    {
        //
        // GET: /Businessnotes/

        public ActionResult Index(Models.Businessnotes crs)
        {
            return View(crs.businessnoteList);
        }

        public ActionResult Details(int id)
        {
            try
            {
                Models.Businessnotes Businessnotes = new Models.Businessnotes();
                foreach (Models.Businessnote Businessnote in Businessnotes.businessnoteList)
                {
                    if (Businessnote.id == id)
                    {
                        return View(Businessnote);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.Businessnotes Businessnotes = new Models.Businessnotes();
                Models.Businessnote Businessnote = new Models.Businessnote();
                Businessnote.id = Businessnotes.businessnoteList.Count;
                Businessnote.noteid = int.Parse(collection["noteid"]);
                Businessnote.title = collection["title"];
                Businessnote.content = collection["content"];
                Businessnote.notetime = collection["notetime"];
                Businessnotes.businessnoteList.Add(Businessnote);
                Businessnotes.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Models.Businessnotes Businessnotes = new Models.Businessnotes();
                foreach (Models.Businessnote Businessnote in Businessnotes.businessnoteList)
                {
                    if (Businessnote.id == id)
                    {
                        return View(Businessnote);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Models.Businessnotes Businessnotes = new Models.Businessnotes();
                foreach (Models.Businessnote Businessnote in Businessnotes.businessnoteList)
                {
                    if (Businessnote.id == id)
                    {
                        Businessnote.noteid = int.Parse(collection["noteid"]);
                        Businessnote.title = collection["title"];
                        Businessnote.content = collection["content"];
                        Businessnote.notetime = collection["notetime"];
                        Businessnotes.Save();
                        break;
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Models.Businessnotes Businessnotes = new Models.Businessnotes();
                foreach (Models.Businessnote Businessnote in Businessnotes.businessnoteList)
                {
                    if (Businessnote.id == id)
                    {
                        return View(Businessnote);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Models.Businessnotes Businessnotes = new Models.Businessnotes();
                foreach (Models.Businessnote Businessnote in Businessnotes.businessnoteList)
                {
                    if (Businessnote.id == id)
                    {
                        Businessnotes.businessnoteList.Remove(Businessnote);
                        Businessnotes.Save();
                        break;
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
