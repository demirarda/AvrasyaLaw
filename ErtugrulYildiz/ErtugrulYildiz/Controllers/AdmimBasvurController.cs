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
    public class AdmimBasvurController : Controller
    {
        private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();

        // GET: AdmimBasvur
        public ActionResult Index()
        {
            return View(db.Apply.ToList());
        }

        // GET: AdmimBasvur/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Apply.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // GET: AdmimBasvur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdmimBasvur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BasvurId,BasvurAciklama,BasvurFotograf")] Apply apply, HttpPostedFileBase BasvurFotograf)
        {
            if (ModelState.IsValid)
            {
				if (BasvurFotograf != null)
				{
					WebImage img = new WebImage(BasvurFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(BasvurFotograf.FileName);

					string basvurFotografName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Basvur/" + basvurFotografName);

					apply.BasvurFotograf = "/Upload/Basvur/" + basvurFotografName;



				}
				db.Apply.Add(apply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apply);
        }

        // GET: AdmimBasvur/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Apply.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // POST: AdmimBasvur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BasvurId,BasvurAciklama,BasvurFotograf")] Apply apply, HttpPostedFileBase BasvurFotograf, int id)
        {
            if (ModelState.IsValid)
            {
				var a = db.Apply.Where(x => x.BasvurId == id).SingleOrDefault();
				if (BasvurFotograf != null)
				{
					if (System.IO.File.Exists(Server.MapPath(a.BasvurFotograf)))
					{
						System.IO.File.Delete(Server.MapPath(a.BasvurFotograf));
					}
					WebImage img = new WebImage(BasvurFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(BasvurFotograf.FileName);

					string basvurFotografName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Basvur/" + basvurFotografName);

					a.BasvurFotograf = "/Upload/Basvur/" + basvurFotografName;
				}
				a.BasvurAciklama = apply.BasvurAciklama;
				
				
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apply);
        }

        // GET: AdmimBasvur/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Apply.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // POST: AdmimBasvur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apply apply = db.Apply.Find(id);
			if (System.IO.File.Exists(Server.MapPath(apply.BasvurFotograf)))
			{
				System.IO.File.Delete(Server.MapPath(apply.BasvurFotograf));
			}
			db.Apply.Remove(apply);
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
