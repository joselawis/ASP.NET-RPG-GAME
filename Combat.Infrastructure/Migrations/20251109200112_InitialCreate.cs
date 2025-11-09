using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Combat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeroId = table.Column<Guid>(type: "uuid", nullable: false),
                    HeroName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HeroType = table.Column<string>(type: "text", nullable: false),
                    HeroHealth = table.Column<int>(type: "integer", nullable: false),
                    HeroAttack = table.Column<int>(type: "integer", nullable: false),
                    HeroDefense = table.Column<int>(type: "integer", nullable: false),
                    HeroSpeed = table.Column<int>(type: "integer", nullable: false),
                    EnemyId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnemyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EnemyType = table.Column<string>(type: "text", nullable: false),
                    EnemyHealth = table.Column<int>(type: "integer", nullable: false),
                    EnemyAttack = table.Column<int>(type: "integer", nullable: false),
                    EnemyDefense = table.Column<int>(type: "integer", nullable: false),
                    EnemySpeed = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Turns = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Combats_StartedAt",
                table: "Combats",
                column: "StartedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Combats");
        }
    }
}