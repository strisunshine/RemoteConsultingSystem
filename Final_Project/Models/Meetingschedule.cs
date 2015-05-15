using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Final_Project.Models
{
    
    public class Meetingschedule
    {
        public int id { get; set; }

        public string title
        { get; set; }
        public DateTime start
        { get; set; }
        public DateTime end
        { get; set; }
        public bool allDay
        { get; set; }
        public string url
        { get; set; }
        public string type
        { get; set; }
    }
    public class Meetingschedules
    {
        public bool convertoJson()
        {
           
            return true;
        }
    }
    
}