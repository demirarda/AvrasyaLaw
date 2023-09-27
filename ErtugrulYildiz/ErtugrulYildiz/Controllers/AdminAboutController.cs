using ErtugrulYildiz.Models.DataContext;
using ErtugrulYildiz.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ErtugrulYildiz.Controllers
{
    public class AdminAboutController : Controller
    {
        ErtugrulYildizDbContext db = new ErtugrulYildizDbContext();
        // GET: AdminAbout
        public ActionResult Index()
        {
            return View(db.About.ToList());
        }


        // GET: AdminAbout/Edit/5
        public ActionResult Edit(int id)
        {
            var about=db.About.Where(x=>x.HakkimizdaId==id).SingleOrDefault();
            return View(about);
        }

        // POST: AdminAbout/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, About about, HttpPostedFileBase HakkimizdaFotograf)
        {
           if(ModelState.IsValid)
            {
                var a = db.About.Where(x => x.HakkimizdaId == id).SingleOrDefault();

                if(HakkimizdaFotograf != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(about.HakkimizdaFotograf)))
                    {
                        System.IO.File.Delete(Server.MapPath(about.HakkimizdaFotograf));
                    }

                    WebImage img = new WebImage(HakkimizdaFotograf.InputStream);
                    FileInfo imgInfo = new FileInfo(HakkimizdaFotograf.FileName);

                    string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(800, 800);
                    img.Save("~/Upload/Hakkimizda/"+logoName);

                    about.HakkimizdaFotograf = "/Upload/Hakkimizda/" + logoName;


				}
                a.HakkimizdaAciklama = about.HakkimizdaAciklama;
                a.HakkimizdaBaslik = about.HakkimizdaBaslik;
                a.HakkimizdaFotograf = about.HakkimizdaFotograf;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           return View();
        }

        
        
    }
}
