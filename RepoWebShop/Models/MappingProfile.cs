using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
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
            CreateMap<WorkingHours, OpenHours>();
            CreateMap<WorkingHours, ProcessingHours>();
            CreateMap<AddSpecialDateViewModel, PublicHoliday>()
                .ForMember(x => x.OpenHours,
                    opt => opt.MapFrom(src => src.OpenHoursStartingAt.HasValue && src.OpenHoursFinishingAt.HasValue  ? 
                    new OpenHours()
                    {
                        StartingAt = src.OpenHoursStartingAt.Value,
                        Duration = src.OpenHoursFinishingAt.Value.Subtract(src.OpenHoursStartingAt.Value),
                        DayId = 8
                    } : null))
                .ForMember(x => x.ProcessingHours,
                    opt => opt.MapFrom(src => src.ProcessingHoursStartingAt.HasValue && src.ProcessingHoursFinishingAt.HasValue ?
                    new ProcessingHours()
                    {
                        StartingAt = src.ProcessingHoursStartingAt.Value,
                        Duration = src.ProcessingHoursFinishingAt.Value.Subtract(src.ProcessingHoursStartingAt.Value),
                        DayId = 8
                    } : null));
        }
    }
}
