using AutoMapper;
using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Domain;
using FilmsLibrary.Models.Domain.Exceptions;
using FilmsLibrary.Models.Sql;
using Microsoft.EntityFrameworkCore;
using Actor = FilmsLibrary.Models.Domain.Actor;
using Genre = FilmsLibrary.Models.Domain.Genre;
using Movie = FilmsLibrary.Models.Domain.Movie;

namespace FilmsLibrary.SqlRepository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateMovieAsync(CreateMovie movie)
        {
            var createMovie = _mapper.Map<Models.Sql.Movie>(movie);

            await _context.Movies.AddAsync(createMovie);

            var actorsMovies = new List<ActorMovie>();
            actorsMovies.AddRange(movie.ActorIds.Select(x => new ActorMovie { MovieId = createMovie.Id, ActorId = x }));

            createMovie.ActorMovies = actorsMovies;

            await _context.SaveChangesAsync();

            return createMovie.Id;
        }

        public async Task<List<Movie>> GetMoviesAsync(List<int> movieIds)
        {
            var movies = await _context.Movies
                .Include(m => m.ActorMovies)
                .ThenInclude(am => am.Actor)
                .Include(m => m.Comments)
                .Where(m => movieIds.Contains(m.Id)).ToListAsync();
            return _mapper.Map<List<Movie>>(movies);
        }

        public async Task UpdateMovieAsync(UpdateMovie updateMovie)
        {
            var movieToUpdate = await _context.Movies.Include(m => m.ActorMovies)
                .Where(m => m.Id == updateMovie.Id).FirstOrDefaultAsync();

            if (movieToUpdate == null)
            {
                throw new NotFoundException($"Movie with id: {updateMovie.Id} was not found");
            }

            var existedActorIds = movieToUpdate.ActorMovies.Select(x => x.ActorId).ToList();
            var newActorIds = updateMovie.ActorIds.Except(existedActorIds);
            var actorIdsToDelete = existedActorIds.Except(updateMovie.ActorIds);

            foreach (var actorId in actorIdsToDelete)
            {
                var actor = await _context.ActorMovies.Where(x => x.ActorId == actorId && x.MovieId == updateMovie.Id)
                    .FirstAsync();
                _context.ActorMovies.Remove(actor);
            }

            foreach (var actorId in newActorIds)
            {
                await _context.ActorMovies.AddAsync(new ActorMovie
                {
                    ActorId = actorId,
                    MovieId = updateMovie.Id,
                });
            }

            movieToUpdate.Title = updateMovie.Title;
            movieToUpdate.Description = updateMovie.Description;
            movieToUpdate.GenreId = updateMovie.GenreId;
            movieToUpdate.ReleaseYear = updateMovie.ReleaseYear;
            movieToUpdate.Rating = updateMovie.Rating;
            movieToUpdate.ImageUrl = updateMovie.ImageUrl;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movieToDelete = await _context.Movies.Where(m => m.Id == movieId).FirstOrDefaultAsync();

            if (movieToDelete == null)
            {
                throw new NotFoundException($"Movie with id: {movieId} was not found");
            }

            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Models.Domain.Movie>> GetMoviesByActorAsync(int actorId)
        {
            var movies = await _context.Movies.FromSqlRaw($"spGetMoviesByActorId {actorId}").ToListAsync();

            return _mapper.Map<List<Models.Domain.Movie>>(movies);
        }

        public async Task<bool> ActorsExistAsync(List<int> ids)
        {
            var actorsCount = await _context.Actors.Where(a => ids.Contains(a.Id)).CountAsync();
            return actorsCount == ids.Count;
        }

        public async Task<List<Actor>> GetAllActorsAsync()
        {
            var actors = await _context.Actors.FromSqlRaw("spGetAllActors").ToListAsync();
            return _mapper.Map<List<Actor>>(actors);
        }

        public async Task<List<Movie>> GetSimilarMoviesAsync(int movieId)
        {
            // list of movie ids and count of matching actors
            var query = from am in _context.ActorMovies
                join am2 in _context.ActorMovies on am.ActorId equals am2.ActorId
                where am2.MovieId == movieId
                group am by am.MovieId
                into grp
                select new { grp.Key, Count = grp.Count() };

            var movies = from q in query
                join m in _context.Movies on q.Key equals m.Id
                where m.Id != movieId
                orderby q.Count descending
                select m;

            var result = await movies.ToListAsync();
            return _mapper.Map<List<Movie>>(result);
        }

        public async Task<bool> GenreExistsAsync(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<List<Movie>> GetAllMoviesAsync(int pageNumber, int pageSize)
        {
            var movies = await _context.Movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<Movie>>(movies);
        }

        public int GetTotalNumberOfRecords(int pageSize)
        {
            return _context.Movies.Count();
        }

        public async Task<MoviesWithPaging> SearchMovies(SearchMovie searchCriteria, int pageNumber, int pageSize)
        {
            var query = _context.Movies
                .Include(x => x.ActorMovies)
                .ThenInclude(x => x.Actor).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchCriteria.Title))
            {
                query = query.Where(m => m.Title.ToLower().Contains(searchCriteria.Title.ToLower()));
            }

            if (searchCriteria.ReleaseYear.HasValue )
            {
                query = query.Where(m => m.ReleaseYear == searchCriteria.ReleaseYear.Value);
            }

            if (searchCriteria.GenreIds != null && searchCriteria.GenreIds.Any())
            {
                query = query.Where(m => searchCriteria.GenreIds.Contains(m.GenreId));
            }

            if (searchCriteria.ActorIds != null && searchCriteria.ActorIds.Any())
            {
                var movieIds = query.SelectMany(m => m.ActorMovies)
                    .Where(m => searchCriteria.ActorIds.Contains(m.ActorId)).Select(m => m.MovieId);
                query = query.Where(m => movieIds.Contains(m.Id));
            }
            
            var movies = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new MoviesWithPaging
            {
                Movies = _mapper.Map<List<Movie>>(movies),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = query.Count()
            };

            return response;
        }

        public async Task<Genre> GetGenreById(int id)
        {
            var genre = await _context.Genres.Where(g => g.Id == id).FirstAsync();
            return _mapper.Map<Genre>(genre);
        }


        public async Task<List<Genre>> GetGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            return _mapper.Map<List<Genre>>(genres);
        }

        public async Task<int> AddActor(Actor actor)
        {
            var entity = _mapper.Map<Models.Sql.Actor>(actor);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Actor> GetActorByName(string firstName, string lastName)
        {
            var actor = await _context.Actors
                .Where(a => a.Name == firstName && a.Surname == lastName)
                .FirstOrDefaultAsync();
            return _mapper.Map<Actor>(actor);
        }
    }
}