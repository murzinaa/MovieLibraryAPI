using FilmsLibrary.Models.Domain;

namespace FilmsLibrary.Infrastructure
{
    public interface IMovieRepository
    {
        Task<int> CreateMovieAsync(CreateMovie movie);

        Task<List<Movie>> GetMoviesAsync(List<int> movieIds);

        Task UpdateMovieAsync(UpdateMovie updateMovie);

        Task DeleteMovieAsync(int movieId);

        Task<List<Movie>> GetMoviesByActorAsync(int id);

        Task<bool> ActorsExistAsync(List<int> ids);

        Task<List<Actor>> GetAllActorsAsync();

        Task<List<Movie>> GetSimilarMoviesAsync(int movieId);

        Task<bool> GenreExistsAsync(int id);

        Task<List<Movie>> GetAllMoviesAsync(int pageNumber, int pageSize);

        Task<List<Genre>> GetGenres();

        Task<int> AddActor(Actor actor);

        Task<Actor> GetActorByName(string firstName, string lastName);

        int GetTotalNumberOfRecords(int pageSize);

        Task<MoviesWithPaging> SearchMovies(SearchMovie searchCriteria, int pageNumber, int pageSize);

        Task<Genre> GetGenreById(int id);
    }
}
