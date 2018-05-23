﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PieRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [Obsolete("Pie objects are deprecated, please use catalog products instead.")]
        public IEnumerable<Pie> ActivePies
        {
            get
            {
                //return _appDbContext.Pies
                //    .Include(p => p.PieDetail)
                //    .Where(x => x.IsActive && x.PieDetail.IsActive);
                return new List<Pie>();
            }
        }

        [Obsolete("Pie objects are deprecated, please use catalog products instead.")]
        public IEnumerable<Pie> AllPies
        {
            get
            {
                //return _appDbContext.Pies.Include(p => p.PieDetail);
                return new List<Pie>();
            }
        }

        public IEnumerable<PieDetail> PiesOfTheWeek //To be removed. Must be replaced from IPieDetailRepository
        {
            get
            {
                var activePies = _appDbContext.Pies.Where(x => x.IsActive == true).Select(x => x.PieDetailId).Distinct();
                return _appDbContext.PieDetails.Where(p => p.IsActive && p.IsPieOfTheWeek && activePies.Contains(p.PieDetailId));
            }
        }

        public Pie Add(Pie pie)
        {
            var result = _appDbContext.Pies.Add(pie);
            _appDbContext.SaveChanges();
            return pie;

        }

        public void Delete(int pieId)
        {
            Pie pie = _appDbContext.Pies.FirstOrDefault(x => x.PieId == pieId);
            pie.IsActive = false;
            _appDbContext.SaveChanges();
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.Include(p => p.PieDetail).FirstOrDefault(p => p.PieId == pieId);
        }

        public void Restore(int pieId)
        {
            var pie = _appDbContext.Pies.FirstOrDefault(x => x.PieId == pieId);
            pie.IsActive = true;
            _appDbContext.SaveChanges();
        }

        public void SavePrice(int pieId, decimal onlinePrice, decimal storePrice)
        {
            var pie = _appDbContext.Pies.FirstOrDefault(x => x.PieId == pieId);
            pie.Price = onlinePrice;
            pie.StorePrice = storePrice;
            _appDbContext.Update(pie);
            _appDbContext.SaveChanges();
        }

        public Task<int> Update(Pie pie)
        {
            Pie oldPieDetail = _appDbContext.Pies.First(x => x.PieId == pie.PieId);

            oldPieDetail.IsActive = pie.IsActive;
            oldPieDetail.Name = pie.Name;
            oldPieDetail.Price = pie.Price;
            oldPieDetail.StorePrice = pie.StorePrice;
            oldPieDetail.SizeDescription = pie.SizeDescription;

            return _appDbContext.SaveChangesAsync();
        }

        public void UpdatePrice(int pieId, int price)
        {
            var pie = _appDbContext.Pies.FirstOrDefault(x => x.PieId == pieId);
            pie.Price = price;
            _appDbContext.SaveChanges();
        }
    }
}
