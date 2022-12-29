using AutoMapper;

namespace FilmsLibrary.SqlRepository.Mappers
{
    public class DomainToSqlProfile : Profile
    {
        public DomainToSqlProfile()
        {
            CreateMap<Models.Domain.Actor, Models.Sql.Actor>();

            CreateMap<Models.Domain.Comment, Models.Sql.Comment>();

            CreateMap<Models.Domain.CreateMovie, Models.Sql.Movie>()
                // .ForMember(dest => dest.GenreCode, opt => opt.MapFrom(src => (short)src.Genre))
                .ForMember(dest => dest.ActorMovies, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt=> opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Models.Domain.UpdateMovie, Models.Sql.Movie>()
                .ForMember(dest => dest.ActorMovies, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());
                // .ForMember(dest => dest.GenreCode, opt => opt.MapFrom(src => (short)src.Genre));
        }
    }
}
