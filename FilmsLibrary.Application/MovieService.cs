using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Contracts;
using FilmsLibrary.Models.Domain;
using FilmsLibrary.Models.Domain.Exceptions;

namespace FilmsLibrary.Application
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var request = new List<int> { id };
            var movie = (await _repository.GetMoviesAsync(request)).FirstOrDefault();

            if (movie == null)
            {
                throw new NotFoundException($"Movie with id {id} was not found");
            }

            return movie;
        }

        public async Task<List<Movie>> GetMoviesAsync(List<int> movieIds)
        {
            var movies = await _repository.GetMoviesAsync(movieIds);

            var foundIds = movies.Select(m => m.Id);
            var notFoundIds = movieIds.Except(foundIds).ToList();

            if (notFoundIds.Any())
            {
                throw new NotFoundException($"Movies with following ids: {string.Join(", ", notFoundIds)} were not found.");
            }

            return movies;
        }

        public async Task<int> CreateMovieAsync(CreateMovie movie)
        {
            if (!await _repository.ActorsExistAsync(movie.ActorIds))
            {
                throw new NotFoundException("Can't create movie because not all actors with given ids exist");
            }
            if (!await _repository.GenreExistsAsync(movie.GenreId))
            {
                throw new NotFoundException($"Genre with id {movie.GenreId} does not exist");
            }

            return await _repository.CreateMovieAsync(movie);
        }

        public async Task UpdateMovieAsync(UpdateMovie updateMovie)
        {
            if (!await _repository.GenreExistsAsync(updateMovie.GenreId))
            {
                throw new NotFoundException($"Genre with id {updateMovie.GenreId} does not exist");
            }

            await _repository.UpdateMovieAsync(updateMovie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            await _repository.DeleteMovieAsync(id);
        }

        public async Task<List<Movie>> GetMovieByActorIdAsync(int id)
        {
            var movies = await _repository.GetMoviesByActorAsync(id);

            if (!movies.Any() || movies == null)
            {
                throw new NotFoundException($"Movies with actor id {id} were not found.");
            }

            return movies;
        }

        public async Task<List<Movie>> GetSimilarMovies(int movieId)
        {
            var movies = await _repository.GetSimilarMoviesAsync(movieId);

            return movies;
        }

        public async Task<MoviesWithPaging> GetAllMoviesAsync(int pageNumber, int pageSize)
        {
            var movies = await _repository.GetAllMoviesAsync(pageNumber, pageSize);
            var totalRecords = _repository.GetTotalNumberOfRecords(pageSize);

            return new MoviesWithPaging
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Movies = movies
            };
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _repository.GetGenres();
        }

        public async Task<MoviesWithPaging> SearchMovies(SearchMovie searchCriteria, int pageNumber, int pageSize)
        {
            return await _repository.SearchMovies(searchCriteria, pageNumber, pageSize);
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await _repository.GetGenreById(id);
        }
    }
}
