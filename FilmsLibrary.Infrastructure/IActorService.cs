using FilmsLibrary.Models.Domain;

namespace FilmsLibrary.Infrastructure
{
    public interface IActorService
    {
        Task<List<Actor>> GetAllActorsAsync();
    }
}
