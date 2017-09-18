using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> Pies { get; }
        IEnumerable<PieDetail> PiesOfTheWeek { get; }
        Pie GetPieById(int pieId);
        Pie Add(Pie pie);
        void Delete(int pieId);
    }
}
