using ErtugrulYildiz.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErtugrulYildiz.Controllers
{
    public class LawyersController : Controller
    {
		private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();
		// GET: Lawyers
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult Team()
		{
			return View(db.Lawyers.ToList().OrderBy(x => x.AvukatId));
		}

		public ActionResult Feature()
		{
			return View(db.Choose.ToList().OrderBy(x => x.SecimId));
		}
	}
}