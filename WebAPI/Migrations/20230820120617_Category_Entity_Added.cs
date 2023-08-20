using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class Category_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b03b0fbe-f0b6-4029-8249-144d22253e17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cceaa045-13cb-4e0d-9303-a9036b34cba9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4bdfced-69b0-4d35-94e3-53d6c35fdc99");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cac0849-5c8e-40a0-81ef-6364c1aee8c2", "5f44e62e-5f0d-4fc0-9578-58b14c6263c1", "User", "USER" },
                    { "267aaa77-1c62-4057-8c58-f2ad76064a86", "e163c3d5-8975-430c-88b7-92837c729ec1", "Admin", "ADMIN" },
                    { "e2e19381-b16b-43da-a5e8-fd799bf223c5", "84456880-0b31-4ae6-97f1-a691dae1ec5b", "Editor", "EDITOR" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Network" },
                    { 3, "Database Management" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cac0849-5c8e-40a0-81ef-6364c1aee8c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "267aaa77-1c62-4057-8c58-f2ad76064a86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2e19381-b16b-43da-a5e8-fd799bf223c5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b03b0fbe-f0b6-4029-8249-144d22253e17", "84135ca0-f947-4976-9a7b-fa096de702cd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cceaa045-13cb-4e0d-9303-a9036b34cba9", "5bdea6c1-aacf-46a9-b5b5-ae2c86e7e0dd", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4bdfced-69b0-4d35-94e3-53d6c35fdc99", "9a9b4529-f8fb-4389-9427-f6656f18939a", "User", "USER" });
        }
    }
}
