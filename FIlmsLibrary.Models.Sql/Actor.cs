using System.Collections.Generic;

namespace FilmsLibrary.Models.Sql
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
    }
}
