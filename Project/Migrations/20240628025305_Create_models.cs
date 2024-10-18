using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class Create_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    KRS = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PESEL = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Offer = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", maxLength: 30, nullable: false),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CurrentVersion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurchasePricePerYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefreshTokenExp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    YearsToBuy = table.Column<int>(type: "int", nullable: false),
                    YearsToSupport = table.Column<int>(type: "int", nullable: false),
                    AlreadyPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAlreadyPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Software_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    AlreadyPayed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RenewalPeriodInMonth = table.Column<int>(type: "int", nullable: false),
                    RealesedPayments = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_Software_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Address", "Discriminator", "Email", "KRS", "Name", "TelephoneNumber" },
                values: new object[] { 1, "Warsaw", "ClientFirm", "qwerty@gmail.com", "95478902934512", "QWERTY", "785234542" });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Address", "Discriminator", "Email", "IsDeleted", "Name", "PESEL", "Surname", "TelephoneNumber" },
                values: new object[] { 2, "London", "ClientPhysical", "alanPo@gmail.com", false, "Alan", "74830591354", "Po", "123456789" });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Address", "Discriminator", "Email", "KRS", "Name", "TelephoneNumber" },
                values: new object[] { 3, "Tokyo", "ClientFirm", "zxcvbn@gmail.com", "902385195", "ZXCVBN", "203945813" });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Address", "Discriminator", "Email", "IsDeleted", "Name", "PESEL", "Surname", "TelephoneNumber" },
                values: new object[] { 4, "Paris", "ClientPhysical", "tonoEefs@gmail.com", false, "Edgar", "83591050021", "Ton", "987654321" });

            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "Id", "DateFrom", "DateTo", "Name", "Offer", "Value" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 8, 12), new DateOnly(2024, 11, 12), "Black Friday", 0, 10m },
                    { 2, new DateOnly(2024, 6, 25), new DateOnly(2024, 6, 29), "Super price", 2, 50m }
                });

            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "Id", "Category", "CurrentVersion", "Description", "Name", "PurchasePricePerYear" },
                values: new object[,]
                {
                    { 1, "Useful", "8.0.0+", "To block add", "AddBlock", 500m },
                    { 2, "Design", "RT+4", "Place to create design", "MyTerShop", 2700m },
                    { 3, "Education", "Full 1.0", "PLace to educate how create game", "GameThrone", 1500m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Login", "Password", "RefreshToken", "RefreshTokenExp", "RoleType", "Salt" },
                values: new object[,]
                {
                    { 1, "Jan", "GeKW1N5tv+++XgrvWsGGSZfhoEcTUhcJtmuDuuzlyUE=", "3J2v9xY98BjSXVuftl1oFsIqt9/cjfEQwLxu36CLoPQ=", new DateTime(2024, 6, 29, 4, 53, 4, 816, DateTimeKind.Local).AddTicks(6481), 1, "t8XlfA4dA9kf52Zb7Z1kgQ==" },
                    { 2, "Anton", "v5fYRtF1v73H9t9D4F4yUF6VkLn0/KnYwM3lI9Oami8=", "cBIpUtsi7kSKBsZFYv4OQywg4FbElSmLUozwjS2TJ7M=", new DateTime(2024, 6, 29, 4, 53, 4, 818, DateTimeKind.Local).AddTicks(577), 0, "BF5iMrZfyrYSJOx28OQz3Q==" }
                });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "AlreadyPaid", "DateFrom", "DateTo", "Desciption", "IdClient", "IdSoftware", "IsAlreadyPaid", "Price", "YearsToBuy", "YearsToSupport" },
                values: new object[,]
                {
                    { 1, 2400m, new DateOnly(2024, 3, 15), new DateOnly(2024, 3, 24), "Newest version", 1, 3, false, 5000m, 2, 2 },
                    { 2, 2700m, new DateOnly(2024, 4, 12), new DateOnly(2024, 4, 19), "All is good", 4, 2, true, 2700m, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "AlreadyPayed", "DateFrom", "IdClient", "IdSoftware", "IsCancelled", "Name", "Price", "RealesedPayments", "RenewalPeriodInMonth" },
                values: new object[,]
                {
                    { 1, 300m, new DateOnly(2024, 5, 27), 3, 2, false, "ExSub+", 300m, 1, 2 },
                    { 2, 494m, new DateOnly(2024, 2, 27), 3, 1, true, "MegaOka++", 260m, 2, 4 },
                    { 3, 1300m, new DateOnly(2023, 8, 27), 3, 2, false, "Turtle", 450m, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_IdClient",
                table: "Contract",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_IdSoftware",
                table: "Contract",
                column: "IdSoftware");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_IdClient",
                table: "Subscription",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_IdSoftware",
                table: "Subscription",
                column: "IdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Software");
        }
    }
}
