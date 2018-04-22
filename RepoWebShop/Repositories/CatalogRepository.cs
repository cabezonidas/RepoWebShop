using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.States;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Models
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendarRepository;

        public CatalogRepository(AppDbContext appDbContext, ICalendarRepository calendarRepository)
        {
            _appDbContext = appDbContext;
            _calendarRepository = calendarRepository;
        }

        public void Activate(int productId)
        {
            var result = _appDbContext.Products.FirstOrDefault(x => x.ProductId == productId);
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
                product.Price = product.Price.ApplyPercentage(percentage).RoundTo(5);
            }

            _appDbContext.Products.UpdateRange(products);
            _appDbContext.SaveChanges();
        }

        public void Create(Product product)
        {
            product.IsActive = true;
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void Deactivate(int productId)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.ProductId == productId);

            if(product != null)
            {
                product.IsActive = false;
                _appDbContext.Update(product);
                _appDbContext.SaveChanges();
            }
        }

        public void Edit(Product product)
        {
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Product> GetActive()
        {
            return _appDbContext.Products.Where(x => x.IsActive).ToList();
        }

        public IEnumerable<Product> GetAll()
        {
            return _appDbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _appDbContext.Products.FirstOrDefault(x => x.IsActive);
        }

        public IEnumerable<ProductInflationEstimateViewModel> InflationEstimate(decimal percentage, int roundTo)
        {
            var result = GetAll().Select(x => new ProductInflationEstimateViewModel { Product = x, Estimate = x.Price.ApplyPercentage(percentage).RoundTo(roundTo) }).ToList();
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
    }
}
