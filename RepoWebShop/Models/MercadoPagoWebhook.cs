﻿using System;

namespace RepoWebShop.Models
{
	public class MercadoPagoWebhook
	{
		public int Id { get; set; }
		public bool LiveMode { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
		public string ApiVersion { get; set; }
		public string Action { get; set; }
		public Data Data { get; set; }
	}
	public class Data
	{
		public string Id { get; set; }
	}
}