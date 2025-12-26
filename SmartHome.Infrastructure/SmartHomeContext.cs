using Microsoft.EntityFrameworkCore; // Це підключить DbContext, DbSet та ModelBuilder
using SmartHome.Infrastructure.Models;

namespace SmartHome.Infrastructure
{
    public class SmartHomeContext : DbContext
    {
        public DbSet<SmartDeviceModel> Devices { get; set; }
        public DbSet<LightModel> Lights { get; set; }
        public DbSet<ThermostatModel> Thermostats { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<WarrantyModel> Warranties { get; set; }
        public DbSet<GroupModel> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=smarthome.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Налаштування TPT (Table-per-Type)
            modelBuilder.Entity<SmartDeviceModel>().ToTable("Devices");
            modelBuilder.Entity<LightModel>().ToTable("Lights");
            modelBuilder.Entity<ThermostatModel>().ToTable("Thermostats");

            // Налаштування 1:1 (Warranty - Device)
            modelBuilder.Entity<SmartDeviceModel>()
                .HasOne(d => d.Warranty)
                .WithOne(w => w.Device)
                .HasForeignKey<WarrantyModel>(w => w.SmartDeviceModelId);
        }
    }
}