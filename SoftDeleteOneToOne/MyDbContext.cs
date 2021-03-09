using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftDeleteOneToOne.Infrastructure;
using SoftDeleteOneToOne.Models;

namespace SoftDeleteOneToOne
{
    public class MyDbContext : DbContext
    {
        private readonly string _connectionString;

        public MyDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser {Id = 1, Username = "Amund"});

            // Required for Identity
            base.OnModelCreating(modelBuilder);

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