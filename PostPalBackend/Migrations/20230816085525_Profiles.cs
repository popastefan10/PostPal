using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostPalBackend.Migrations
{
	/// <inheritdoc />
	public partial class Profiles : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Profiles",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
					DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Profiles", x => x.Id);
					table.ForeignKey(
						name: "FK_Profiles_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Profiles_UserId",
				table: "Profiles",
				column: "UserId",
				unique: true);

			migrationBuilder.Sql(@"
				CREATE TRIGGER [dbo].[Profiles_UPDATE] ON [dbo].[Profiles]
					AFTER UPDATE
				AS
				BEGIN
					SET NOCOUNT ON;

					IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

					DECLARE @Id uniqueidentifier

					SELECT @Id = INSERTED.Id
					FROM INSERTED

					UPDATE dbo.Profiles
					SET DateModified = GETDATE()
					WHERE Id= @Id
				END");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"DROP TRIGGER [dbo].[Profiles_UPDATE]");

			migrationBuilder.DropTable(
				name: "Profiles");
		}
	}
}
