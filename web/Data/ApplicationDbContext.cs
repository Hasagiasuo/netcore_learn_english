using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Word>()
                .Property(w => w.English)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Word>()
                .Property(w => w.Ukrainian)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Word>()
                .Property(w => w.Category)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
