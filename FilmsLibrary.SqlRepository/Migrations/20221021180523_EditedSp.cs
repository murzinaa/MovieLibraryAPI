using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsLibrary.SqlRepository.Migrations
{
    public partial class EditedSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = "ALTER PROCEDURE spGetMoviesByActorId(@actorId int) " +
                     "AS " +
                     "BEGIN " +
                     "SELECT m.Id, m.Title, m.ReleaseYear, m.Rating, m.Description, m.GenreId " +
                     "FROM Movie m " +
                     "INNER JOIN ActorMovie am ON am.MovieId = m.Id " +
                     "WHERE am.ActorId = @actorId " +
                     "END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE spGetMoviesByActorId");
        }
    }
}
