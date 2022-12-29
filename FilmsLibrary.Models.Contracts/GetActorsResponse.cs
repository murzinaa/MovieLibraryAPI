using System.Collections.Generic;
using FilmsLibrary.Models.Contracts.Models;

namespace FilmsLibrary.Models.Contracts
{
    public class GetActorsResponse
    {
        public List<Actor> Actors { get; set; }
    }
}
