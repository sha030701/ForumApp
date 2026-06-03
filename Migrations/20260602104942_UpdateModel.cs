using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "ForumThreads",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Comments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_UserAccountId",
                table: "ForumThreads",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserAccountId",
                table: "Comments",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserAccounts_UserAccountId",
                table: "Comments",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThreads_UserAccounts_UserAccountId",
                table: "ForumThreads",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserAccounts_UserAccountId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumThreads_UserAccounts_UserAccountId",
                table: "ForumThreads");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_ForumThreads_UserAccountId",
                table: "ForumThreads");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserAccountId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Comments");
        }
    }
}
