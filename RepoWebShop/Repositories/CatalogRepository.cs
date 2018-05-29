﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Models
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IMapper _mapper;

        public CatalogRepository(IMapper mapper, IPieDetailRepository pieDetailRepository, AppDbContext appDbContext, ICalendarRepository calendarRepository)
        {
            _mapper = mapper;
            _pieDetailRepository = pieDetailRepository;
            _appDbContext = appDbContext;
            _calendarRepository = calendarRepository;
        }

        public void Activate(int productId)
        {
            var result = GetAll().FirstOrDefault(x => x.ProductId == productId);
            if(result != null)
            {
                result.IsActive = true;
                _appDbContext.Products.Update(result);
                _appDbContext.SaveChanges();
            }
        }

        public void ApplyPriceRise(decimal percentage, int roundTo)
        {
            var products = _appDbContext.Products.ToList();
            foreach (var product in products)
            {
                product.OldPrice = product.Price;
                product.OldPriceInStore = product.PriceInStore;
                product.Price = product.Price.ApplyPercentage(percentage).RoundTo(5);
                product.PriceInStore = product.PriceInStore.ApplyPercentage(percentage).RoundTo(5);
            }

            _appDbContext.Products.UpdateRange(products);
            _appDbContext.SaveChanges();
        }

        public void Create(Product product)
        {
            if(product.PieDetailId.HasValue && product.PieDetailId != -1)
            {
                var pieDetail = _pieDetailRepository.GetPieDetailById(product.PieDetailId.Value);
                product.PieDetail = pieDetail;
            }
            product.IsActive = true;
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void Deactivate(int productId)
        {
            var product = GetAll().FirstOrDefault(x => x.ProductId == productId);

            if(product != null)
            {
                product.IsActive = false;
                _appDbContext.Update(product);
                _appDbContext.SaveChanges();
            }
        }

        public void Edit(Product product)
        {
            if (product.PieDetailId.HasValue && product.PieDetailId != -1)
            {
                var pieDetail = _pieDetailRepository.GetPieDetailById(product.PieDetailId.Value);
                product.PieDetail = pieDetail;
            }
            else
            {
                product.PieDetail = null;
                product.PieDetailId = null;
            }
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Product> GetActive()
        {
            return GetAll().Where(x => x.IsActive).ToList();
        }

        public IEnumerable<Product> GetAvailableToBuyOnline()
        {
            return GetAll().Where(x => x.IsActive && x.IsOnSale).ToList();
        }

        public IEnumerable<Product> GetAll()
        {
            return _appDbContext.Products.Include(x => x.PieDetail).ToList();
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ProductId == id);
        }

        public IEnumerable<ProductInflationEstimateViewModel> InflationEstimate(decimal percentage, int roundTo)
        {
            var result = GetAll().Select(x => new ProductInflationEstimateViewModel { Product = x, EstimateOnline = x.Price.ApplyPercentage(percentage).RoundTo(roundTo), EstimateInStore = x.PriceInStore.ApplyPercentage(percentage).RoundTo(roundTo) }).ToList();
            return result;
        }

        public void RestorePrices()
        {
            var products = _appDbContext.Products.ToList();
            foreach (var product in products)
            {
                product.Price = product.OldPrice ?? product.Price;
            }

            _appDbContext.Products.UpdateRange(products);
            _appDbContext.SaveChanges();
        }

        public Product CreateOrUpdate(ProductViewModel vm)
        {
            var existingProduct = _appDbContext.Products.FirstOrDefault(x => x.ProductId == vm.ProductId);
            var result = existingProduct != null ? _mapper.Map(vm, existingProduct) : _mapper.Map<ProductViewModel, Product>(vm);

            if(existingProduct != null)
            {
                result.OldPrice = vm.Price != existingProduct.Price ? existingProduct.Price : existingProduct.OldPrice;
                result.OldPriceInStore = vm.PriceInStore != existingProduct.PriceInStore ? existingProduct.PriceInStore : existingProduct.OldPriceInStore;
            }

            //result.Description = result.Description.ToTitleCase();
            //result.Name = result.Name.ToTitleCase();
            result.Category = result.Category.ToTitleCase();
            result.Flavour = result.Flavour.ToTitleCase();
            result.SizeDescription = result.SizeDescription.ToTitleCase();
            result.Temperature = result.Temperature.ToTitleCase();
            result.PieDetail = _pieDetailRepository.GetPieDetailById(vm.PieDetailId);
            

            if (existingProduct != null)
                _appDbContext.Products.Update(result);
            else
                _appDbContext.Products.Add(result);

            _appDbContext.SaveChanges();
            return result;
        }

        public IEnumerable<Product> GetByParent(int id) => _pieDetailRepository.GetChildren(id);

        public void QuickUpdate(int productId, decimal price, decimal priceInStore, bool onlineSale, string category, string temp, int minAmountOrder, int increments, int portions, int prepTime)
        {
            var prod = _appDbContext.Products.First(x => x.ProductId == productId);
            prod.OldPrice = price != prod.Price ? prod.Price : prod.OldPrice;
            prod.OldPriceInStore = priceInStore != prod.PriceInStore ? prod.PriceInStore : prod.OldPriceInStore;
            prod.Price = price;
            prod.PriceInStore = priceInStore;
            prod.IsOnSale = onlineSale;
            prod.Category = category.ToTitleCase();
            prod.Temperature = temp.ToTitleCase();
            prod.MinOrderAmount = minAmountOrder;
            prod.MultipleAmount = increments;
            prod.Portions = portions;
            prod.PreparationTime = prepTime;
            _appDbContext.Products.Update(prod);
            _appDbContext.SaveChanges();
        }
    }
}
