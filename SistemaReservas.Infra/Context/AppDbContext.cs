using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaReservas.Domain.Entities; 

namespace SistemaReservas.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");

            builder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.PricePerNight).HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Host)
                      .WithMany(u => u.Properties)
                      .HasForeignKey(p => p.HostId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings");
                entity.HasKey(b => b.Id);

                entity.Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");

                entity.HasOne(b => b.Property)
                      .WithMany(p => p.Bookings)
                      .HasForeignKey(b => b.PropertyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Guest)
                      .WithMany(u => u.Bookings)
                      .HasForeignKey(b => b.GuestId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}