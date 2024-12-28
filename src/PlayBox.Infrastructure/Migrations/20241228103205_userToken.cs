using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayBox.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "refreshtoken",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "refreshtokenexpirytime",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refreshtoken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "refreshtokenexpirytime",
                table: "users");
        }
    }
}
