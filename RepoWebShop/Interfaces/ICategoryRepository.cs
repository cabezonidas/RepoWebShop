using System.Collections.Generic;
using RepoWebShop.Models;

namespace RepoWebShop.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
