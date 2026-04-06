using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RepoLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantityMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OperationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MeasurementType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FromValue = table.Column<double>(type: "double precision", nullable: false),
                    FromUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ToValue = table.Column<double>(type: "double precision", nullable: false),
                    ToUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Result = table.Column<double>(type: "double precision", nullable: false),
                    ResultUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsError = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuantityMeasurements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_CreatedAt",
                table: "QuantityMeasurements",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_MeasurementType",
                table: "QuantityMeasurements",
                column: "MeasurementType");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_OperationType",
                table: "QuantityMeasurements",
                column: "OperationType");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_UserId",
                table: "QuantityMeasurements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "QuantityMeasurements");
            migrationBuilder.DropTable(name: "Users");
        }
    }
}
