using System.Collections.Generic;

namespace FilmsLibrary.Models.Domain
{
    public class GetAllMovies
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }
        
        public List<Movie> Movies { get; set; }
    }
}