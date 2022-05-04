
using Microsoft.EntityFrameworkCore;
using Moq;
using NEAproject.Controllers;
using NEAproject.Models;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NEAUnitTests
{
    public class searchhistorytest
    {
        List<Searchhistory> entities;
        //[SetUp]
        public void setup()
        {
            entities = new List<Searchhistory>();
            entities.Add(new Searchhistory() { Algorithmname = "", Pagename = "Comparison", Timecreated = DateTime.Now, Userid = "372b8302-cabf-40a3-a0a6-b2177260ff3d", Valueofn = 7 });
            entities.Add(new Searchhistory() { Algorithmname = "getNtothe2points", Pagename = "Index", Timecreated = DateTime.Now, Userid = "372b8302-cabf-40a3-a0a6-b2177260ff3d", Valueofn = 7 });
            var dbmock = new Mock<NEAdbContext>();
            dbmock.Setup(x => x.Searchhistory).Returns(dbcontextmock.GetQueryableMockDbSet<Searchhistory>(entities));
            dbmock.Setup(x => x.SaveChanges()).Returns(1);
            //create entity and then we will assert the values to test that they have inserted correctly 
            //var n = new Mock<IDbContextGenerator>();
        }
        
        [Fact]
        public void tester()
        {
            var option = new DbContextOptionsBuilder<NEAdbContext>().UseInMemoryDatabase(databaseName:"NEAdb").Options;
            using (var dbcontext = new NEAdbContext(option))
            {
                dbcontext.Searchhistory.Add(new Searchhistory() { Searchid = 100, Algorithmname = "", Pagename = "Comparison", Timecreated = DateTime.Now, Userid = "372b8302-cabf-40a3-a0a6-b2177260ff3d", Valueofn = 7 });
                dbcontext.SaveChanges();

            }
            using(var dbcontext = new NEAdbContext(option))
            {
                SearchHistoryDetailsController c = new SearchHistoryDetailsController(dbcontext);
                var result = (c.OnGetAsync(100).Result) as Microsoft.AspNetCore.Mvc.ObjectResult;
                var actualresult = result.Value;
                Assert.Equal(100, ((Searchhistory)actualresult).Searchid);
                Assert.Equal("Comparison", ((Searchhistory)actualresult).Pagename);
            }
        }
    }
}
