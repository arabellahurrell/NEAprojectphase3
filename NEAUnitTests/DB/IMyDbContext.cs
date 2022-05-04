using Microsoft.EntityFrameworkCore;
using System;

namespace NEAUnitTests.DB
{
    public interface IMyDbContext : IDisposable
    {
        int SaveChanges();
        DbSet<Domain.Entity> Entities { get; set; }
    }
}
