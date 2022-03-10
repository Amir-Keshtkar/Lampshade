using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Infrastructure.EfCore.Migrations
{
    public partial class changeProductPicturesRepositoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPicturesRepository_Products_ProductId",
                table: "ProductPicturesRepository");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPicturesRepository",
                table: "ProductPicturesRepository");

            migrationBuilder.RenameTable(
                name: "ProductPicturesRepository",
                newName: "ProductPictures");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPicturesRepository_ProductId",
                table: "ProductPictures",
                newName: "IX_ProductPictures_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPictures",
                table: "ProductPictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_Products_ProductId",
                table: "ProductPictures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_Products_ProductId",
                table: "ProductPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPictures",
                table: "ProductPictures");

            migrationBuilder.RenameTable(
                name: "ProductPictures",
                newName: "ProductPicturesRepository");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPictures_ProductId",
                table: "ProductPicturesRepository",
                newName: "IX_ProductPicturesRepository_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPicturesRepository",
                table: "ProductPicturesRepository",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPicturesRepository_Products_ProductId",
                table: "ProductPicturesRepository",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
