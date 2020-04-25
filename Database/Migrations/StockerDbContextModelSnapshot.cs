﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stocker.Database;

namespace Stocker.Database.Migrations
{
    [DbContext(typeof(StockerDbContext))]
    partial class StockerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Stocker.Database.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<decimal>("MinorToMajorMultiplier")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Stocker.Database.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("StockExchangeId")
                        .HasColumnType("integer");

                    b.Property<string>("Ticker")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StockExchangeId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Stocker.Database.Models.StockExchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("StockExchanges");
                });

            modelBuilder.Entity("Stocker.Database.Models.StockTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("ConversionRate")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("LoggedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("StockExchangeId")
                        .HasColumnType("integer");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<int>("TradingPlatformId")
                        .HasColumnType("integer");

                    b.Property<int>("UserCurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StockExchangeId");

                    b.HasIndex("StockId");

                    b.HasIndex("TradingPlatformId");

                    b.HasIndex("UserCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("StockTransactions");
                });

            modelBuilder.Entity("Stocker.Database.Models.TradingPlatform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TradingPlatforms");
                });

            modelBuilder.Entity("Stocker.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Stocker.Database.Models.Stock", b =>
                {
                    b.HasOne("Stocker.Database.Models.StockExchange", "StockExchange")
                        .WithMany()
                        .HasForeignKey("StockExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stocker.Database.Models.StockExchange", b =>
                {
                    b.HasOne("Stocker.Database.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stocker.Database.Models.StockTransaction", b =>
                {
                    b.HasOne("Stocker.Database.Models.StockExchange", "StockExchange")
                        .WithMany()
                        .HasForeignKey("StockExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stocker.Database.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stocker.Database.Models.TradingPlatform", "TradingPlatform")
                        .WithMany()
                        .HasForeignKey("TradingPlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stocker.Database.Models.Currency", "UserCurrency")
                        .WithMany()
                        .HasForeignKey("UserCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stocker.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Stocker.Database.Models.Amount", "Commission", b1 =>
                        {
                            b1.Property<long>("StockTransactionId")
                                .HasColumnType("bigint");

                            b1.Property<int>("CurrencyId")
                                .HasColumnName("CommissionCurrencyId")
                                .HasColumnType("integer");

                            b1.Property<long>("ValueMinor")
                                .HasColumnName("CommissionValueMinor")
                                .HasColumnType("bigint");

                            b1.HasKey("StockTransactionId");

                            b1.HasIndex("CurrencyId");

                            b1.ToTable("StockTransactions");

                            b1.HasOne("Stocker.Database.Models.Currency", "Currency")
                                .WithMany()
                                .HasForeignKey("CurrencyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("StockTransactionId");
                        });

                    b.OwnsOne("Stocker.Database.Models.Amount", "PricePerUnit", b1 =>
                        {
                            b1.Property<long>("StockTransactionId")
                                .HasColumnType("bigint");

                            b1.Property<int>("CurrencyId")
                                .HasColumnName("PricePerUnitCurrencyId")
                                .HasColumnType("integer");

                            b1.Property<long>("ValueMinor")
                                .HasColumnName("PricePerUnitValueMinor")
                                .HasColumnType("bigint");

                            b1.HasKey("StockTransactionId");

                            b1.HasIndex("CurrencyId");

                            b1.ToTable("StockTransactions");

                            b1.HasOne("Stocker.Database.Models.Currency", "Currency")
                                .WithMany()
                                .HasForeignKey("CurrencyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("StockTransactionId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
