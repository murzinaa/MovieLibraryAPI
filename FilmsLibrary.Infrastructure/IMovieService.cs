using FilmsLibrary.Models.Contracts;
using FilmsLibrary.Models.Domain;

namespace FilmsLibrary.Infrastructure
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(int id);

        Task<List<Movie>> GetMoviesAsync(List<int> movieIds);

        Task<int> CreateMovieAsync(CreateMovie movie);

        Task UpdateMovieAsync(UpdateMovie updateMovie);

        Task DeleteMovieAsync(int id);

        Task<List<Movie>> GetMovieByActorIdAsync(int id);

        Task<List<Movie>> GetSimilarMovies(int movieId);

        Task<GetAllMovies> GetAllMoviesAsync(int pageNumber, int pageSize);

        Task<List<Genre>> GetGenresAsync();
    }
}
