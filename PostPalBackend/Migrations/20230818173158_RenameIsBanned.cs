using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostPalBackend.Migrations
{
    /// <inheritdoc />
    public partial class RenameIsBanned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isBanned",
                table: "Users",
                newName: "IsBanned");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBanned",
                table: "Users",
                newName: "isBanned");
        }
    }
}
