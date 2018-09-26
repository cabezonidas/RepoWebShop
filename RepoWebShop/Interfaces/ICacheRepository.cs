﻿using RepoWebShop.FeModels;
using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface ICacheRepository
    {
		IEnumerable<_Product> GetProducts();
		void SetProducts(IEnumerable<_Product> Products);

		Calendar GetPublicCalendar();
		void SetPublicCalendar(Calendar Calendar);
	}
}
