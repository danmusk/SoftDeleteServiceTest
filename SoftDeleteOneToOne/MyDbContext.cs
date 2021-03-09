using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftDeleteOneToOne.Infrastructure;
using SoftDeleteOneToOne.Models;

namespace SoftDeleteOneToOne
{
    public class MyDbContext : DbContext
    {
        private readonly string _connectionString;

        public MyDbContext()
        {
            _connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=MyLocalDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser {Id = 1, Username = "Amund"});

            // Add all EntityConfigurations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

			// Disable CascadeDelete
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Enable SoftDelete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISingleSoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSingleSoftDeleteQueryFilter();
                }
            }

        }
    }
}