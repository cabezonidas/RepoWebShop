using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IPhotosetAlbums
    {
        PhotosetList Photosets { get; }
        IEnumerable<string> GetPieDetailFotos(long flickrAlbumId);
        string GetPrimaryPicture(long flickrAlbumId);
        IEnumerable<PhotosetPhotos> GetGalleryPictures(IEnumerable<long> albums);
    }
}
