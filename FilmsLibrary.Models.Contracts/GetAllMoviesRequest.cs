namespace FilmsLibrary.Models.Contracts
{
    public class GetAllMoviesRequest
    {
        public const string Route = "/movie/all";

        public int PageNumber { get; set; } = 1;
        
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        
        private int _pageSize = 20;
        private const int MaxPageSize = 100;
    }
}
