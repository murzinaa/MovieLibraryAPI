using System.Collections.Generic;
using FilmsLibrary.Models.Contracts.Models;

namespace FilmsLibrary.Models.Contracts
{
    public class GetMoviesByActorResponse
    {
        public List<Movie> Movies { get; set; }
    }
}
