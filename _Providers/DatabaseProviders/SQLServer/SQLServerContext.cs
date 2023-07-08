using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace _Providers.DatabaseProviders.SQLServer
{
    public class SQLServerContext<T> : DbContext where T : Entity
    {
        public SQLServerContext(DbContextOptions<SQLServerContext<T>> options) : base(options)
        {
        }

        public DbSet<T> Entity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().ToTable(typeof(T).Name); 
        }
    }
}
