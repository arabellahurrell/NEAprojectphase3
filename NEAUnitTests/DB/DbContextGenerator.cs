﻿
using Microsoft.Extensions.Options;

namespace NEAUnitTests.DB
{
    public class DbContextGenerator : IDbContextGenerator
    {
        private readonly IOptions<Domain.AppSettings> options;
        
        public DbContextGenerator(IOptions<Domain.AppSettings> options)
        {
            this.options = options;
        }

        public IMyDbContext GenerateMyDbContext()
        {
            Log.Debug("My Db Context Created with Connection String: " + options.Value.ConnString);
            return new MyDbContext(options.Value.ConnString);
        }

    }
}
