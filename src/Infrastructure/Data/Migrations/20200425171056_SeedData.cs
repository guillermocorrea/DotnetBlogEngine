using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "PublishDate", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Draft post body", null, "Draft", "Draft Post Title", 1 },
                    { 2, "Pending post body", null, "Pending", "Pending Post Title", 1 },
                    { 3, "Rejected post body", null, "Rejected", "Rejected Post Title", 1 },
                    { 4, "Approved post body", new DateTime(2020, 4, 25, 12, 10, 55, 411, DateTimeKind.Local).AddTicks(5804), "Approved", "Approved Post Title", 1 },
                    { 5, "Approved post body 2", new DateTime(2020, 4, 25, 12, 10, 55, 412, DateTimeKind.Local).AddTicks(731), "Approved", "Approved Post Title 2", 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "UserId", "Username" },
                values: new object[] { 1, "Great post!", new DateTime(2020, 4, 25, 12, 10, 55, 412, DateTimeKind.Local).AddTicks(5009), 4, null, "Anonymous" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
