﻿using System.Collections.Generic;
using RepoWebShop.Models;
using System.Threading.Tasks;
using RepoWebShop.ViewModels;

namespace RepoWebShop.Interfaces
{
    public interface IPieDetailRepository
    {
        // Dictionary<int, string> TimeEstimations(IEnumerable<Product> products);
        PieDetailViewModel MapDbPieDetailToPieDetailViewModel(PieDetail dbPieDetail);
        IEnumerable<PieDetail> PieDetails { get; }
        IEnumerable<PieDetail> PieDetailsWithChildren { get; }
        PieDetail GetPieDetailById(int pieDetailId);
        Task<int> Add(PieDetail pieDetail);
        Task<int> Update(PieDetail pieDetail);
        void Delete(int pieDetailId);
        void Restore(int pieDetailId);
        void UpdateIngredients(ProductViewModel vm);
        IEnumerable<Product> GetChildren(int id);
        IEnumerable<Product> GetCatalogChildren(int id);
    }
}
