using AutoMapper;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.Domain.Entities;

namespace Loan.WebApi.ApplicationServices.Mappers
{
    public class PessoaMapper: Profile
    {
        public PessoaMapper()
        {
            CreateMap<Pessoa, PessoaViewModel>()
                .ForMember(dst => dst.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dst => dst.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dst => dst.Email, m => m.MapFrom(src => src.Email))
                .ForMember(dst => dst.Telefone, m => m.MapFrom(src => src.Telefone));
        }
    }
}
