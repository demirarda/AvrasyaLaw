using ErtugrulYildiz.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErtugrulYildiz.Controllers
{
    public class PracticeController : Controller
    {
		private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();
		// GET: Practice
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult Services()
		{
			return View(db.Services.ToList().OrderBy(x => x.HizmetId));
		}
		public ActionResult Action()
		{
			return View(db.Apply.ToList().OrderByDescending(x => x.BasvurId));
		}

		
	}
}