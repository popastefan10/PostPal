using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostPalBackend.Migrations
{
	/// <inheritdoc />
	public partial class UsersDateFieldsGeneration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>(
				name: "DateCreated",
				table: "Users",
				type: "datetime2",
				nullable: true,
				defaultValueSql: "getdate()",
				oldClrType: typeof(DateTime),
				oldType: "datetime2",
				oldNullable: true);

			migrationBuilder.Sql(@"
				CREATE TRIGGER [dbo].[Users_UPDATE] ON [dbo].[Users]
					AFTER UPDATE
				AS
				BEGIN
					SET NOCOUNT ON;

					IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

					DECLARE @Id uniqueidentifier

					SELECT @Id = INSERTED.Id
					FROM INSERTED

					UPDATE dbo.Users
					SET DateModified = GETDATE()
					WHERE Id= @Id
				END");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"DROP TRIGGER [dbo].[Users_UPDATE]");

			migrationBuilder.AlterColumn<DateTime>(
				name: "DateCreated",
				table: "Users",
				type: "datetime2",
				nullable: true,
				oldClrType: typeof(DateTime),
				oldType: "datetime2",
				oldNullable: true,
				oldDefaultValueSql: "getdate()");
		}
	}
}
