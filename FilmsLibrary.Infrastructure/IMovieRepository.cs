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

        Task<List<Movie>> GetAllMoviesAsync();

        Task<List<Genre>> GetGenres();
    }
}
