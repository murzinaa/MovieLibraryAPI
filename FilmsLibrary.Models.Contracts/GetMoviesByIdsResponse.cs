﻿using System.Collections.Generic;
using FilmsLibrary.Models.Contracts.Models;

namespace FilmsLibrary.Models.Contracts
{
    public class GetMoviesByIdsResponse
    {
        public List<Movie> Movies { get; set; }
    }
}
