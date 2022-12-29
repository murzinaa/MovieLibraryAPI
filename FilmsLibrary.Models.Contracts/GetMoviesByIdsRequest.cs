using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmsLibrary.Models.Contracts
{
    public class GetMoviesByIdsRequest
    {
        public const string Route = "/movies";

        [Required]
        [MinLength(1, ErrorMessage = "MovieIds should not be empty")]
        public List<int> MovieIds { get; set; }
    }
}
