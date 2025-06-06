﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Inked.Ordering.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingContext))]
    [Migration("20231021004633_FixOrderitemseqSchema")]
    partial class FixOrderitemseqSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ordering")
                .HasAnnotation("ProductVersion", "8.0.0-rc.2.23480.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("buyerseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderitemseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("paymentseq")
                .IncrementsBy(10);

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "buyerseq");

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdentityGuid")
                        .IsUnique();

                    b.ToTable("buyers", "ordering");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("cardtypes", "ordering");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "paymentseq");

                    b.Property<int>("BuyerId")
                        .HasColumnType("integer");

                    b.Property<string>("_alias")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Alias");

                    b.Property<string>("_cardHolderName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("CardHolderName");

                    b.Property<string>("_cardNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("CardNumber");

                    b.Property<int>("_cardTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("CardTypeId");

                    b.Property<DateTime>("_expiration")
                        .HasMaxLength(25)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Expiration");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("_cardTypeId");

                    b.ToTable("paymentmethods", "ordering");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderseq");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("_buyerId")
                        .HasColumnType("integer")
                        .HasColumnName("BuyerId");

                    b.Property<DateTime>("_orderDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("OrderDate");

                    b.Property<int>("_orderStatusId")
                        .HasColumnType("integer")
                        .HasColumnName("OrderStatusId");

                    b.Property<int?>("_paymentMethodId")
                        .HasColumnType("integer")
                        .HasColumnName("PaymentMethodId");

                    b.HasKey("Id");

                    b.HasIndex("_buyerId");

                    b.HasIndex("_orderStatusId");

                    b.HasIndex("_paymentMethodId");

                    b.ToTable("orders", "ordering");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderitemseq");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<decimal>("_discount")
                        .HasColumnType("numeric")
                        .HasColumnName("Discount");

                    b.Property<string>("_pictureUrl")
                        .HasColumnType("text")
                        .HasColumnName("PictureUrl");

                    b.Property<string>("_productName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ProductName");

                    b.Property<decimal>("_unitPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("UnitPrice");

                    b.Property<int>("_units")
                        .HasColumnType("integer")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems", "ordering");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("orderstatus", "ordering");
                });

            modelBuilder.Entity("eShop.Ordering.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("requests", "ordering");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("_cardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardType");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("_buyerId");

                    b.HasOne(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("_orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("_paymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("text");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders", "ordering");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Navigation("PaymentMethods");
                });

            modelBuilder.Entity(" eShop.Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
