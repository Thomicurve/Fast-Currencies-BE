﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fast_currencies_be;

#nullable disable

namespace fast_currencies_be.Migrations
{
    [DbContext(typeof(FastCurrenciesContext))]
    [Migration("20231015162544_currencyRate_added")]
    partial class currencyRate_added
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("fast_currencies_be.ConvertionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyFromId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyToId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.HasIndex("UserId");

                    b.ToTable("ConvertionHistory", (string)null);
                });

            modelBuilder.Entity("fast_currencies_be.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Currencies", (string)null);
                });

            modelBuilder.Entity("fast_currencies_be.CurrencyRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyDestinationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyOriginId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Rate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyDestinationId");

                    b.HasIndex("CurrencyOriginId");

                    b.ToTable("CurrencyRates", (string)null);
                });

            modelBuilder.Entity("fast_currencies_be.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentRequests")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LastRequestMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Requests", (string)null);
                });

            modelBuilder.Entity("fast_currencies_be.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxRequests")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions", (string)null);
                });

            modelBuilder.Entity("fast_currencies_be.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("fast_currencies_be.ConvertionHistory", b =>
                {
                    b.HasOne("fast_currencies_be.Currency", "CurrencyFrom")
                        .WithMany()
                        .HasForeignKey("CurrencyFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fast_currencies_be.Currency", "CurrencyTo")
                        .WithMany()
                        .HasForeignKey("CurrencyToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fast_currencies_be.User", "User")
                        .WithMany("ConvertionHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrencyFrom");

                    b.Navigation("CurrencyTo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fast_currencies_be.CurrencyRate", b =>
                {
                    b.HasOne("fast_currencies_be.Currency", "CurrencyDestination")
                        .WithMany()
                        .HasForeignKey("CurrencyDestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fast_currencies_be.Currency", "CurrencyOrigin")
                        .WithMany()
                        .HasForeignKey("CurrencyOriginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrencyDestination");

                    b.Navigation("CurrencyOrigin");
                });

            modelBuilder.Entity("fast_currencies_be.Request", b =>
                {
                    b.HasOne("fast_currencies_be.User", null)
                        .WithOne("Request")
                        .HasForeignKey("fast_currencies_be.Request", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fast_currencies_be.User", b =>
                {
                    b.HasOne("fast_currencies_be.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("fast_currencies_be.Subscription", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("fast_currencies_be.User", b =>
                {
                    b.Navigation("ConvertionHistories");

                    b.Navigation("Request")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
