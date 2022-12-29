using System.ComponentModel.DataAnnotations;

namespace FilmsLibrary.Models.Contracts
{
    public class GetMovieByIdRequest
    {
        public const string Route = "/movie/{MovieId}";

        [Required]
        public int MovieId { get; set; }
    }
}
