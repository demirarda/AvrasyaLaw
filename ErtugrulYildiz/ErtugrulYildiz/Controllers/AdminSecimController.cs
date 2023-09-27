using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ErtugrulYildiz.Models.DataContext;
using ErtugrulYildiz.Models.Model;

namespace ErtugrulYildiz.Controllers
{
    public class AdminSecimController : Controller
    {
        private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();

        // GET: AdminSecim
        public ActionResult Index()
        {
            return View(db.Choose.ToList());
        }

        // GET: AdminSecim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choose choose = db.Choose.Find(id);
            if (choose == null)
            {
                return HttpNotFound();
            }
            return View(choose);
        }

        // GET: AdminSecim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminSecim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecimId,SecimBaslik,SecimAciklama,SecimFotograf")] Choose choose , HttpPostedFileBase SecimFotograf)
        {
            if (ModelState.IsValid)
            {
				if (SecimFotograf != null)
				{
					WebImage img = new WebImage(SecimFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(SecimFotograf.FileName);

					string secimFotografName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Secim/" + secimFotografName);

					choose.SecimFotograf = "/Upload/Secim/" + secimFotografName;



				}
				db.Choose.Add(choose);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choose);
        }

        // GET: AdminSecim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choose choose = db.Choose.Find(id);
            if (choose == null)
            {
                return HttpNotFound();
            }
            return View(choose);
        }

        // POST: AdminSecim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecimId,SecimBaslik,SecimAciklama,SecimFotograf")] Choose choose, HttpPostedFileBase SecimFotograf, int id)
        {
            if (ModelState.IsValid)
            {
				var a = db.Choose.Where(x => x.SecimId == id).SingleOrDefault();
				if (SecimFotograf != null)
				{
					if (System.IO.File.Exists(Server.MapPath(a.SecimFotograf)))
					{
						System.IO.File.Delete(Server.MapPath(a.SecimFotograf));
					}
					WebImage img = new WebImage(SecimFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(SecimFotograf.FileName);

					string secimFotografName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Secim/" + secimFotografName);

					a.SecimFotograf = "/Upload/Secim/" + secimFotografName;
				}
				a.SecimBaslik = choose.SecimBaslik;
				a.SecimAciklama = choose.SecimAciklama;
				
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(choose);
        }

        // GET: AdminSecim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choose choose = db.Choose.Find(id);
            if (choose == null)
            {
                return HttpNotFound();
            }
            return View(choose);
        }

        // POST: AdminSecim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choose choose = db.Choose.Find(id);
			if (System.IO.File.Exists(Server.MapPath(choose.SecimFotograf)))
			{
				System.IO.File.Delete(Server.MapPath(choose.SecimFotograf));
			}
			db.Choose.Remove(choose);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
