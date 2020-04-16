using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coding.API.Migrations
{
    public partial class AddedProductAndOtherPropsToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resume",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CodepenProfile",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookProfile",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullResume",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GithubUrl",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInProfile",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RedditProfile",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortResume",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StackOverflowProfile",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwiterProfile",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ProductPhoto = table.Column<string>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserId",
                table: "Product",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropColumn(
                name: "CodepenProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FacebookProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullResume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GithubUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LinkedInProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RedditProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShortResume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StackOverflowProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwiterProfile",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
