using AutoMapper;   
using Microsoft.AspNetCore.Identity;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
using RepoWebShop.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;

namespace RepoWebShop.Models
{
    public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			CreateMap<_ProviderData, ApplicationUser>()
				.ForMember(x => x.UserName, opt => {
					if (opt.DestinationMember != null)
						opt.UseDestinationValue();
					else
						opt.MapFrom(src => src.Email);
				})
				.ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(x => x.FirstName, opt => opt.ResolveUsing(src => src.DisplayName.Split(' ').FirstOrDefault()))
				.ForMember(x => x.LastName, opt => opt.ResolveUsing(src => {
					var names = src.DisplayName.Split(' ');
					var firstNameLength = names.FirstOrDefault()?.Length ?? 0;
					return names.Count() > 1 && firstNameLength > 0 && src.DisplayName.Length > firstNameLength ? src.DisplayName.Substring(firstNameLength).TrimStart() : string.Empty;
				}))
				.ForMember(x => x.FacebookNameIdentifier, opt => opt.ResolveUsing(src => {
					return src.ProviderId.ToLower() == "facebook" ? src.Uid : null;
				}))
				.ForMember(x => x.GoogleNameIdentifier, opt => opt.ResolveUsing(src => {
					return src.ProviderId.ToLower() == "google" ? src.Uid : null;
				}));


			CreateMap<ApplicationUser, _User>();
			CreateMap<AlbumPictures, _Album>()
				.ForMember(x => x.AlbumId, opt => opt.MapFrom(src => src.Photoset.Id))
				.ForMember(x => x.Title, opt => opt.MapFrom(src => src.Photoset.Title))
				.ForMember(x => x.PrimaryPicture, opt => opt.MapFrom(src => src.PrimaryPicture))
				.ForMember(x => x.Photos, opt => opt.MapFrom(src => src.Pictures));



			CreateMap<Product, _Item>();
			CreateMap<PieDetail, _Product>()
				.ForMember(x => x.FlickrAlbumId, opt => opt.MapFrom(src => src.FlickrAlbumId == 0 ? string.Empty : src.FlickrAlbumId.ToString()));

			CreateMap<FECAEResponse.FECAECabResponse, InvoiceData>();
            CreateMap<FECAEResponse.FECAEDetResponse, Cae>();

            CreateMap<ElectronicInvoiceTest.Err, FECAEResponse.CodeMessage>();
            CreateMap<ElectronicInvoiceTest.Evt, FECAEResponse.CodeMessage>();
            CreateMap<ElectronicInvoiceTest.FECAECabResponse, FECAEResponse.FECAECabResponse>();
            CreateMap<ElectronicInvoiceTest.FECAEDetResponse, FECAEResponse.FECAEDetResponse>().ForMember(x => x.Observaciones,
                opt => opt.MapFrom(src => src.Observaciones.Select(x => new FECAEResponse.CodeMessage { Code = x.Code, Msg = x.Msg })));
            CreateMap<FECAEDetRequest, ElectronicInvoiceTest.FECAEDetRequest>().ForMember(x => x.Iva,
                opt => opt.MapFrom(src => src.Iva.Select(x => new ElectronicInvoiceTest.AlicIva { Id = x.Id, BaseImp = x.BaseImp, Importe = x.Importe })));
            CreateMap<FECAECabRequest, ElectronicInvoiceTest.FECAECabRequest>();
            CreateMap<FEAuthRequest, ElectronicInvoiceTest.FEAuthRequest>();

            CreateMap<ElectronicInvoiceProd.Err, FECAEResponse.CodeMessage>();
            CreateMap<ElectronicInvoiceProd.Evt, FECAEResponse.CodeMessage>();
            CreateMap<ElectronicInvoiceProd.FECAECabResponse, FECAEResponse.FECAECabResponse>();
            CreateMap<ElectronicInvoiceProd.FECAEDetResponse, FECAEResponse.FECAEDetResponse>().ForMember(x => x.Observaciones,
                opt => opt.MapFrom(src => src.Observaciones.Select(x => new FECAEResponse.CodeMessage { Code = x.Code, Msg = x.Msg })));
            CreateMap<FECAEDetRequest, ElectronicInvoiceProd.FECAEDetRequest>().ForMember(x => x.Iva,
                opt => opt.MapFrom(src => src.Iva.Select(x => new ElectronicInvoiceProd.AlicIva { Id = x.Id, BaseImp = x.BaseImp, Importe = x.Importe })));
            CreateMap<FECAECabRequest, ElectronicInvoiceProd.FECAECabRequest >();
            CreateMap<FEAuthRequest, ElectronicInvoiceProd.FEAuthRequest>();

