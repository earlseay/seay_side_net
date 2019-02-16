using SeaySideNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaySideNet.Controllers
{
	public class MyAppsController : Controller
	{
		// GET: MyApps
		public ActionResult Index()
		{
			var mal = new AppSet();
			mal.Apps.Add(new App("Home Network", null, "https://seayside.net:8443/manage/site/default/dashboard"));
			mal.Apps.Add(new App("Sonic Boom", null, "https://app.sbwell.com/home"));
			return View(mal);
		}
	}
}