using System.Collections.Generic;
using FilmsLibrary.Models.Contracts.Models;

namespace FilmsLibrary.Models.Contracts
{
    public class GetAllMoviesResponse
    {
        public List<Movie> Movies { get; set; }
    }
}
