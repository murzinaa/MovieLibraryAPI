using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsLibrary.SqlRepository.Migrations
{
    public partial class CreateSPGetMoviesByActorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = "CREATE PROCEDURE spGetMoviesByActorId(@actorId int) " +
                         "AS " +
                         "BEGIN " +
                         "SELECT m.Id, m.Title, m.ReleaseYear, m.Rating, m.Description, m.GenreCode " +
                         "FROM Movie m " +
                         "INNER JOIN ActorMovie am ON am.MovieId = m.Id " +
                         "WHERE am.ActorId = @actorId " +
                         "END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC spGetMoviesByActorId");
        }
    }
}
