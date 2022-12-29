namespace FilmsLibrary.Models.Contracts
{
    public class DeleteMovieRequest
    {
        public const string Route = "/movie/{MovieId}";

        public int MovieId { get; set; }
    }
}
