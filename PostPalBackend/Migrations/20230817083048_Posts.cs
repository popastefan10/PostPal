using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostPalBackend.Migrations
{
	/// <inheritdoc />
	public partial class Posts : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Posts",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ImagesUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
					DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Posts", x => x.Id);
					table.ForeignKey(
						name: "FK_Posts_Profiles_ProfileId",
						column: x => x.ProfileId,
						principalTable: "Profiles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Posts_ProfileId",
				table: "Posts",
				column: "ProfileId");

			migrationBuilder.Sql(@"
				CREATE TRIGGER [dbo].[Posts_UPDATE] ON [dbo].[Posts]
					AFTER UPDATE
				AS
				BEGIN
					SET NOCOUNT ON;

					IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

					DECLARE @Id uniqueidentifier

					SELECT @Id = INSERTED.Id
					FROM INSERTED

					UPDATE dbo.Posts
					SET DateModified = GETDATE()
					WHERE Id= @Id
				END");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"DROP TRIGGER [dbo].[Posts_UPDATE]");

			migrationBuilder.DropTable(
				name: "Posts");
		}
	}
}
