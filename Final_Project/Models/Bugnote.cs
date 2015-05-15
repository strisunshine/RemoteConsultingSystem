using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Final_Project.Models
{
    //public enum Status
    //{
    //    [Description("not started")]
    //    notstarted,
    //    pending,
    //    complete,

    //}
    public class Bugnote
    {
        //public Bugnote()
        //{
        //    StatusList = new List<SelectListItem>;
        //}
        public int id { get; set; }

        [DisplayName("Bug")]
        [Required(ErrorMessage = "Bug name is required")]
        public string name { get; set; }

        [DisplayName("Client")]
        [Required(ErrorMessage = "Client name is required")]
        public string client { get; set; }

        [DisplayName("Description")]

        public string description { get; set; }

        [DisplayName("Resolution")]
        public string resolution { get; set; }

        [DisplayName("Status")]
        //[Required(ErrorMessage = "Status must be chosen")]
        public string status { get; set; }
        public virtual Project Project
        { get; set; }
    }

    public class Bugnotes
    {
        string path;

        public List<Bugnote> bugnoteList { get; set; }

        public Bugnotes()
        {
            bugnoteList = new List<Bugnote>();
            path = HttpContext.Current.Server.MapPath("~\\App_Data\\Bugtracker.xml");
            Fill();
        }

        public void Fill()
        {
            try
            {
                XDocument doc = XDocument.Load(path);

                var q = from crs in doc.Elements("bugnotes").Elements("bugnote") select crs;
                int i = 0;
                foreach (var elem in q)
                {
                    Bugnote c = new Bugnote();
                    c.id = ++i;
                    string ProjectName1 = elem.Element("projectName").Value;
                    Models.Projects projects = new Models.Projects();
                    foreach (Models.Project project in projects.projectList)
                    {
                        if (project.ProjectName == ProjectName1)
                            c.Project = project;
                    }
                    c.name = elem.Element("name").Value;
                    c.client = elem.Element("client").Value;
                    c.description = elem.Element("description").Value;
                    c.resolution = elem.Element("resolution").Value;
                    c.status = elem.Element("status").Value;
                    bugnoteList.Add(c);
                }
            }
            catch
            {
                Bugnote b = new Bugnote();
                b.id = 0;

                Models.Projects projects = new Models.Projects();
                foreach (Models.Project project in projects.projectList)
                {
                    if (project.ProjectId == 1)
                        b.Project = project;
                }
                b.name = "SAP implementation";
                b.client = "Clickdriven Co.";
                b.description = "The implementation needs better hardware support.";
                b.resolution = "April 28th";
                b.status = "not started";
                bugnoteList.Add(b);
            }
            return;
        }
        public bool Save()
        {
            try
            {
                XDocument doc = new XDocument();
                XElement elm = new XElement("bugnotes");
                foreach (Bugnote crs in bugnoteList)
                {
                    XElement b = new XElement("bugnote");
                    XElement p = new XElement("projectName");
                    p.Value = crs.Project.ProjectName;
                    b.Add(p);
                    XElement n = new XElement("name");
                    n.Value = crs.name;
                    b.Add(n);
                    XElement c = new XElement("client");
                    c.Value = crs.client;
                    b.Add(c);
                    XElement d = new XElement("description");
                    d.Value = crs.description;
                    b.Add(d);
                    XElement std = new XElement("resolution");
                    std.Value = crs.resolution;
                    b.Add(std);
                    XElement stt = new XElement("status");
                    stt.Value = crs.status;
                    b.Add(stt);
                    elm.Add(b);
                }
                doc.Add(elm);
                doc.Save(path);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}