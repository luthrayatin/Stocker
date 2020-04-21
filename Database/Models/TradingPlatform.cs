using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stocker.Database.Models
{
    public class TradingPlatform
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TradingPlatformConfiguration : IEntityTypeConfiguration<TradingPlatform>
    {
        public void Configure(EntityTypeBuilder<TradingPlatform> builder)
        {
            builder.ToTable("TradingPlatforms");
            builder.HasKey(tp => tp.Id);
            builder.Property(tp => tp.Id);
            builder.Property(tp => tp.Name);
        }
    }
}