            CreateMap<Lunch, LunchTicketViewModel>();
            CreateMap<LunchTicketViewModel, Lunch>();
            CreateMap<Product, ProductEstimationViewModel>();
            CreateMap<ProductEstimationViewModel, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<OrderCatering, ShoppingCartComboCatering>();
            CreateMap<ShoppingCartComboCatering, OrderCatering>();
            CreateMap<LunchComboViewModel, Lunch>();
            CreateMap<Lunch, LunchComboViewModel>();
            CreateMap<DeliveryAddress, DeliveryAddressViewModel>();
            CreateMap<DeliveryAddress, DeliveryAddressViewModel>();

            CreateMap<ExternalLoginInfo, ApplicationUser>()
                .ForMember(x => x.ValidationMailToken, opt => opt.MapFrom(y => DateTime.Now))
                .ForMember(x => x.UserName, opt =>
                {
                    if (opt.DestinationMember != null)
                        opt.UseDestinationValue();
                    else
                    {
                        opt.PreCondition(w => !string.IsNullOrEmpty(w.Principal.GetClaimValue(ClaimTypes.Email)));
                        opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.Email));
                    }
                })
                .ForMember(x => x.Email, opt =>
                {
                    opt.PreCondition(w => !string.IsNullOrEmpty(w.Principal.GetClaimValue(ClaimTypes.Email)));
                    opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.Email));
                })
                .ForMember(x => x.FacebookNameIdentifier, opt =>
                {
                    opt.PreCondition(w => w.LoginProvider == "Facebook");
                    opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.NameIdentifier));
                })
                .ForMember(x => x.GoogleNameIdentifier, opt =>
                {
                    opt.PreCondition(w => w.LoginProvider == "Google");
                    opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.NameIdentifier));
                })
                .ForMember(x => x.EmailConfirmed, opt =>
                {
                    opt.PreCondition(w => w.Principal.GetClaimValue(ClaimTypes.Email).Length > 0);
                    opt.MapFrom(y => true);
                })
                .ForMember(x => x.FirstName, opt => opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.GivenName)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.Surname)))
                .ForMember(x => x.AddressLine1, opt => opt.UseDestinationValue())
                .ForMember(x => x.Gender, opt => opt.MapFrom(y => y.Principal.GetClaimValue(ClaimTypes.Gender)))
                .ForMember(x => x.DateOfBirth, opt =>
                {
                    opt.PreCondition(w =>
                    {
                        var date = w.Principal.GetClaimValue(ClaimTypes.DateOfBirth);
                        DateTime result;
                        return DateTime.TryParse(date, out result);
                    });
                    opt.MapFrom(z => DateTime.Parse(z.Principal.GetClaimValue(ClaimTypes.DateOfBirth)));
                });
            CreateMap<RegisterProviderWithMailViewModel, ApplicationUser>()
                .ForMember(x => x.ValidationMailToken, opt => opt.MapFrom(y => DateTime.Now))
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.Email));
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(x => x.ValidationMailToken, opt => opt.MapFrom(y => DateTime.Now));
            CreateMap<IdentityUser, ApplicationUser>();
            CreateMap<ApplicationUserViewModel, ApplicationUser>();

            CreateMap<ApplicationUser, HashValidationViewModel>();
            CreateMap<ApplicationUser, RegisterProviderWithMailViewModel>();

            CreateMap<DeliveryAddress, DeliveryAddressViewModel>();
            CreateMap<DeliveryAddressViewModel, DeliveryAddress>();
            CreateMap<PieDetailCreateViewModel, PieDetail>();
            CreateMap<PieDetail, PieDetailCreateViewModel>();
            CreateMap<PieDetail, PieDetail>().ForMember(x => x.PieDetailId, opt => opt.Ignore());
            CreateMap<Order, OrderStatusViewModel>();
            CreateMap<PaymentNotice, Order>();
            CreateMap<WorkingHours, OpenHours>();
            CreateMap<WorkingHours, ProcessingHours>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
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
