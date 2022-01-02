using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

//class to represent the database entity for search history with on fields represented as property 

namespace NEAproject.Models
{
    public partial class Searchhistory
    {
        public int Searchid { get; set; }
        public string Userid { get; set; }
        public int? Valueofn { get; set; }
        public string Algorithmname { get; set; }
        public DateTime? Timecreated { get; set; }
        public string Pagename { get; set; }

        //asp user property as virtual to represent the relationship of foreign key.
        public virtual AspNetUsers User { get; set; }
    }
}
