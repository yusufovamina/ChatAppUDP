using Microsoft.EntityFrameworkCore;
using System;

namespace ChatAppUDP
{

    public  class AppContext : DbContext
    {
        public DbSet<ConnectionInfo> ConnectionInfos { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-0LP9EBH; Initial Catalog = ConnectionsIP; TrustServerCertificate=true; Integrated Security = true;");
        }
    }   //"Data Source = DESKTOP-0LP9EBH; Initial Catalog = Umico; TrustServerCertificate=true; Integrated Security = true;"
        //"Data Source = STHQ012E-09; Database=Umico; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;"

}