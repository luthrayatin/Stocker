using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stocker.Database.Models
{
    public class StockExchange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }

    public class StockExchangeConfiguration : IEntityTypeConfiguration<StockExchange>
    {
        public void Configure(EntityTypeBuilder<StockExchange> builder)
        {
            builder.ToTable("StockExchanges");
            builder.HasKey(se => se.Id);
            builder.Property(se => se.Id);
            builder.Property(se => se.Name);
            builder.Property(se => se.Country);
            builder.HasOne(se => se.Currency).WithMany().HasForeignKey(se => se.CurrencyId);
            builder.HasIndex(se => se.Name).IsUnique();
        }
    }
}