using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSWEB.Server.Migrations
{
    /// <inheritdoc />
    public partial class checkcirculardependecy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Users_UserId",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Users_UserId1",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ProductsCreated",
                schema: "product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ProductsUpdated",
                schema: "product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_UserId",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                schema: "product",
                table: "ProductCategories",
                newName: "ProductCategoriesUpdated");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_UserId1",
                schema: "product",
                table: "ProductCategories",
                newName: "IX_ProductCategories_ProductCategoriesUpdated");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                schema: "product",
                table: "ProductCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedTime",
                schema: "product",
                table: "ProductCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoriesCreated",
                schema: "product",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategoriesCreated",
                schema: "product",
                table: "ProductCategories",
                column: "ProductCategoriesCreated");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Users_ProductCategoriesCreated",
                schema: "product",
                table: "ProductCategories",
                column: "ProductCategoriesCreated",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Users_ProductCategoriesUpdated",
                schema: "product",
                table: "ProductCategories",
                column: "ProductCategoriesUpdated",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ProductsCreated",
                schema: "product",
                table: "Products",
                column: "ProductsCreated",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ProductsUpdated",
                schema: "product",
                table: "Products",
                column: "ProductsUpdated",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Users_ProductCategoriesCreated",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Users_ProductCategoriesUpdated",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ProductsCreated",
                schema: "product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ProductsUpdated",
                schema: "product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductCategoriesCreated",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTime",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductCategoriesCreated",
                schema: "product",
                table: "ProductCategories");

            migrationBuilder.RenameColumn(
                name: "ProductCategoriesUpdated",
                schema: "product",
                table: "ProductCategories",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_ProductCategoriesUpdated",
                schema: "product",
                table: "ProductCategories",
                newName: "IX_ProductCategories_UserId1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "product",
                table: "ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_UserId",
                schema: "product",
                table: "ProductCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Users_UserId",
                schema: "product",
                table: "ProductCategories",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Users_UserId1",
                schema: "product",
                table: "ProductCategories",
                column: "UserId1",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ProductsCreated",
                schema: "product",
                table: "Products",
                column: "ProductsCreated",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ProductsUpdated",
                schema: "product",
                table: "Products",
                column: "ProductsUpdated",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
