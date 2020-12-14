using AutoMapper;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.Domain.Entities;

namespace Loan.WebApi.ApplicationServices.Mappers
{
    public class EmprestimoMapper: Profile
    {
        public EmprestimoMapper()
        {
            CreateMap<Emprestimo, EmprestimoViewModel>()
                .ForMember(dst => dst.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dst => dst.Data, m => m.MapFrom(src => src.Data))
                .ForMember(dst => dst.Devolucao, m => m.MapFrom(src => src.Devolucao))
                .ForMember(dst => dst.Pessoa, m => m.MapFrom(src => src.Pessoa))
                .ForMember(dst => dst.Item, m => m.MapFrom(src => src.Item));
        }
    }
}
