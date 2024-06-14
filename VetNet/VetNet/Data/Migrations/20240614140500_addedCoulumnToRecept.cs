using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedCoulumnToRecept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "otvoren",
                table: "recepti",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otvoren",
                table: "recepti");
        }
    }
}
