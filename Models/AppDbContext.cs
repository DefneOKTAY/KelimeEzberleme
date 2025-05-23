using Microsoft.EntityFrameworkCore;
using KullaniciWebApi.Models;
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

        // ✅ Yeni eklenen satır:
        public DbSet<WordProgress> WordProgresses => Set<WordProgress>();
    }
}
