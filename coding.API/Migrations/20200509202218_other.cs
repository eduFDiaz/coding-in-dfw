using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coding.API.Migrations
{
    public partial class other : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequirements_Products_ProductId1",
                table: "ProductRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequirements_Requirements_RequirementId1",
                table: "ProductRequirements");

            migrationBuilder.DropIndex(
                name: "IX_ProductRequirements_ProductId1",
                table: "ProductRequirements");

            migrationBuilder.DropIndex(
                name: "IX_ProductRequirements_RequirementId1",
                table: "ProductRequirements");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductRequirements");

            migrationBuilder.DropColumn(
                name: "RequirementId1",
                table: "ProductRequirements");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequirementId",
                table: "ProductRequirements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductRequirements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequirements_RequirementId",
                table: "ProductRequirements",
                column: "RequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequirements_Products_ProductId",
                table: "ProductRequirements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequirements_Requirements_RequirementId",
                table: "ProductRequirements",
                column: "RequirementId",
                principalTable: "Requirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequirements_Products_ProductId",
                table: "ProductRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequirements_Requirements_RequirementId",
                table: "ProductRequirements");

            migrationBuilder.DropIndex(
                name: "IX_ProductRequirements_RequirementId",
                table: "ProductRequirements");

            migrationBuilder.AlterColumn<int>(
                name: "RequirementId",
                table: "ProductRequirements",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductRequirements",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "ProductRequirements",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequirementId1",
                table: "ProductRequirements",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequirements_ProductId1",
                table: "ProductRequirements",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequirements_RequirementId1",
                table: "ProductRequirements",
                column: "RequirementId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequirements_Products_ProductId1",
                table: "ProductRequirements",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequirements_Requirements_RequirementId1",
                table: "ProductRequirements",
                column: "RequirementId1",
                principalTable: "Requirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
