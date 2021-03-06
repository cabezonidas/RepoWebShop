using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.ApiControllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class AdminDataController : Controller
    {
        private readonly IPieDetailRepository _pieDetailRepository;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGalleryRepository _galleryRepository;
        private readonly IFlickrRepository _photosetAlbums;
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendar;

        public AdminDataController(ICalendarRepository calendar, AppDbContext appDbContext, IFlickrRepository photosetAlbums, IPieDetailRepository pieDetailRepository, ICategoryRepository categoryRepository, IPieRepository pieRepository, IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
            _pieDetailRepository = pieDetailRepository;
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
            _appDbContext = appDbContext;
            _photosetAlbums = photosetAlbums;
            _calendar = calendar;
        }

        [HttpPost]
        [Route("SaveAdminNumber/{number}/{username}")]
        public IActionResult SaveAdminNumber(string number, string username)
        {
            _appDbContext.AdminNotifications.Add(new AdminNotification { AdminUser = username, Phone = number });
            _appDbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteAdminNumber/{id}")]
        public IActionResult DeleteNumber(int id)
        {
            var result = _appDbContext.AdminNotifications.Where(x => x.AdminNotificationId == id);
            _appDbContext.AdminNotifications.RemoveRange(result);
            _appDbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteContact/{id}")]
        public IActionResult DeleteContact(int id)
        {
            var result = _appDbContext.Contacts.Where(x => x.ContactId == id);
            _appDbContext.Contacts.RemoveRange(result);
            _appDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            return Ok(_pieDetailRepository.PieDetails);
        }

        [HttpGet]
        [Route("GetPrices")]
        public IActionResult GetPrices()
        {
            return Ok(_pieRepository.AllPies.OrderBy(x=>x.PieDetail.Name + x.Name));
        }

        [HttpPut]
        [Route("UpdatePrice/{pieId}/{price}")]
        public IActionResult UpdatePrice(int pieId, int price)
        {
            try
            {
                _pieRepository.UpdatePrice(pieId, price);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SavePrice/{pieId}/{onlinePrice}/{storePrice}")]
        public IActionResult SavePrice(int pieId, decimal onlinePrice, decimal storePrice)
        {
            try
            {
                _pieRepository.SavePrice(pieId, onlinePrice, storePrice);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPies/{id}")]
        public IActionResult GetPies(int id)
        {
            return Ok(_pieRepository.AllPies.Where(x => x.PieDetail.PieDetailId == id));
        }

        [HttpGet]
        [Route("PieDetailHasChildren/{id}")]
        public IActionResult PieDetailHasChildren(int id)
        {
            return Ok(_pieRepository.AllPies.Where(x => x.PieDetail.PieDetailId == id).Count() > 0);
        }


        [HttpDelete]
        [Route("DeletePie/{pieId}")]
        public IActionResult DeletePie(int pieId)
        {
            try
            {
                _pieRepository.Delete(pieId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RestorePie/{pieId}")]
        public IActionResult RestorePie(int pieId)
        {
            try
            {
                _pieRepository.Restore(pieId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddFlickrSetToGallery/{setId}")]
        public IActionResult AddFlickrSetToGallery(string setId)
        {
            try
            {
                _galleryRepository.AddFlickrAlbum(setId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveFlickrFromToGallery/{setId}")]
        public IActionResult RemoveFlickrSetFromGallery(string setId)
        {
            try
            {
                _galleryRepository.RemoveFlickrAlbum(setId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("HideFlickrFromToGallery/{setId}")]
        public IActionResult HideFlickrSetFromGallery(string setId)
        {
            try
            {
                _galleryRepository.HideFlickrAlbum(setId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePieDetail/{pieDetailId}")]
        public IActionResult DeletePieDetail(int pieDetailId)
        {
            try
            {
                _pieDetailRepository.Delete(pieDetailId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RestorePieDetail/{pieDetailId}")]
        public IActionResult RestorePieDetail(int pieDetailId)
        {
            try
            {
                _pieDetailRepository.Restore(pieDetailId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ClearUnusedSessionData/{top}")]
        public async Task<IActionResult> ClearUnusedSessionData(int top)
        {
            var localTime = _calendar.LocalTime();
            var orderIds = await _appDbContext.Orders.Select(o => o.BookingId).ToArrayAsync();
            var usedLunchIds = await _appDbContext.OrderCaterings.Select(l => l.LunchId).ToArrayAsync();

            var unusedCartData = await _appDbContext.ShoppingCartData.Where(scd =>
                !orderIds.Contains(scd.BookingId) &&
                localTime.Ticks - scd.LastUpdate.Ticks > TimeSpan.TicksPerDay
            ).Take(top).ToArrayAsync();
            var unusedCartDates = await _appDbContext.ShoppingCartPickUpDates.Where(scpud =>
                !orderIds.Contains(scpud.BookingId) &&
                localTime > scpud.From
            ).Take(top).ToArrayAsync();
            var unusedCartProducts = await _appDbContext.ShoppingCartCatalogProducts.Where(sccp =>
                !orderIds.Contains(sccp.ShoppingCartId) &&
                localTime.Ticks - sccp.Created.Ticks > TimeSpan.TicksPerDay
            ).Take(top).ToArrayAsync();
            _appDbContext.ShoppingCartData.RemoveRange(unusedCartData);
            _appDbContext.ShoppingCartPickUpDates.RemoveRange(unusedCartDates);
            _appDbContext.ShoppingCartCatalogProducts.RemoveRange(unusedCartProducts);
            await _appDbContext.SaveChangesAsync();

            var unusedCartLunches = await _appDbContext.ShoppingCartCustomLunch.Where(sccl =>
                !sccl.Created.HasValue || localTime.Ticks - sccl.Created.Value.Ticks > TimeSpan.TicksPerDay
            ).ToArrayAsync();
            _appDbContext.ShoppingCartCustomLunch.RemoveRange(unusedCartLunches);
            await _appDbContext.SaveChangesAsync();

            var unusedLunches = await _appDbContext.Lunch.Where(l =>
                !usedLunchIds.Contains(l.LunchId) &&
                String.IsNullOrEmpty(l.Title) &&
                !l.IsCombo &&
                (!l.Created.HasValue || localTime.Ticks - l.Created.Value.Ticks > TimeSpan.TicksPerDay)
            ).Take(top).ToArrayAsync();

            _appDbContext.Lunch.RemoveRange(unusedLunches);
            await _appDbContext.SaveChangesAsync();

            return Ok(new
                {
                    cartDataRows = unusedCartData.Count(),
                    cartDatesRows = unusedCartDates.Count(),
                    cartProductsRows = unusedCartProducts.Count(),
                    cartLunchesRows = unusedCartLunches.Count(),
                    lunchesRows = unusedLunches.Count(),
                }
            );
        }
    }
}
