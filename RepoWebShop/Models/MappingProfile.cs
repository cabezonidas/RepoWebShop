using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PieDetailCreateViewModel, PieDetail>();
            CreateMap<PieDetail, PieDetailCreateViewModel>();
            CreateMap<PieDetail, PieDetail>().ForMember(x => x.PieDetailId, opt => opt.Ignore());
            CreateMap<Order, OrderStatusViewModel>();
            CreateMap<PaymentNotice, Order>();
            CreateMap<IdentityUser, ApplicationUser>();
        }
    }
}
