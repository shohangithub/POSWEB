using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSWEB.Server.Migrations
{
    /// <inheritdoc />
    public partial class add_User_Role_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "user",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "user",
                table: "Users");
        }
    }
}
