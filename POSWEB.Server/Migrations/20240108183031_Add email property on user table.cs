using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSWEB.Server.Migrations
{
    /// <inheritdoc />
    public partial class Addemailpropertyonusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "user",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "user",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "user",
                table: "Users");
        }
    }
}
