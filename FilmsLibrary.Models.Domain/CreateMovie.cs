using System.Collections.Generic;

namespace FilmsLibrary.Models.Domain
{
    public class CreateMovie
    {
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        public int GenreId { get; set; }

        public int Rating { get; set; }
        
        public string ImageUrl { get; set; }

        public List<int> ActorIds { get; set; }
    }
}