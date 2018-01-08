using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class AlbumPictures
    {
        public Photoset Photoset { get; set; }
        public string Stat { get; set; }

        [BindNever]
        public IEnumerable<Photo> Pictures
        {
            get
            {
                try
                {
                    return Photoset.Photo;
                }
                catch
                {
                    return new List<Photo>().AsEnumerable();
                }
            }

        }

        [BindNever]
        public IEnumerable<string> PicturesUrls
        {
            get
            {
                try
                {
                    return Pictures.Select(x => $"https://farm{x.Farm}.staticflickr.com/{x.Server}/{x.Id}_{x.Secret}_");
                }
                catch
                {
                    return new List<string>().AsEnumerable();
                }
            }
        }

        [BindNever]
        public string PrimaryPicture
        {
            get
            {
                try
                {
                    Photo photo = Pictures.First(x => x.IsPrimary == "1");
                    return $"https://farm{photo.Farm}.staticflickr.com/{photo.Server}/{photo.Id}_{photo.Secret}_";
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
