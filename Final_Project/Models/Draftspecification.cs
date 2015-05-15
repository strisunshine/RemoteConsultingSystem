using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    
    public class Draftspecification
    {
        public int id { get; set; }
        public string Title
        { get; set; }
        public string CustomerName
        { get; set; }
        public string Topic
        { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date
        { get; set; }
        public List<string> RequirementsList
        { get; set; }
        public string Note
        { get; set; }
        public virtual Project Project
        { get; set; }
       
    }
   
}