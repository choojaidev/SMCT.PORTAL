using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMCTPortal.Model.SMPeople;
using SMCTPortal.Model.SMTCitizen;
using System;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace SMCTPortal.Model
{
    public class SMCTDbContext : DbContext
    {
    
        public SMCTDbContext(DbContextOptions<SMCTDbContext> options)
             : base(options)
        {
        }

        public DbSet<tbPeople> tbPeople { get; set; }
    }
}
