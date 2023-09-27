using ErtugrulYildiz.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErtugrulYildiz.Controllers
{
    public class AboutController : Controller
    {
		private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();
		// GET: About
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult About()
		{
			return View(db.About.ToList().OrderByDescending(x => x.HakkimizdaId));
		}

        public ActionResult Feature() 
        {
            return View(db.Choose.ToList().OrderBy(x=> x.SecimId));
        }
	}
}