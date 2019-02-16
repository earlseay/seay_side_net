using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaySideNet.Models
{
	public class AppSet
	{
		public AppSet()
		{
			Apps = new List<App>();
		}

		public List<App> Apps { get; set; }
	}
}