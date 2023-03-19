using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FilmsLibrary.Models.Contracts
{
    public class SearchMoviesRequest : IValidatableObject
    {
        public const string Route = "/movie/search";

        public string Title { get; set; }

        public List<int> GenreIds { get; set; }

        public int? ReleaseYear { get; set; }

        public List<int> ActorIds { get; set; }
        
        public int PageNumber { get; set; } = 1;
        
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (!(Title != null || (GenreIds != null && GenreIds.Any()) || (ActorIds != null && ActorIds.Any()) || ReleaseYear != null))
            {
                validationResults.Add(new ValidationResult("Request should contain at least one not null parameter."));
            }
            
            return validationResults;
        }
        
        private int _pageSize = 20;
        private const int MaxPageSize = 100;
    }
}