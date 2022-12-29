using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsLibrary.SqlRepository.Migrations
{
    public partial class CreateSPGetAllActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script = "CREATE PROCEDURE spGetAllActors " +
                         "AS " +
                         "BEGIN " +
                         "SELECT* FROM Actor " +
                         "END";
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROC spGetAllActors";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
