using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changebaseentityrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_TenantId",
                schema: "product",
                table: "ProductCategories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUnits_TenantId",
                schema: "lookup",
                table: "BaseUnits",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_TenantId",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_BaseUnits_TenantId",
                schema: "lookup",
                table: "BaseUnits");
        }
    }
}
