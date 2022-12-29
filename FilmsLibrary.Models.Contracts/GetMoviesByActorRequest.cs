namespace FilmsLibrary.Models.Contracts
{
    public class GetMoviesByActorRequest
    {
        public const string Route = "/movie/actor/{ActorId}";

        public int ActorId { get; set; }
    }
}
