using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEAUnitTests
{
    public static class dbcontextmock
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> source) where T:class
        {
            var querable = source.AsQueryable();
            var dbset = new Mock<DbSet<T>>();
            dbset.As<IQueryable<T>>().Setup(x => x.Provider).Returns(querable.Provider);
            dbset.As<IQueryable<T>>().Setup(x => x.Expression).Returns(querable.Expression);
            dbset.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(querable.ElementType);
            dbset.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(querable.GetEnumerator());
            dbset.Setup(x => x.Add(It.IsAny<T>())).Callback<T>((alpha) => source.Add(alpha));
            return dbset.Object;
        }

    }
}
