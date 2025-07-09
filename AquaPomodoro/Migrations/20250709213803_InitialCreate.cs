using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AquaPomodoro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AquariumPomodoro");

            migrationBuilder.CreateTable(
                name: "fishes",
                schema: "AquariumPomodoro",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    gifURL = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fishes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "AquariumPomodoro",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    coinBalance = table.Column<int>(type: "integer", nullable: false),
                    totalFocusTime = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aquariums",
                schema: "AquariumPomodoro",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userID = table.Column<int>(type: "integer", nullable: false),
                    fishID = table.Column<int>(type: "integer", nullable: false),
                    addedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aquariums", x => x.id);
                    table.ForeignKey(
                        name: "FK_aquariums_fishes_fishID",
                        column: x => x.fishID,
                        principalSchema: "AquariumPomodoro",
                        principalTable: "fishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aquariums_users_userID",
                        column: x => x.userID,
                        principalSchema: "AquariumPomodoro",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aquariums_fishID",
                schema: "AquariumPomodoro",
                table: "aquariums",
                column: "fishID");

            migrationBuilder.CreateIndex(
                name: "IX_aquariums_userID",
                schema: "AquariumPomodoro",
                table: "aquariums",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_users_mail",
                schema: "AquariumPomodoro",
                table: "users",
                column: "mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aquariums",
                schema: "AquariumPomodoro");

            migrationBuilder.DropTable(
                name: "fishes",
                schema: "AquariumPomodoro");

            migrationBuilder.DropTable(
                name: "users",
                schema: "AquariumPomodoro");
        }
    }
}
