using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class Final_ProjectContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatDefault1ically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Final_ProjectContext() : base("name=Final_ProjectContext")
        {
        }


        public System.Data.Entity.DbSet<Final_Project.Models.Draftspecification> Draftspecifications { get; set; }

        public System.Data.Entity.DbSet<Final_Project.Models.Meetingschedule> Meetingschedules { get; set; }

        public System.Data.Entity.DbSet<Final_Project.Models.Project> Projects { get; set; }
    
    }
}
