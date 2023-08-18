using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostPalBackend.Migrations
{
	/// <inheritdoc />
	public partial class Comments : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Comments",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
					DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comments", x => x.Id);
					table.ForeignKey(
						name: "FK_Comments_Posts_PostId",
						column: x => x.PostId,
						principalTable: "Posts",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Comments_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Comments_PostId",
				table: "Comments",
				column: "PostId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_UserId",
				table: "Comments",
				column: "UserId");

			migrationBuilder.Sql(@"
				CREATE TRIGGER [dbo].[Comments_UPDATE] ON [dbo].[Comments]
					AFTER UPDATE
				AS
				BEGIN
					SET NOCOUNT ON;

					IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

					DECLARE @Id uniqueidentifier

					SELECT @Id = INSERTED.Id
					FROM INSERTED

					UPDATE dbo.Comments
					SET DateModified = GETDATE()
					WHERE Id= @Id
				END");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"DROP TRIGGER [dbo].[Comments_UPDATE]");

			migrationBuilder.DropTable(
				name: "Comments");
		}
	}
}
