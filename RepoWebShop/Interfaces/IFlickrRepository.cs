using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IFlickrRepository
    {
        IEnumerable<PhotosetMetadata> Albums { get; }
        AlbumPictures GetAlbumPictures(long album);
        IEnumerable<AlbumPictures> GetAlbumsPictures(IEnumerable<long> albums);
    }
}
