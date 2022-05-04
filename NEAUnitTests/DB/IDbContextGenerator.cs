using System;
using System.Collections.Generic;
using System.Text;

namespace NEAUnitTests.DB
{
    public interface IDbContextGenerator
    {
        IMyDbContext GenerateMyDbContext();
    }
}
