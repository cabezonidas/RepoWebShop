using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public interface IPieDetailRepository
    {
        IEnumerable<PieDetail> PieDetails { get; }
        IEnumerable<PieDetail> PieDetailsWithChildren { get; }
        PieDetail GetPieDetailById(int pieDetailId);
        Task<int> Add(PieDetail pieDetail);
        Task<int> Update(PieDetail pieDetail);
        void Delete(int pieDetailId);
    }
}
