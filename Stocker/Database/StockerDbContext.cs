using Microsoft.EntityFrameworkCore;
using Stocker.Database.Models;

namespace Stocker.Database
{
    public class StockerDbContext : DbContext
    {
        public StockerDbContext(DbContextOptions options) : base(options)
        {
        }

        public StockerDbContext()
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockExchange> StockExchanges { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TradingPlatform> TradingPlatforms { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}