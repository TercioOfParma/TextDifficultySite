using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextDifficultyDeterminer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MySQLNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    LanguageName = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    FrequencyWordId = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    Language = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    Word = table.Column<string>(type: "longtext", nullable: false),
                    FrequencyOfWord = table.Column<long>(type: "bigint", nullable: false),
                    DifficultyScore = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.FrequencyWordId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
