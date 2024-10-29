using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using System.Reflection;

namespace RentACar.Data
{
    public class RentACarDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RentACarDbContext()
        {

        }
        public RentACarDbContext(DbContextOptions<RentACarDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration here, if needed
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<Make> Makes { get; set; } = null!;
        public virtual DbSet<VehicleType> VehicleTypes { get; set; } = null!;
        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Engine> Engines { get; set; } = null!;
        public virtual DbSet<Transmission> Transmissions { get; set; } = null!;
        public virtual DbSet<Rental> Rentals { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
    }
}
