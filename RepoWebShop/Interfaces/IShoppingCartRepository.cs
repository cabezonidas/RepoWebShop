using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        IEnumerable<ShoppingCartItem> GetItems(string bookingId);
        IEnumerable<ShoppingCartCatalogItem> GetCatalogItems(string bookingId);
        IEnumerable<ShoppingCartComboCatering> GetShoppingCaterings(string bookingId);


        ShoppingCartComment GetComments(string bookingId);
        DeliveryAddress GetDelivery(string bookingId);
        Discount GetDiscount(string bookingId);
        decimal GetTotalWithoutDiscount(string bookingId);

        decimal GetItemsAndCatalogProductsTotal(string bookingId);
        decimal GetCateringsTotal(string bookingId);
        int GetPreparationTime(string bookingId);
        void ClearCart(string bookingId);
        decimal GetLunchTotal(Lunch lunch);
        decimal GetTotal(string bookingId);

        void AddComments(string comments);
        void ClearFromCart(int pieId);
        void ClearCatalogItemFromCart(int productId);
        void ClearCateringFromCart(int cateringId);

        void AddToCart(Pie pie, int amount);
        void AddCatalogItemToCart(Product product, int v);
        void AddCateringToCart(Lunch catering, int amount);

        int RemoveFromCart(Pie pie);
        int RemoveProductFromCart(Product product);
        void RemoveLunchFromCart(Lunch lunch);

        void RemoveDelivery();
        void RemoveShoppingDiscount();
        void AddDiscount(Discount discount);
        
        void RenewId();
        string GetSessionCartId();
        string GetMpPreference(string bookingId);
        void SetMpPreference(string preferenceId);
        bool TrySetPickUpDate(string bookingId, DateTime pickUpDate, out string error);
        ShoppingCartPickUpDate GetPickUpDate(string bookingId);
        PickUpTimeViewModel GetTimeSlots(string bookingId);
        void AcknowledgeSystemTime(string bookingId);
        SessionDetailsViewModel SessionsDetails();
        IEnumerable<ShoppingCartComboCatering> AllShoppingCartCaterings();
        int CountTrolleyObjects(string bookingId);
    }
}
