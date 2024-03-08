using HomeAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<House> Houses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<HouseOwner> HouseOwners { get; set; }
        public DbSet<HouseCategory> HouseCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseCategory>()
                .HasKey(hc => new { hc.HouseId, hc.CategoryId });

            modelBuilder.Entity<HouseCategory>()
                .HasOne(h => h.House)
                .WithMany(hc => hc.HouseCategories)
                .HasForeignKey(h => h.HouseId);

            modelBuilder.Entity<HouseCategory>()
                .HasOne(h => h.Category)
                .WithMany(hc => hc.HouseCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<HouseOwner>()
                .HasKey(ho => new { ho.HouseId, ho.OwnerId });

            modelBuilder.Entity<HouseOwner>()
                .HasOne(h => h.House)
                .WithMany(ho => ho.HouseOwners)
                .HasForeignKey(h => h.HouseId);

            modelBuilder.Entity<HouseOwner>()
                .HasOne(h => h.Owner)
                .WithMany(ho => ho.HouseOwners)
                .HasForeignKey(o => o.OwnerId);
        }
    }
}
