using FilmsLibrary.Models.Sql;
using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.SqlRepository.Utilities
{
    public static class DatabaseInitializer
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Actor>().HasData(
                new Actor
                {
                    Id = 1,
                    Name = "Orlando",
                    Surname = "Bloom"
                },
                new Actor
                {
                    Id = 2,
                    Name = "Johnny",
                    Surname = "Depp"
                });
            builder.Entity<Movie>().HasData(new Movie
            {
                Id = 1,
                Title = "Pirates of the Caribbean: The Curse of the Black Pearl",
                Description = "Blacksmith Will Turner teams up with eccentric pirate Captain " +
                              "Jack Sparrow to save his love, the governor's daughter, from Jack's former pirate allies, who are now undead.",
                ReleaseYear = 2003,
                Rating = 80,
            });
           builder.Entity<ActorMovie>().HasData(new ActorMovie
           {
               Id = 1,
               ActorId = 1,
               MovieId = 1,
           },
           new ActorMovie
           {
               Id = 2,
               ActorId = 2,
               MovieId = 1,
           });
        }
    }
}
