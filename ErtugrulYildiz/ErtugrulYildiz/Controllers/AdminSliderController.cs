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
    public class AdminSliderController : Controller
    {
        private ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();

        // GET: AdminSlider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: AdminSlider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: AdminSlider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminSlider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SliderId,SliderBaslik,SliderAciklama,SliderFotograf")] Slider slider , HttpPostedFileBase SliderFotograf)
        {
            if (ModelState.IsValid)
            {
                if (SliderFotograf != null)
                {
                    WebImage img = new WebImage(SliderFotograf.InputStream);
                    FileInfo imgInfo = new FileInfo(SliderFotograf.FileName);

                    string sliderImgName = Guid.NewGuid().ToString()+imgInfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Upload/Slider/" + sliderImgName);

                    slider.SliderFotograf = "/Upload/Slider/" + sliderImgName; 



				}
                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: AdminSlider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: AdminSlider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SliderId,SliderBaslik,SliderAciklama,SliderFotograf")] Slider slider, HttpPostedFileBase SliderFotograf, int id)
        {
            if (ModelState.IsValid)
            {
                var s = db.Slider.Where(x => x.SliderId== id).SingleOrDefault();
                if(SliderFotograf != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.SliderFotograf)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.SliderFotograf));
                    }
					WebImage img = new WebImage(SliderFotograf.InputStream);
					FileInfo imgInfo = new FileInfo(SliderFotograf.FileName);

					string sliderImgName = Guid.NewGuid().ToString() + imgInfo.Extension;
					img.Resize(600, 400);
					img.Save("~/Upload/Slider/" + sliderImgName);

					s.SliderFotograf = "/Upload/Slider/" + sliderImgName;
				}
                s.SliderBaslik = slider.SliderBaslik;
                s.SliderAciklama = slider.SliderAciklama;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: AdminSlider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: AdminSlider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            if (System.IO.File.Exists(Server.MapPath(slider.SliderFotograf)))
            {
                System.IO.File.Delete(Server.MapPath(slider.SliderFotograf));
            }
            db.Slider.Remove(slider);
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
