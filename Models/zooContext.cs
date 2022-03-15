using Microsoft.EntityFrameworkCore;

namespace zoo.Models
{
    public class zooContext:DbContext
    {
        public DbSet<Vrt> Vrtovi{get;set;}
        public DbSet<Lokacija> Lokacije{get;set;}
        
        public zooContext(DbContextOptions options):base(options)
        {

        }

    }
}