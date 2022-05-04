using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEAproject.Models
{
    public class TestDatadb
    {
        public TestDatadb()
        {

        }
        public void seed(NEAdbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Searchhistory.AddRange(new Searchhistory() {Algorithmname = "", Pagename ="Comparison" , Timecreated = DateTime.Now, Userid = "372b8302-cabf-40a3-a0a6-b2177260ff3d", Valueofn = 7},
                new Searchhistory() { Algorithmname = "getNtothe2points", Pagename = "Index", Timecreated= DateTime.Now, Userid = "372b8302-cabf-40a3-a0a6-b2177260ff3d", Valueofn = 7 });
            context.SaveChanges();
        }
    }
}
