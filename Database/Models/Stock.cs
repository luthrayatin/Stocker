using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stocker.Database.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public int StockExchangeId { get; set; }
        public StockExchange StockExchange { get; set; }
        public IEnumerable<StockTransaction> Transactions { get; set; }
    }

    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id);
            builder.Property(s => s.Name);
            builder.Property(s => s.Ticker);
            builder.HasOne(s => s.StockExchange).WithMany().HasForeignKey(s => s.StockExchangeId);
            builder.HasMany(s => s.Transactions).WithOne(st => st.Stock);
        }
    }
}