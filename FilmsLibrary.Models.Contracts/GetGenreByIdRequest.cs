namespace FilmsLibrary.Models.Contracts
{
    public class GetGenreByIdRequest
    {
        public const string Route = "/genre/{Id}";
        
        public int Id { get; set; }
    }
}