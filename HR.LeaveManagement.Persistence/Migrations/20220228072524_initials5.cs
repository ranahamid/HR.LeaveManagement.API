using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    public partial class initials5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 7, 25, 24, 697, DateTimeKind.Utc).AddTicks(2219), new DateTime(2022, 2, 28, 7, 25, 24, 697, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 7, 25, 24, 697, DateTimeKind.Utc).AddTicks(2223), new DateTime(2022, 2, 28, 7, 25, 24, 697, DateTimeKind.Utc).AddTicks(2223) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4981), new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4982) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4984), new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4984) });
        }
    }
}
