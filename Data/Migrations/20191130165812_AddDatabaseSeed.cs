using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketReservationSystem.Data.Migrations
{
    public partial class AddDatabaseSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "752b64c2-5925-409f-815e-01110aa58e14", "User", "USER", "a9ed8228-259e-4e24-95b4-4827dd163ad7" }
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "889450f1-acda-4333-afc7-912c5765c8f4", "Cashier", "CASHIER", "84aba04e-7fa8-4b53-81c8-0a2bc44dd3f4" }
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "6401d89d-9128-4110-9e50-081e7624644a", "Bookkeeper", "BOOKKEEPER", "9fa9cfb9-9dec-47a1-911d-8fbd3f0194cc" }
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "d770841d-c313-4dfa-a550-ede59a99968e", "Administrator", "ADMINISTRATOR", "34875d08-e472-4eed-92c3-d1940d007456" }
            );
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", 
                    "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", 
                    "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", 
                    "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { "9032786a-184f-4b30-b247-6f7071727a84", "admin@admin.com", "ADMIN@ADMIN.COM", 
                    "admin@admin.com", "ADMIN@ADMIN.COM", true, 
                    "AQAAAAEAACcQAAAAEOvKeECFN3etzNkTtcmGbRM3CfzOiJXxBezeQvf4w1OKcyWh9ixAIoLi2QxiqveqFA==",
                    "OSDB2ZMXRXL5J2P2MGKBL2KHIHBP2AKB", "4e743746-319a-444a-9c4a-c7ba77291c93", null,
                    false, false, null, true, 0 }
            );
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "9032786a-184f-4b30-b247-6f7071727a84", "d770841d-c313-4dfa-a550-ede59a99968e" }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumn: "UserId",
                keyValue: "9032786a-184f-4b30-b247-6f7071727a84"
            );
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9032786a-184f-4b30-b247-6f7071727a84"
            );
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "752b64c2-5925-409f-815e-01110aa58e14"
            );
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "889450f1-acda-4333-afc7-912c5765c8f4"
            );
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6401d89d-9128-4110-9e50-081e7624644a"
            );
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d770841d-c313-4dfa-a550-ede59a99968e"
            );
        }
    }
}
