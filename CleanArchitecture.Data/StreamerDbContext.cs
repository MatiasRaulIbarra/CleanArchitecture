
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext :DbContext 
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source= DESKTOP-A9CLA51\SQLEXPRESS ; Initial Catalog= Streamer ; User Id=sa ; Password = 123456 ;TrustServerCertificate=True")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging();
        }

        //Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;TrustServerCertificate=True;
        public DbSet<Streamer> Streamers { get; set; }

        public DbSet<Video> Videos { get; set; }
    }
}
