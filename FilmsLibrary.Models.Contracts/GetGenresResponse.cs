using System.Collections.Generic;
using FilmsLibrary.Models.Contracts.Models;

namespace FilmsLibrary.Models.Contracts
{
    public class GetGenresResponse
    {
        public List<Genre> Genres { get; set; }
    }
}