using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FilmsLibrary.Mappers
{
    public class DomainToContractProfile : Profile
    {
        public DomainToContractProfile()
        {
            CreateMap<Models.Domain.Movie, Models.Contracts.Models.Movie>();

            CreateMap<Models.Domain.Actor, Models.Contracts.Models.Actor>();

            CreateMap<Models.Domain.Comment, Models.Contracts.Models.Comment>();

            CreateMap<Models.Domain.Movie, Models.Contracts.Models.GetSimilarMovies.Movie>();

            CreateMap<Models.Domain.Genre, Models.Contracts.Models.Genre>();
        }
    }
}
