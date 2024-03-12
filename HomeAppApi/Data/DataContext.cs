using HomeAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<House> Houses { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .HasMany<City>(s => s.Cities)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<State>()
                .HasMany<House>(s => s.Houses)
                .WithOne(h => h.State)
                .HasForeignKey(h => h.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasMany<House>(c => c.Houses)
                .WithOne(h => h.City)
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Owner>()
                .HasMany<House>(o => o.Houses)
                .WithOne(h => h.Owner)
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<State>()
                .HasData(
                    new State { StateId = 1, Name = "Illinois" },
                    new State { StateId = 2, Name = "California" }
                );


        }
    }
}
