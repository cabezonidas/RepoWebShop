using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICalendarRepository _calendarRepository;

        public ShoppingCartRepository(IMapper mapper, AppDbContext appDbContext, ICalendarRepository calendarRepository)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _calendarRepository = calendarRepository;
        }

        private ShoppingCartComment GetShoppingCartComments(string bookingId)
        {
            return _appDbContext.ShoppingCartComments.FirstOrDefault(x => x.ShoppingCartId == bookingId);
        }

        public IQueryable<ShoppingCartItem> GetItems(string bookingId)
        {
            return _appDbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == bookingId).Include(x => x.Pie).ThenInclude(x => x.PieDetail);
        }

        public ShoppingCartComment GetComments(string shoppingCartId)
        {
            var shoppingCartComment = _appDbContext.ShoppingCartComments
                .Where(c => c.ShoppingCartId == shoppingCartId)
                .OrderByDescending(c => c.Created)
                .FirstOrDefault();

            return shoppingCartComment;
        }

        public string ClearComments(string bookingId)
        {
            var result = GetComments(bookingId);
            if(result != null)
            {
                var comments = _appDbContext.ShoppingCartComments.Where(x => x.ShoppingCartId == bookingId);
                _appDbContext.ShoppingCartComments.RemoveRange(comments);
            }
                
            return result?.Comments ?? String.Empty;
        }

        public IQueryable<ShoppingCartItem> EmptyItems(string bookingId)
        {
            var result = GetItems(bookingId);
            _appDbContext.ShoppingCartItems.RemoveRange(result);
            return result;
        }
    }
}
