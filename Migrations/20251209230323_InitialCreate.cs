using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SSN = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    UserType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UserType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Accommodation = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Bedrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    Bathrooms = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    BuiltYear = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ResidencePicture = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.ResidenceId);
                    table.ForeignKey(
                        name: "FK_Residences_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReservationEndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "ResidenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "DOB", "Email", "Name", "Password", "PhoneNumber", "SSN", "UserType" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@airbb.com", "Admin User", "adminpass", "1234567890", "111-11-1111", "Admin" },
                    { 2, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "client@airbb.com", "Client User", "clientpass", "9876543210", "222-22-2222", "Client" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name", "Region" },
                values: new object[,]
                {
                    { 1, "Chicago", "Illinois" },
                    { 2, "New York", "New York" },
                    { 3, "Miami", "Florida" },
                    { 4, "Atlanta", "Georgia" }
                });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "ResidenceId", "Accommodation", "Bathrooms", "Bedrooms", "BuiltYear", "GuestNumber", "LocationId", "Name", "OwnerId", "PricePerNight", "ResidencePicture" },
                values: new object[,]
                {
                    { 1, "Full Apartment", 2m, 2, 2000, 4, 1, "Chicago Loop Apartment", 1, 180m, "chi1.jpg" },
                    { 2, "Condo Suite", 1m, 1, 2005, 2, 1, "River North Condo", 1, 140m, "chi2.jpg" },
                    { 3, "Loft", 1m, 1, 2010, 3, 2, "Midtown Loft", 2, 220m, "ny1.jpg" },
                    { 4, "Brownstone Home", 1m, 2, 1995, 4, 2, "Brooklyn Brownstone", 2, 200m, "ny2.jpg" },
                    { 5, "Beach House", 2m, 3, 2012, 6, 3, "Miami Beach House", 3, 260m, "miami1.jpg" },
                    { 6, "Condo", 1m, 1, 2015, 2, 3, "South Beach Condo", 3, 180m, "miami2.jpg" },
                    { 7, "Family House", 2m, 3, 2003, 5, 4, "Suburban Family Home", 4, 190m, "atlanta1.jpg" },
                    { 8, "Apartment", 1m, 2, 2008, 3, 4, "Downtown Apartment", 4, 160m, "atlanta2.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ResidenceId",
                table: "Reservations",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_LocationId",
                table: "Residences",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
