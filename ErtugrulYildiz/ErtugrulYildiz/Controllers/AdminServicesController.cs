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
    public class AdminServicesController : Controller
    {
        private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();

        // GET: AdminServices
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: AdminServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // GET: AdminServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HizmetId,HizmetBaslik,HizmetAciklama,HizmetFotograf,CalismaBaslik,CalismaAciklama")] Services services, HttpPostedFileBase HizmetFotograf)
        {
            if (ModelState.IsValid)
            {
				if (HizmetFotograf != null)
				{
					WebImage img = new WebImage(HizmetFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(HizmetFotograf.FileName);

					string servicesImgName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Hizmet/" + servicesImgName);

					services.HizmetFotograf = "/Upload/Hizmet/" + servicesImgName;



				}
				db.Services.Add(services);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(services);
        }

        // GET: AdminServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: AdminServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HizmetId,HizmetBaslik,HizmetAciklama,HizmetFotograf,CalismaBaslik,CalismaAciklama")] Services services, HttpPostedFileBase HizmetFotograf, int id)
        {
            if (ModelState.IsValid)
            {
				var h = db.Services.Where(x => x.HizmetId == id).SingleOrDefault();
				if (HizmetFotograf != null)
				{
					if (System.IO.File.Exists(Server.MapPath(h.HizmetFotograf)))
					{
						System.IO.File.Delete(Server.MapPath(h.HizmetFotograf));
					}
					WebImage img = new WebImage(HizmetFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(HizmetFotograf.FileName);

					string servicesImgName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Hizmet/" + servicesImgName);

					h.HizmetFotograf = "/Upload/Hizmet/" + servicesImgName;
				}
				h.HizmetAciklama = services.HizmetAciklama;
				h.HizmetBaslik = services.HizmetBaslik;
				
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(services);
        }

        // GET: AdminServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: AdminServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services services = db.Services.Find(id);
			if (System.IO.File.Exists(Server.MapPath(services.HizmetFotograf)))
			{
				System.IO.File.Delete(Server.MapPath(services.HizmetFotograf));
			}
			db.Services.Remove(services);
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
