using AutoMapper;

namespace FilmsLibrary.SqlRepository.Mappers
{
    public class SqlToDomainProfile : Profile
    {
        public SqlToDomainProfile()
        {
            CreateMap<Models.Sql.Comment, Models.Domain.Comment>();

            CreateMap<Models.Sql.Actor, Models.Domain.Actor>();

            CreateMap<Models.Sql.Movie, Models.Domain.Movie>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorMovies.Select(x => x.Actor)));

            CreateMap<Models.Sql.Genre, Models.Domain.Genre>();
        }
    }
}
