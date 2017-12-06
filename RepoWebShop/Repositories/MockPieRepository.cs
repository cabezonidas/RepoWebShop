using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using RepoWebShop.Models;

namespace RepoWebShop.Repositories
{
    public class MockPieRepository : IPieRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();
        
        public IEnumerable<Pie> ActivePies
        {
            get
            {
                return new List<Pie>
                {
                    new Pie {PieId = 1, Name="Strawberry Pie", Price=15.95M },
                    new Pie {PieId = 2, Name="Cheese cake", Price=18.95M },
                    new Pie {PieId = 3, Name="Rhubarb Pie", Price=15.95M },
                    new Pie {PieId = 4, Name="Pumpkin Pie", Price=12.95M }
                };
            }
        }

        public IEnumerable<Pie> AllPies => throw new NotImplementedException();

        IEnumerable<PieDetail> IPieRepository.PiesOfTheWeek => throw new NotImplementedException();

        public Pie Add(Pie pie)
        {
            throw new NotImplementedException();
        }

        public void Delete(int pieId)
        {
            throw new NotImplementedException();
        }

        public Pie GetPieById(int pieId)
        {
            throw new System.NotImplementedException();
        }

        public void Restore(int pieId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrice(int pieId, int price)
        {
            throw new NotImplementedException();
        }
    }
}
