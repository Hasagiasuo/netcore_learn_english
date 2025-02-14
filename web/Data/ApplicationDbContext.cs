using Microsoft.EntityFrameworkCore;
using web.Models;


namespace web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Animals> Animals { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Fruits> Fruits { get; set; }
        public DbSet<Weather> Weather { get; set; }
        public DbSet<ImgAnimals> ImgAnimals { get; set; }
        public DbSet<ImgColors> ImgColors { get; set; }
        public DbSet<ImgFruits> ImgFruits { get; set; }
        public DbSet<ImgWeather> ImgWeather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ImgAnimals>()
                .HasOne(i => i.Animal)
                .WithMany()
                .HasForeignKey(i => i.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImgColors>()
                .HasOne(i => i.Color)
                .WithMany()
                .HasForeignKey(i => i.ColorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImgFruits>()
                .HasOne(i => i.Fruit)
                .WithMany()
                .HasForeignKey(i => i.FruitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImgWeather>()
              .HasOne(i => i.Weather)
              .WithMany()
              .HasForeignKey(i => i.WeatherId)
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
