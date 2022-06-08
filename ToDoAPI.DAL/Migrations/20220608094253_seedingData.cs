using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAPI.DAL.Migrations
{
    public partial class seedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "EntBy", "EntDt", "IsCompleted", "ModBy", "ModDt", "Name" },
                values: new object[] { 1, null, new DateTime(2022, 6, 8, 15, 12, 52, 996, DateTimeKind.Local).AddTicks(1249), false, null, new DateTime(2022, 6, 8, 15, 12, 52, 996, DateTimeKind.Local).AddTicks(1263), "Test1" });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "EntBy", "EntDt", "IsCompleted", "ModBy", "ModDt", "Name" },
                values: new object[] { 2, null, new DateTime(2022, 6, 8, 15, 12, 52, 996, DateTimeKind.Local).AddTicks(1265), false, null, new DateTime(2022, 6, 8, 15, 12, 52, 996, DateTimeKind.Local).AddTicks(1266), "Test2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
