using AutoMapper;
using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Contracts;
using FilmsLibrary.Models.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsLibrary.Controllers
{
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        public ActorController(IActorService actorService, IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all actors.
        /// </summary>
        /// <returns>GetActorsResponse. List of all actors.</returns>
        [HttpGet(GetActorsRequest.Route)]
        [ProducesResponseType(typeof(GetActorsResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<GetActorsResponse> GetAllActorsAsync()
        {
            var actors = await _actorService.GetAllActorsAsync();
            var result = _mapper.Map<List<Actor>>(actors);

            return new GetActorsResponse
            {
                Actors = result,
            };
        }

        /// <summary>
        /// Creates actor and returns id of created actor. If actor with given first name and last name exists,
        /// the id of existing actor will be returned.
        /// </summary>
        /// <returns>An id of new actor.</returns>
        [HttpPost(AddActorRequest.Route)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<AddActorResponse> AddActor(AddActorRequest request)
        {
            var actor = _mapper.Map<Models.Domain.Actor>(request);
            int actorId = await _actorService.AddActor(actor);
            return new AddActorResponse
            {
                Id = actorId
            };
        }
    }
}
