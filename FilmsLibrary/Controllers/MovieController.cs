using AutoMapper;
using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Contracts;
using FilmsLibrary.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Genre = FilmsLibrary.Models.Contracts.Models.Genre;
using Movie = FilmsLibrary.Models.Contracts.Models.Movie;

namespace FilmsLibrary.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;
        private readonly IMapper _mapper;

        public MovieController(IMovieService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get movie by Id.
        /// </summary>
        /// <param name="request">GetMovieByIdRequest request.</param>
        /// <returns>GetMovieByIdResponse: information about requested movie.</returns>
        [HttpGet(GetMovieByIdRequest.Route)]
        [ProducesResponseType(typeof(GetMovieByIdResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetMovieByIdResponse> GetMovieByIdAsync([FromRoute] GetMovieByIdRequest request)
        {
            var movie = await _service.GetMovieByIdAsync(request.MovieId);

            var result =  _mapper.Map<Movie>(movie);

            return new GetMovieByIdResponse
            {
                Movie = result,
            };
        }

        /// <summary>
        /// Get movies by ids.
        /// </summary>
        /// <param name="request">GetMoviesByIdsRequest request.</param>
        /// <returns>GetMoviesByIdsResponse: information about requested movies.</returns>
        [HttpGet(GetMoviesByIdsRequest.Route)]
        [ProducesResponseType(typeof(GetMoviesByIdsResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetMoviesByIdsResponse> GetMoviesByIdsAsync([FromQuery] GetMoviesByIdsRequest request)
        {
            var movies = await _service.GetMoviesAsync(request.MovieIds);

            var result = _mapper.Map<List<Movie>>(movies);

            return new GetMoviesByIdsResponse
            {
                Movies = result,
            };
        }

        /// <summary>
        /// Create movie.
        /// </summary>
        /// <param name="request">CreateMovieRequest request.</param>
        /// <returns>CreateMovieResponse: id of created movie.</returns>
        [HttpPost(CreateMovieRequest.Route)]
        [ProducesResponseType(typeof(CreateMovieResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<CreateMovieResponse> CreateMovieAsync([FromBody] CreateMovieRequest request)
        {
            var createMovie = _mapper.Map<CreateMovie>(request);
            var id = await _service.CreateMovieAsync(createMovie);

            return new CreateMovieResponse
            {
                Id = id,
            };
        }

        /// <summary>
        /// Update movie.
        /// </summary>
        /// <param name="request">UpdateMovieRequest request.</param>
        /// <returns>A <see cref="Task"/> IActionResult.</returns>
        [HttpPut(UpdateMovieRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateMovieAsync([FromBody] UpdateMovieRequest request)
        {
            var updateMovieRequest = _mapper.Map<UpdateMovie>(request);
            await _service.UpdateMovieAsync(updateMovieRequest);

            return Ok();
        }

        /// <summary>
        /// Delete movie.
        /// </summary>
        /// <param name="request">DeleteMovieRequest request.</param>
        /// <returns>A <see cref="Task"/> IActionResult.</returns>
        [HttpDelete(DeleteMovieRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteMovieAsync([FromRoute] DeleteMovieRequest request)
        {
            await _service.DeleteMovieAsync(request.MovieId);

            return Ok();
        }

        /// <summary>
        /// Get all movies by actor id.
        /// </summary>
        /// <param name="request">GetMoviesByActorRequest request.</param>
        /// <returns>List of movies.</returns>
        [HttpGet(GetMoviesByActorRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetMoviesByActorResponse> GetMoviesByActorIdAsync([FromRoute] GetMoviesByActorRequest request)
        {
            var movies = await _service.GetMovieByActorIdAsync(request.ActorId);

            return new GetMoviesByActorResponse
            {
                Movies = _mapper.Map<List<Movie>>(movies),
            };
        }

        /// <summary>
        /// Get similar movies by movie id
        /// </summary>
        /// <param name="request">GetSimilarMoviesRequest request.</param>
        /// <returns>GetSimilarMoviesResponse.</returns>
        [HttpGet(GetSimilarMoviesRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetSimilarMoviesResponse> GetSimilarMovies([FromRoute] GetSimilarMoviesRequest request)
        {
            var movies = await _service.GetSimilarMovies(request.MovieId);

            return new GetSimilarMoviesResponse
            {
                Movies = _mapper.Map<List<Models.Contracts.Models.GetSimilarMovies.Movie>>(movies),
            };
        }
        
        /// <summary>
        /// Get all movies.
        /// </summary>
        /// <returns>GetAllMoviesResponse.</returns>
        [HttpGet(GetAllMoviesRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetAllMoviesResponse> GetAllMovies([FromQuery] GetAllMoviesRequest request)
        {
            var movies = await _service.GetAllMoviesAsync(request.PageNumber, request.PageSize);
            return _mapper.Map<GetAllMoviesResponse>(movies);
        }
        
        /// <summary>
        /// Get all genres.
        /// </summary>
        /// <returns>GetGenresResponse.</returns>
        [HttpGet("/genres")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetGenresResponse> GetGenres()
        {
            var genres = await _service.GetGenresAsync();

            return new GetGenresResponse
            {
                Genres = _mapper.Map<List<Genre>>(genres),
            };
        }
        
        /// <summary>
        /// Search movies by search criteria.
        /// </summary>
        /// <returns>SearchMoviesResponse.</returns>
        [HttpPost(SearchMoviesRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<SearchMoviesResponse> SearchMovies([FromBody] SearchMoviesRequest request)
        {
            var searchCriteria = _mapper.Map<SearchMovie>(request);

            var movies = await _service.SearchMovies(searchCriteria, request.PageNumber, request.PageSize);

            return _mapper.Map<SearchMoviesResponse>(movies);
        }
        
        // <summary>
        /// Get genre by id.
        /// </summary>
        /// <returns>GetGenreByIdResponse.</returns>
        [HttpGet(GetGenreByIdRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetGenreByIdResponse> GetGenreById([FromRoute] GetGenreByIdRequest request)
        {
            var genre = await _service.GetGenreById(request.Id);
            return new GetGenreByIdResponse
            {
                Genre = _mapper.Map<Genre>(genre),
            };
        }
    }
}
