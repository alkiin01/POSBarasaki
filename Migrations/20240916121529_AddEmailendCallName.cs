using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSBarasaki.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailendCallName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Callname",
                table:"Users",
                nullable: true
               );
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable:true
               );
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
