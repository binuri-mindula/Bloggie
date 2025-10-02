using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class InitialAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e229e34-e033-4b55-ac65-72fd74374b57",
                column: "NormalizedName",
                value: "USER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2058158a-78b6-4a32-a1bd-a498f85b1c0e",
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c97ec567-e888-4f65-be16-f6c062759046",
                column: "NormalizedName",
                value: "SUPERADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f227666e-7fba-46f8-96c9-a9e39c525131",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff93d7af-3f8b-40d6-94d5-3af250624439", "AQAAAAIAAYagAAAAEO2OhAPhAb0uNcg4RtOg2Sbja0HP+pnULQgDtS7ebkMRo9NsG/uhLnZgqFhK1y6pQQ==", "11848248-8acc-4730-b2e2-962eec3557da" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e229e34-e033-4b55-ac65-72fd74374b57",
                column: "NormalizedName",
                value: "User");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2058158a-78b6-4a32-a1bd-a498f85b1c0e",
                column: "NormalizedName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c97ec567-e888-4f65-be16-f6c062759046",
                column: "NormalizedName",
                value: "SuperAdmin");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f227666e-7fba-46f8-96c9-a9e39c525131",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2101aad-2e9e-4137-8349-a06434aed1b2", "AQAAAAIAAYagAAAAEMR83ek3ltYLYn3eO97tarOeMjF98YxqgeUsCCsFe8f9sQwGyJqXIxvjPgRbHwep5Q==", "dff87dda-5ca0-42eb-a075-8bb3ef3c45f9" });
        }
    }
}
