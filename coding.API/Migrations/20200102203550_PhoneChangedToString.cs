using Microsoft.EntityFrameworkCore.Migrations;

namespace coding.API.Migrations
{
    public partial class PhoneChangedToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
