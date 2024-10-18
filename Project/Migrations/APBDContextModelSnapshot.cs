﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Context;

#nullable disable

namespace Project.Migrations
{
    [DbContext(typeof(APBDContext))]
    partial class APBDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project.Model.AbstractClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Client", (string)null);

                    b.HasDiscriminator().HasValue("AbstractClient");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Project.Model.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AlreadyPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("DateFrom")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DateTo")
                        .HasColumnType("date");

                    b.Property<string>("Desciption")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.Property<bool>("IsAlreadyPaid")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("YearsToBuy")
                        .HasColumnType("int");

                    b.Property<int>("YearsToSupport")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Contract", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlreadyPaid = 2400m,
                            DateFrom = new DateOnly(2024, 3, 15),
                            DateTo = new DateOnly(2024, 3, 24),
                            Desciption = "Newest version",
                            IdClient = 1,
                            IdSoftware = 3,
                            IsAlreadyPaid = false,
                            Price = 5000m,
                            YearsToBuy = 2,
                            YearsToSupport = 2
                        },
                        new
                        {
                            Id = 2,
                            AlreadyPaid = 2700m,
                            DateFrom = new DateOnly(2024, 4, 12),
                            DateTo = new DateOnly(2024, 4, 19),
                            Desciption = "All is good",
                            IdClient = 4,
                            IdSoftware = 2,
                            IsAlreadyPaid = true,
                            Price = 2700m,
                            YearsToBuy = 1,
                            YearsToSupport = 0
                        });
                });

            modelBuilder.Entity("Project.Model.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateFrom")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DateTo")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Offer")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasMaxLength(30)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Discount", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateFrom = new DateOnly(2024, 8, 12),
                            DateTo = new DateOnly(2024, 11, 12),
                            Name = "Black Friday",
                            Offer = 0,
                            Value = 10m
                        },
                        new
                        {
                            Id = 2,
                            DateFrom = new DateOnly(2024, 6, 25),
                            DateTo = new DateOnly(2024, 6, 29),
                            Name = "Super price",
                            Offer = 2,
                            Value = 50m
                        });
                });

            modelBuilder.Entity("Project.Model.Software", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CurrentVersion")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PurchasePricePerYear")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Software", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Useful",
                            CurrentVersion = "8.0.0+",
                            Description = "To block add",
                            Name = "AddBlock",
                            PurchasePricePerYear = 500m
                        },
                        new
                        {
                            Id = 2,
                            Category = "Design",
                            CurrentVersion = "RT+4",
                            Description = "Place to create design",
                            Name = "MyTerShop",
                            PurchasePricePerYear = 2700m
                        },
                        new
                        {
                            Id = 3,
                            Category = "Education",
                            CurrentVersion = "Full 1.0",
                            Description = "PLace to educate how create game",
                            Name = "GameThrone",
                            PurchasePricePerYear = 1500m
                        });
                });

            modelBuilder.Entity("Project.Model.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AlreadyPayed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("DateFrom")
                        .HasColumnType("date");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RealesedPayments")
                        .HasColumnType("int");

                    b.Property<int>("RenewalPeriodInMonth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Subscription", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlreadyPayed = 300m,
                            DateFrom = new DateOnly(2024, 5, 27),
                            IdClient = 3,
                            IdSoftware = 2,
                            IsCancelled = false,
                            Name = "ExSub+",
                            Price = 300m,
                            RealesedPayments = 1,
                            RenewalPeriodInMonth = 2
                        },
                        new
                        {
                            Id = 2,
                            AlreadyPayed = 494m,
                            DateFrom = new DateOnly(2024, 2, 27),
                            IdClient = 3,
                            IdSoftware = 1,
                            IsCancelled = true,
                            Name = "MegaOka++",
                            Price = 260m,
                            RealesedPayments = 2,
                            RenewalPeriodInMonth = 4
                        },
                        new
                        {
                            Id = 3,
                            AlreadyPayed = 1300m,
                            DateFrom = new DateOnly(2023, 8, 27),
                            IdClient = 3,
                            IdSoftware = 2,
                            IsCancelled = false,
                            Name = "Turtle",
                            Price = 450m,
                            RealesedPayments = 3,
                            RenewalPeriodInMonth = 3
                        });
                });

            modelBuilder.Entity("Project.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("RefreshTokenExp")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "Jan",
                            Password = "GeKW1N5tv+++XgrvWsGGSZfhoEcTUhcJtmuDuuzlyUE=",
                            RefreshToken = "3J2v9xY98BjSXVuftl1oFsIqt9/cjfEQwLxu36CLoPQ=",
                            RefreshTokenExp = new DateTime(2024, 6, 29, 4, 53, 4, 816, DateTimeKind.Local).AddTicks(6481),
                            RoleType = 1,
                            Salt = "t8XlfA4dA9kf52Zb7Z1kgQ=="
                        },
                        new
                        {
                            Id = 2,
                            Login = "Anton",
                            Password = "v5fYRtF1v73H9t9D4F4yUF6VkLn0/KnYwM3lI9Oami8=",
                            RefreshToken = "cBIpUtsi7kSKBsZFYv4OQywg4FbElSmLUozwjS2TJ7M=",
                            RefreshTokenExp = new DateTime(2024, 6, 29, 4, 53, 4, 818, DateTimeKind.Local).AddTicks(577),
                            RoleType = 0,
                            Salt = "BF5iMrZfyrYSJOx28OQz3Q=="
                        });
                });

            modelBuilder.Entity("Project.Model.ClientFirm", b =>
                {
                    b.HasBaseType("Project.Model.AbstractClient");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasDiscriminator().HasValue("ClientFirm");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Warsaw",
                            Email = "qwerty@gmail.com",
                            Name = "QWERTY",
                            TelephoneNumber = "785234542",
                            KRS = "95478902934512"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Tokyo",
                            Email = "zxcvbn@gmail.com",
                            Name = "ZXCVBN",
                            TelephoneNumber = "203945813",
                            KRS = "902385195"
                        });
                });

            modelBuilder.Entity("Project.Model.ClientPhysical", b =>
                {
                    b.HasBaseType("Project.Model.AbstractClient");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasDiscriminator().HasValue("ClientPhysical");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Address = "London",
                            Email = "alanPo@gmail.com",
                            Name = "Alan",
                            TelephoneNumber = "123456789",
                            IsDeleted = false,
                            PESEL = "74830591354",
                            Surname = "Po"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Paris",
                            Email = "tonoEefs@gmail.com",
                            Name = "Edgar",
                            TelephoneNumber = "987654321",
                            IsDeleted = false,
                            PESEL = "83591050021",
                            Surname = "Ton"
                        });
                });

            modelBuilder.Entity("Project.Model.Contract", b =>
                {
                    b.HasOne("Project.Model.AbstractClient", "AbstractClient")
                        .WithMany("Contracts")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Project.Model.Software", "Software")
                        .WithMany("Contracts")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AbstractClient");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Project.Model.Subscription", b =>
                {
                    b.HasOne("Project.Model.AbstractClient", "AbstractClient")
                        .WithMany("Subscriptions")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Project.Model.Software", "Software")
                        .WithMany("Subscriptions")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AbstractClient");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Project.Model.AbstractClient", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("Project.Model.Software", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}