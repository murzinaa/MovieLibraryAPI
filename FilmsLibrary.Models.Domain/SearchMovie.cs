using System.Collections.Generic;

namespace FilmsLibrary.Models.Domain
{
    public class SearchMovie
    {
        public string Title { get; set; }

        public List<int> GenreIds { get; set; }

        public int? ReleaseYear { get; set; }

        public List<int> ActorIds { get; set; }
    }
}