using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Final_Project.Models
{
    
    public class Businessnote
    {
        public int id { get; set; }

        [DisplayName("BusinessnoteID")]
        [Required(ErrorMessage = "It should be interger")]
        public int noteid { get; set; }

        [DisplayName("title")]
        [Required(ErrorMessage = "Enter the title of businessnote")]
        public string title { get; set; }

        [DisplayName("content")]

        public string content { get; set; }

        [DisplayName("notetime")]
        public string notetime { get; set; }

    }
    public class Businessnotes
    {
        string path;

        public List<Businessnote> businessnoteList { get; set; }

        public Businessnotes()
        {
            businessnoteList = new List<Businessnote>();
            path = HttpContext.Current.Server.MapPath("~\\App_Data\\Businessnotes.xml");
            Fill();
        }

        public void Fill()
        {
            try
            {
                XDocument doc = XDocument.Load(path);

                var q = from crs in doc.Elements("businessnotes").Elements("businessnote") select crs;
                int i = 0;
                foreach (var elem in q)
                {
                    Businessnote b = new Businessnote();
                    b.id = ++i;
                    b.noteid = int.Parse(elem.Element("noteid").Value);
                    b.title = elem.Element("title").Value;
                    b.content = elem.Element("content").Value;
                    b.notetime = elem.Element("notetime").Value;
                    businessnoteList.Add(b);
                }
            }
            catch
            {
                Businessnote b = new Businessnote();
                b.id = 0;
                b.noteid = 1001;
                b.title = "The Small Business Jobs Act";
                b.content = "Small businesses create most of the jobs in America. But since the 'Great Recession' began in December 2007, over 6 million of those small business jobs have been lost. The Small Business Jobs Act signed into law by President Obama in September 2010 includes both temporary and long-lasting provisions that aim to help create 500,000 new jobs.";
                b.notetime = "10/09/2013";
                businessnoteList.Add(b);
            }
            return;
        }
        public bool Save()
        {
            try
            {
                XDocument doc = new XDocument();
                XElement elm = new XElement("businessnotes");
                foreach (Businessnote crs in businessnoteList)
                {
                    XElement b = new XElement("businessnote");
                    XElement n = new XElement("noteid");
                    n.Value = crs.noteid.ToString();
                    b.Add(n);
                    XElement t = new XElement("title");
                    t.Value = crs.title;
                    b.Add(t);
                    XElement c = new XElement("content");
                    c.Value = crs.content;
                    b.Add(c);
                    XElement ntt = new XElement("notetime");
                    ntt.Value = crs.notetime;
                    b.Add(ntt);
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