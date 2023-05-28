using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Domain;

namespace FilmsLibrary.Application
{
    public class ActorService : IActorService
    {
        private readonly IMovieRepository _repository;

        public ActorService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Actor>> GetAllActorsAsync()
        {
            var actors = await _repository.GetAllActorsAsync();

            return actors;
        }

        public async Task<int> AddActor(Actor actor)
        {
            var existingActor = await _repository.GetActorByName(actor.Name, actor.Surname);
            if (existingActor == null)
            {
                return await _repository.AddActor(actor);
            }
            return existingActor.Id;
        }
    }
}
