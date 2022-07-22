using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.API.Migrations
{
    public partial class s112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "0b394a88-efbd-438e-9738-9db5bf3a2518");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                column: "ConcurrencyStamp",
                value: "5657439d-e9c3-484e-88ec-01de94005541");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f574f2dd-e46e-4c71-9106-a878e267e3e6", "AQAAAAEAACcQAAAAEKu+bg2jMi1uAi59UXktSGxT1hfWz0ASk5auw7WRgiKKFbwizHQmIhcKEg+HhRusUg==", "7cde6722-5bea-4e37-ac7c-0c29d1ce36b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d48c1a82-8f11-4557-a2bb-dd9017a26821", "AQAAAAEAACcQAAAAEKx6ZPUkBQQpefgdyKDmrVh3SOIdGc9r96cmAHdlSFC9clXGJh8ec17ZKvN+PoTShw==", "1d6aef2a-c0b7-40c2-9689-ae78095a91da" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "84b4df01-8f78-4a18-9397-d1733b94ee37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                column: "ConcurrencyStamp",
                value: "36e04c61-a836-479d-aff2-4bdd7a8fc514");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a30603b-a205-438a-81c3-5809025a26c8", "AQAAAAEAACcQAAAAEN+x/C/dVA61GbWkVxrvEbIuiiNC6abFkPdDqZi9O31ES1RxuylsyVHojkFQryI45g==", "fd7b821c-e5fc-4319-899f-332303e1310a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf094e18-bd86-49b6-b674-0cafcdeb3aeb", "AQAAAAEAACcQAAAAEJlEOXY49YLEqk4TxELcQ0gdHfcCAeEc/H/TKAbzA66xGKI+168AS5Z/Mnb1o0RY/w==", "805d76e3-3e99-4433-8777-9617502173ca" });
        }
    }
}
