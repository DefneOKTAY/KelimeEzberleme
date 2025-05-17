using Microsoft.EntityFrameworkCore;

namespace KullaniciWebApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Word> Words => Set<Word>();
        public DbSet<WordSample> WordSamples => Set<WordSample>();
    }
}

