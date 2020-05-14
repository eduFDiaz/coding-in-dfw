using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coding.API.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequirementId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_RequirementId",
                table: "Products",
                column: "RequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Requirements_RequirementId",
                table: "Products",
                column: "RequirementId",
                principalTable: "Requirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Requirements_RequirementId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_RequirementId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RequirementId",
                table: "Products");
        }
    }
}
