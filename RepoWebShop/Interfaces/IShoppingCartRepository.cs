﻿using RepoWebShop.FeModels;
using RepoWebShop.Models;
using RepoWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        long GetCuit(string bookingId);
        void AddCuitToCart(string bookingId, long cuit);
        void RemoveCuitFromCart(string bookingId);
        IEnumerable<ShoppingCartItem> GetItems(string bookingId);
		_Totals GetTotals(string bookingId);
		Task<_Totals> GetTotalsAsync(string bookingId);
		IEnumerable<ShoppingCartCatalogItem> GetCatalogItems(string bookingId);
        IEnumerable<ShoppingCartComboCatering> GetShoppingCaterings(string bookingId);
        ShoppingCartLunch GetSessionLunch(string bookingId = null);
        ShoppingCartLunch GetOrCreateSessionLunch(string bookingId = null);
		IEnumerable<_PickUpOptions> PickUpOptionsByDay();
		ShoppingCartComment GetComments(string bookingId);
        DeliveryAddress GetDelivery(string bookingId);
        Discount GetDiscount(string bookingId);
		void RemoveCatalogItemFromCart(int id);
		decimal GetTotalWithoutDiscount(string bookingId);
		decimal GetSubtotalWithoutDelivery(string bookingId);


		decimal GetProductsTotal(string bookingId);
        decimal GetCateringsTotal(string bookingId);
        int GetPreparationTime(string bookingId);
        Task ClearCartAsync(string bookingId);
        decimal GetLunchTotal(Lunch lunch);
        decimal GetTotal(string bookingId);

        decimal GetLunchTotalInStore(Lunch lunch);
		decimal GetCustomCateringTotals(string bookingId);
		decimal GetCustomCateringTotalsInStore(string bookingId);
		decimal GetCateringsTotalInStore(string bookingId);
        decimal GetLunchOnlineSavings(Lunch lunch);
        decimal GetCateringsTotalSavings(string bookingId);
        decimal GetProductsTotalInStore(string bookingId);
        decimal GetProductsOnlineSavings(string bookingId);
		_Catering SessionCatering();
		void AddComments(string comments);
        void ClearFromCart(int pieId);
        void ClearCatalogItemFromCart(int productId);
        void ClearCateringFromCart(int cateringId);

        void AddToCart(Pie pie, int amount);
        void AddCatalogItemToCart(Product product, int v);
        void AddCateringToCart(Lunch catering, int amount);
		void AddCatalogItemToCart(int id);
		int RemoveFromCart(Pie pie);
        int RemoveProductFromCart(Product product);
        void RemoveLunchFromCart(Lunch lunch);

        void RemoveDelivery();
        void RemoveShoppingDiscount();
        void AddDiscount(Discount discount);
		_Catering Map(Lunch c);
		void RenewId();
        string GetSessionCartId();
        string GetMpPreference(string bookingId);
        void SetMpPreference(string preferenceId);
        bool TrySetPickUpDate(string bookingId, DateTime pickUpDate, out string error);
        ShoppingCartPickUpDate GetPickUpDate(string bookingId);
        PickUpTimeViewModel GetTimeSlots(string bookingId);
        void AcknowledgeSystemTime(string bookingId);
        SessionDetailsViewModel SessionsDetails(string friendlyBookingId = null);
        IEnumerable<ShoppingCartComboCatering> AllShoppingCartCaterings();
        IEnumerable<string> GetPendingBookings();
        int CountTrolleyObjects(string bookingId);
        void AddCateringToOrder(ShoppingCartLunch customLunch);
        ShoppingCartLunch GetSessionLunchIfNotEmpty(string bookingId = null);
        void ClearCustomCateringFromCart();
        decimal GetTotalInStore(string bookingId);
		Task<ShoppingCartViewModel> GetShoppingCartViewModel();

		IEnumerable<_CartCatering> GetCartCaterings(string bookingId);
	}
}
