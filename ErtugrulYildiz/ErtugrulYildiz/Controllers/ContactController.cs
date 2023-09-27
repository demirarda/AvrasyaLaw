using ErtugrulYildiz.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErtugrulYildiz.Controllers
{
    public class ContactController : Controller
    {
		private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();
		// GET: Contact
		public ActionResult Index()
        {
			return View(db.Contact.ToList().OrderByDescending(x => x.IletisimId));
		}
    }
}