using Microsoft.EntityFrameworkCore;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Model.DbContext;

namespace UnitConverterAPI.Context
{
    public class AuthenticationDbContext : DbContext, IAuthenticationDbContextService
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
                : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<AppModel> Apps { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var UsersConfig = modelBuilder.Entity<UserModel>();
            UsersConfig.ToTable("Users");
            var AppsConfig = modelBuilder.Entity<AppModel>();
            AppsConfig.ToTable("App");
            var CompaniesConfig = modelBuilder.Entity<CompanyModel>();
            CompaniesConfig.ToTable("Company");

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.Property(e => e.Username).IsRequired();
            });

        }
    }
}
