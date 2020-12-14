using AutoMapper;
using Loan.WebApi.ApplicationServices.ViewModels;
using Loan.WebApi.Domain.Entities;

namespace Loan.WebApi.ApplicationServices.Mappers
{
    public class ItemMapper: Profile
    {
        public ItemMapper()
        {
            CreateMap<Item, ItemViewModel>()
                .ForMember(dst => dst.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dst => dst.Nome, m => m.MapFrom(src => src.Nome));
        }
    }
}
