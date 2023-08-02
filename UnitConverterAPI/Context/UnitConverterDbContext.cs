using Microsoft.EntityFrameworkCore;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Model;
using UnitConverterAPI.Model.DbContext;

namespace UnitConverterAPI.Context
{
    public class UnitConverterDbContext : DbContext, IUnitConverterDbContextService
    {
        public UnitConverterDbContext(DbContextOptions<UnitConverterDbContext> options)
             : base(options)
        {

        }

        public DbSet<TemperatureConverterModel> TemperatureConverterDefinition { get; set; }
        public DbSet<WeightConverterModel> WeightConverterDefinition { get; set; }
        public DbSet<LengthConverterModel> LengthConverterDefinition { get; set; }
        public DbSet<TemperatureLoggerModel> ConverterLogger { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var LengthConverterConfig = modelBuilder.Entity<LengthConverterModel>();
            LengthConverterConfig.ToTable("LengthConverterDefinition");

            var TemperatureConverterConfig = modelBuilder.Entity<TemperatureConverterModel>();
            TemperatureConverterConfig.ToTable("TemperatureConverterDefinition");

            var WeightConverterConfig = modelBuilder.Entity<WeightConverterModel>();
            WeightConverterConfig.ToTable("WeightConverterDefinition");

            var TemperatureLoggerConfig = modelBuilder.Entity<TemperatureLoggerModel>();
            TemperatureLoggerConfig.ToTable("ConverterLogger");
        }
    }
}
