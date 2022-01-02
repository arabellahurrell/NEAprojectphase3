using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NEAproject.Data
{
    //main application database context.
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        //parameterised constructor which holds the super class constructor with parameter for database context options.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            //calling parent class contructor
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
            //overidden method which will build the model  
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
