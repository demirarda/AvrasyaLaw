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
using System.Web.Services.Description;
using ErtugrulYildiz.Models.DataContext;
using ErtugrulYildiz.Models.Model;

namespace ErtugrulYildiz.Controllers
{
    public class AdminLawyersController : Controller
    {
        private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();

        // GET: AdminLawyers
        public ActionResult Index()
        {
            return View(db.Lawyers.ToList());
        }

        // GET: AdminLawyers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyers lawyers = db.Lawyers.Find(id);
            if (lawyers == null)
            {
                return HttpNotFound();
            }
            return View(lawyers);
        }

        // GET: AdminLawyers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminLawyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AvukatId,AvukatAdSoyad,AvukatUnvan,AvukatFotograf")] Lawyers lawyers, HttpPostedFileBase AvukatFotograf)
        {
            if (ModelState.IsValid)
            {
				if (AvukatFotograf != null)
				{
					WebImage img = new WebImage(AvukatFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(AvukatFotograf.FileName);

					string lawyersImgName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(400, 400);
					img.Save("~/Upload/Avukat/" + lawyersImgName);

					lawyers.AvukatFotograf = "/Upload/Avukat/" + lawyersImgName;



				}
				db.Lawyers.Add(lawyers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lawyers);
        }

        // GET: AdminLawyers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyers lawyers = db.Lawyers.Find(id);
            if (lawyers == null)
            {
                return HttpNotFound();
            }
            return View(lawyers);
        }

        // POST: AdminLawyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvukatId,AvukatAdSoyad,AvukatUnvan,AvukatFotograf")] Lawyers lawyers, HttpPostedFileBase AvukatFotograf, int id)
        {
            if (ModelState.IsValid)
            {
				var a = db.Lawyers.Where(x => x.AvukatId == id).SingleOrDefault();
				if (AvukatFotograf != null)
				{
					if (System.IO.File.Exists(Server.MapPath(a.AvukatFotograf)))
					{
						System.IO.File.Delete(Server.MapPath(a.AvukatFotograf));
					}
					WebImage img = new WebImage(AvukatFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(AvukatFotograf.FileName);

					string lawyersImgName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(400, 400);
					img.Save("~/Upload/Avukat/" + lawyersImgName);

					a.AvukatFotograf = "/Upload/Avukat/" + lawyersImgName;
				}
				a.AvukatAdSoyad = lawyers.AvukatAdSoyad;
				a.AvukatUnvan = lawyers.AvukatUnvan;
				
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lawyers);
        }

        // GET: AdminLawyers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyers lawyers = db.Lawyers.Find(id);
            if (lawyers == null)
            {
                return HttpNotFound();
            }
            return View(lawyers);
        }

        // POST: AdminLawyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lawyers lawyers = db.Lawyers.Find(id);
			if (System.IO.File.Exists(Server.MapPath(lawyers.AvukatFotograf)))
			{
				System.IO.File.Delete(Server.MapPath(lawyers.AvukatFotograf));
			}
			db.Lawyers.Remove(lawyers);
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
