using AutoMapper;
using FilmsLibrary.Models.Contracts;
using FilmsLibrary.Models.Domain;

namespace FilmsLibrary.Mappers
{
    public class ContractToDomainProfile : Profile
    {
        public ContractToDomainProfile()
        {
            CreateMap<CreateMovieRequest, CreateMovie>();

            CreateMap<UpdateMovieRequest, UpdateMovie>();
        }
    }
}
