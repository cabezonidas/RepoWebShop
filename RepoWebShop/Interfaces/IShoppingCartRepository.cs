using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        IEnumerable<ShoppingCartItem> GetItems(string bookingId);
        IEnumerable<ShoppingCartCatalogItem> GetCatalogItems(string bookingId);
        ShoppingCartComment GetComments(string bookingId);
        DeliveryAddress GetDelivery(string bookingId);
        Discount GetDiscount(string bookingId);
        decimal GetTotalWithoutDiscount(string bookingId);

        decimal GetItemsAndCatalogProductsTotal(string bookingId);
        int GetPreparationTime(string bookingId);
        void ClearCart(string bookingId);
        decimal GetTotal(string bookingId);

        void AddComments(string comments);
        void ClearFromCart(int pieId);
        void ClearCatalogItemFromCart(int productId);
        void AddToCart(Pie pie, int amount);
        void AddCatalogItemToCart(Product product, int v);
        int RemoveFromCart(Pie pie);
        int RemoveProductFromCart(Product product);
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
    }
}
