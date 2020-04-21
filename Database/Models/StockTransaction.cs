using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stocker.Database.Models
{
    public class StockTransaction
    {
        public long Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }
        public Amount PricePerUnit { get; set; }
        public int StockExchangeId { get; set; }
        public StockExchange StockExchange { get; set; }
        public int TradingPlatformId { get; set; }
        public TradingPlatform TradingPlatform { get; set; }
        public DateTimeOffset Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserCurrencyId { get; set; }
        public Currency UserCurrency { get; set; }
        /// <summary>
        /// Conversion rate for converting user currency to stock currency for this transaction.
        /// </summary>
        /// <value></value>
        public decimal ConversionRate { get; set; }
        public DateTimeOffset LoggedAt { get; set; }
        public Amount Commission { get; set; }
    }

    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.ToTable("StockTransactions");
            builder.HasKey(st => st.Id);
            builder.Property(st => st.Id).ValueGeneratedOnAdd();
            builder.HasOne(st => st.Stock).WithMany(s => s.Transactions).HasForeignKey(st => st.StockId);
            builder.Property(st => st.Quantity).IsRequired();
            builder.OwnsOne(st => st.PricePerUnit, am =>
            {
                am.Property(a => a.ValueMinor).HasColumnName("PricePerUnitValueMinor").IsRequired();
                am.Property(a => a.CurrencyId).HasColumnName("PricePerUnitCurrencyId").IsRequired();
                am.HasOne(a => a.Currency).WithMany().HasForeignKey(a => a.CurrencyId);
            });
            builder.HasOne(st => st.StockExchange).WithMany().HasForeignKey(st => st.StockExchangeId);
            builder.HasOne(st => st.TradingPlatform).WithMany().HasForeignKey(st => st.TradingPlatformId);
            builder.Property(st => st.Date);
            builder.HasOne(st => st.User).WithMany().HasForeignKey(st => st.UserId);
            builder.HasOne(st => st.UserCurrency).WithMany().HasForeignKey(st => st.UserCurrencyId);
            builder.Property(st => st.ConversionRate).IsRequired();
            builder.Property(st => st.LoggedAt).IsRequired();
            builder.OwnsOne(st => st.Commission, am =>
            {
                am.Property(a => a.ValueMinor).HasColumnName("CommissionValueMinor");
                am.Property(a => a.CurrencyId).HasColumnName("CommissionCurrencyId");
                am.HasOne(a => a.Currency).WithMany().HasForeignKey(a => a.CurrencyId);
            });
        }
    }
}