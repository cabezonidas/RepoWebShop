using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _ImagesController : Controller
    {
		private readonly IFlickrRepository _flickrRepo;

		public _ImagesController(IFlickrRepository flickrRepo)
		{
			_flickrRepo = flickrRepo;
		}

		[HttpGet]
		[Route("Album/{id}")]
		public _Album Album(long id)
		{
			_Album _album = _flickrRepo.GetFeAlbumBy(id);
			return _album;
		}
	}
}
