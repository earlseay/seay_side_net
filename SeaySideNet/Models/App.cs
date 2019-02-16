using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaySideNet.Models
{
	public class App
	{
		public App(string name, string imagePath, string url)
		{
			Name = name;
			ImagePath = imagePath;
			Url = url;
		}

		public string Name { get; set; }
		public string ImagePath { get; set; }
		public string Url { get; set; }
	}
}