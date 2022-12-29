using System.Collections.Generic;

namespace FilmsLibrary.Models.Contracts
{
    public class GetSimilarMoviesResponse
    {
        public List<Models.GetSimilarMovies.Movie> Movies { get; set; }
    }
}
