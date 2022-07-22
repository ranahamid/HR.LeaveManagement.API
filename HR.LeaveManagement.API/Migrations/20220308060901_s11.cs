using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.API.Migrations
{
    public partial class s11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "84b4df01-8f78-4a18-9397-d1733b94ee37", "EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "36e04c61-a836-479d-aff2-4bdd7a8fc514", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Firstname", "Lastname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a30603b-a205-438a-81c3-5809025a26c8", "System", "Admin", "admin@localhost.com", "admin@localhost.com", "AQAAAAEAACcQAAAAEN+x/C/dVA61GbWkVxrvEbIuiiNC6abFkPdDqZi9O31ES1RxuylsyVHojkFQryI45g==", "fd7b821c-e5fc-4319-899f-332303e1310a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Firstname", "Lastname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf094e18-bd86-49b6-b674-0cafcdeb3aeb", "System", "User", "user@localhost.com", "user@localhost.com", "AQAAAAEAACcQAAAAEJlEOXY49YLEqk4TxELcQ0gdHfcCAeEc/H/TKAbzA66xGKI+168AS5Z/Mnb1o0RY/w==", "805d76e3-3e99-4433-8777-9617502173ca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "9452472a-0c21-45a4-aed2-eba251479ce7", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "8daa5cb8-29f6-4529-8b38-2a3a873551e2", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Firstname", "Lastname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07cc72e1-5a14-4d18-af57-f8f7059ad7ba", null, null, null, null, "AQAAAAEAACcQAAAAEAsZuXBNffYoYQLs/w7Dd6Vv/kEYqzKMLPPIZzSEvRvybAcpvCW5Xg+/geYmH/ojGA==", "64c6c4b4-5682-445f-9651-bf55a84100a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Firstname", "Lastname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f456517b-551a-4498-a350-a3f3f976b856", null, null, null, null, "AQAAAAEAACcQAAAAEL9Eh//1/XfvFUCrpS7rsKvtBUzE48FSJyrAxlmMEsCbnkLngFcUzRlf3og4xivOeA==", "b83578c1-5657-438a-b5af-44b3b1e1e917" });
        }
    }
}
