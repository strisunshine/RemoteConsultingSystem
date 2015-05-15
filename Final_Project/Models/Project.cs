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
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName 
        { get; set; }

    }


        public class Projects
        {
            string path;

            public List<Project> projectList { get; set; }

            public Projects()
            {
                projectList = new List<Project>();
                path = HttpContext.Current.Server.MapPath("~\\App_Data\\Project.xml");
                Fill();
            }

            public void Fill()
            {
                try
                {
                    XDocument doc = XDocument.Load(path);

                    var q = from crs in doc.Elements("projects").Elements("project") select crs;
                    int i = 0;
                    foreach (var elem in q)
                    {
                        Project p = new Project();
                        p.ProjectId = ++i;
                        p.ProjectName = elem.Element("ProjectName").Value;
                        projectList.Add(p);
                    }
                }
                catch
                {
                    Project p = new Project();
                    p.ProjectId = 0;
                    p.ProjectName = "Sample Project";
                    projectList.Add(p);
                }
                return;
            }
            public bool Save()
            {
                try
                {
                    XDocument doc = new XDocument();
                    XElement elm = new XElement("projects");
                    foreach (Project crs in projectList)
                    {
                        XElement p = new XElement("project");
                        XElement i = new XElement("ProjectId");
                        XElement n = new XElement("ProjectName");
                        n.Value = crs.ProjectName;
                        p.Add(n);

                        elm.Add(p);
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