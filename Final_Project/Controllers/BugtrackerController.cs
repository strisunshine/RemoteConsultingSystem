using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace Final_Project.Controllers
{
    
    [Authorize(Roles = "admin")]
    
    public class BugtrackerController : Controller
    {
        //
        // GET: /Bugtracker/

        private Final_ProjectContext db = new Final_ProjectContext();
        public ActionResult Index(Models.Bugnotes crs)
        {
            //var draftspecification = from t1 in crs join t2 in db.Projects on t1.Project.ProjectId equals t2.ProjectId select t1;
            return View(crs.bugnoteList);
        }

        public ActionResult Details(int id)
        {
            try
            {
                Models.Bugnotes bugnotes = new Models.Bugnotes();
                foreach (Models.Bugnote Bugnote in bugnotes.bugnoteList)
                {
                    if (Bugnote.id == id)
                    {
                        return View(Bugnote);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public ActionResult Create()
        {
            Models.Projects crt = new Projects();
            List<Project> projectlist = new List<Project>();
            projectlist = crt.projectList;
            //DataTable dt = ConvertToDataTable(projectlist);
            
            //DataSet ds = new DataSet("table");
            //ds.Tables.Add(dt);
            //dt.Columns.Add("ProjectId", typeof(int));
            //dt.Columns.Add("ProjectName", typeof(string));
            //foreach (Models.Project project in projectlist)
            //{
            //    dt.Rows.Add(project.ProjectId, project.ProjectName);
            //}       
            //ds.Tables.Add(dt); 
            //ViewBag.projectlist = projectlist;
            //string path = System.Web.HttpContext.Current.Server.MapPath("~\\App_Data\\Project.xml");
            //XDocument doc = XDocument.Load(path);
            //var q = from crs in doc.Elements("projects").Elements("project") select crs;
            //ViewData["ProjectId"] = new SelectList(projectlist, "ProjectId", "ProjectName");
            //ViewBag.ProjectList = new SelectList(new[]
            //{
            //         new { Id ="1", Name ="C Project" },
            //         new { Id ="2", Name ="D Project" }

            //}, "Id", "Name");
            //ViewBag.ProjectId = "1";
            //bool isSelected = false;
            //List<SelectListItem> dropdownItems = new List<SelectListItem>();
            //foreach (Models.Project project in projectlist)
            //{
            //    if (4 == project.ProjectId)
            //        isSelected = true;
            //    else
            //        isSelected = false;

            //    dropdownItems.Add(new SelectListItem { Text = project.ProjectName, Value = project.ProjectId.ToString(), Selected = isSelected });
            //}
            //ViewBag.ProjectId = dropdownItems;
            //ViewBag.ProjectId = new SelectList(ds, "ProjectId", "ProjectName");
            //ViewData["ProjectId"] = dropdownItems;

            //ViewBag.ProjectIIdd = new List<SelectListItem>();
            ViewBag.ProjectId = new SelectList(projectlist, "ProjectId", "ProjectName");
           
            List<string> alist = new List<string> { "not started", "pending", "complete" };
            //Models.Bugnotes crs;
            ViewBag.status = new SelectList(alist, "alist[0]");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
           { 
                Models.Bugnotes bugnotes = new Models.Bugnotes();
                Models.Bugnote bugnote = new Models.Bugnote();
                Models.Projects projects = new Models.Projects();
                bugnote.id = bugnotes.bugnoteList.Count;
                bugnote.name = collection["name"];
                int ProjectId1 = Convert.ToInt32(collection["ProjectId"]);
                foreach (Models.Project project in projects.projectList)
                {
                    if (project.ProjectId == ProjectId1)
                        bugnote.Project = project;
                }
                string projectname1 = bugnote.Project.ProjectName;
                //Models.Projects crt = new Projects();
                //List<Project> projectlist = crt.projectList;
                //List<int> p = new List<int>();
                //foreach (Project pro in projectlist)
                //{
                //    p.Add(pro.ProjectId);
                //}
                //if (p.Contains(testid)) 
                    //bugnote.Project.ProjectId = testid;
                //else
                //{
                //    return RedirectToAction("Index");
                //}
                
                bugnote.client = collection["client"];
                bugnote.description = collection["description"];
                bugnote.resolution = collection["resolution"];
                bugnote.status = collection["status"];
                bugnotes.bugnoteList.Add(bugnote);
                bugnotes.Save();

                //Models.Projects crt = new Projects();
                //List<Project> projectlist = new List<Project>();
                //projectlist = crt.projectList;
                //ViewBag.projectlist = projectlist;
                //ViewBag.ProjectIIdd = new SelectList(projectlist, "ProjectId", "ProjectName");
                //List<string> alist = new List<string> { "not started", "pending", "complete" };
                //ViewBag.status = new SelectList(alist, "alist[0]");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Models.Projects crt = new Projects();
            List<Project> projectlist = crt.projectList;
            ViewBag.ProjectId = new SelectList(projectlist, "ProjectId", "ProjectName");
            try
            {
                Models.Bugnotes bugnotes = new Models.Bugnotes();
                foreach (Models.Bugnote bugnote in bugnotes.bugnoteList)
                {
                    if (bugnote.id == id)
                    {
                        return View(bugnote);
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
                Models.Bugnotes bugnotes = new Models.Bugnotes();
                foreach (Models.Bugnote bugnote in bugnotes.bugnoteList)
                {
                    if (bugnote.id == id)
                    {
                        bugnote.Project.ProjectId = Convert.ToInt32(collection["ProjectId"]);
                        Project a = new Project();
                        a.ProjectId = bugnote.Project.ProjectId;
                        bugnote.Project.ProjectName = a.ProjectName;
                        bugnote.name = collection["name"];
                        bugnote.client = collection["client"];
                        bugnote.description = collection["description"];
                        bugnote.resolution = collection["resolution"];
                        bugnote.status = collection["status"];
                        bugnotes.Save();
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
                Models.Bugnotes bugnotes = new Models.Bugnotes();
                foreach (Models.Bugnote bugnote in bugnotes.bugnoteList)
                {
                    if (bugnote.id == id)
                    {
                        return View(bugnote);
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
                Models.Bugnotes bugnotes = new Models.Bugnotes();
                foreach (Models.Bugnote bugnote in bugnotes.bugnoteList)
                {
                    if (bugnote.id == id)
                    {
                        bugnotes.bugnoteList.Remove(bugnote);
                        bugnotes.Save();
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
