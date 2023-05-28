using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsLibrary.SqlRepository.Migrations
{
    public partial class DeletedAgeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Actor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Actor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Actor",
                keyColumn: "Id",
                keyValue: 1,
                column: "Age",
                value: 45);

            migrationBuilder.UpdateData(
                table: "Actor",
                keyColumn: "Id",
                keyValue: 2,
                column: "Age",
                value: 59);
        }
    }
}
