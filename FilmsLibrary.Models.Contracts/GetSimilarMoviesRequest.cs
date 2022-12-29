namespace FilmsLibrary.Models.Contracts
{
    public class GetSimilarMoviesRequest
    {
        public const string Route = "/movie/{MovieId}/similar";

        public int MovieId { get; set; }
    }
}
