using System.Collections.Generic;
using System.Threading.Tasks;
using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Domain;
using FilmsLibraryEFCore.FileRepository.Helpers;

namespace FilmsLibraryEFCore.FileRepository
{
    public class FileRepository : IFileRepository
    {
        private readonly FileHelper _helper;
        private readonly FilePath _path;

        public FileRepository(FileHelper helper, FilePath path)
        {
            _helper = helper;
            _path = path;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            var data = await _helper.ReadDataFromFile(_path.MoviePath);

            var movies = new List<Movie>();

            foreach (var line in data)
            {
                var lines = line.Split(", ");
                var movie = new Movie
                {
                    Id = int.Parse(lines[0]),
                    Title = lines[1],
                    ReleaseYear = int.Parse(lines[2]),
                    Description = lines[3],
                    Rating = int.Parse(lines[4]),
                    GenreId = int.Parse(lines[5])
                };
                movie.Actors = await GetActorsByMovieId(int.Parse(lines[0]));
                movies.Add(movie);
            }

            return movies;
        }

        public async Task<List<Actor>> GetActorsByMovieId(int id)
        {
            // ActorMovie columns: Id, MovieId, ActorId
            var data1 = await _helper.ReadDataFromFile(_path.ActorMoviePath);
            var data2 = await _helper.ReadDataFromFile(_path.ActorPath);
            List<int> actorIds = new List<int>();
            List<Actor> actors = new List<Actor>();

            foreach (var item in data1)
            {
                var items = item.Split(", ");
                if (int.Parse(items[1]) == id)
                {
                    actorIds.Add(int.Parse(items[2]));
                }
            }

            foreach (var item in data2)
            {
                var data = item.Split(", ");
                if (actorIds.Contains(int.Parse(data[0])))
                {
                    var actor = new Actor
                    {
                        Id = int.Parse(data[0]),
                        Name = data[1],
                        Surname = data[2],
                        Age = int.Parse(data[3])
                    };

                    actors.Add(actor);
                }
            }

            return actors;
        }
    }
}
