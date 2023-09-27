using ErtugrulYildiz.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErtugrulYildiz.Controllers
{
    public class HomeController : Controller
    {
		private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Slider()
		{
			return View(db.Slider.ToList().OrderBy(x=>x.SliderId));
		}

		public ActionResult Info()
		{
			return View(db.Services.ToList().OrderBy(x=>x.HizmetId));
		}

		public ActionResult About()
		{
			return View(db.About.ToList().OrderByDescending(x => x.HakkimizdaId));
		}
	}
}