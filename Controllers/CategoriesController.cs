using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using R61M614_Mid06Evd.Models;

namespace R61M614_Mid06Evd.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly dbmidExam06Entities db=new dbmidExam06Entities();
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.OrderBy(c=>c.Name).ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category entity, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid) 
            {
                if (picture != null) { 
                        string ext= Path.GetExtension(picture.FileName);
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        string filetoSave = Path.Combine(Server.MapPath("~/"), "Pictures",
                            entity.Name + ext);
                        picture.SaveAs(filetoSave);
                        entity.Picture = "~/Pictures/" + entity.Name + ext;
                        db.Categories.Add(entity);
                        if (db.SaveChanges() > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else {
                        ModelState.AddModelError("", "Please provide Valid picture");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please provide Valid picture");
                }

            }
            return View(entity);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Categories.Find(id);
            if(model==null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category entity, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    string ext = Path.GetExtension(picture.FileName);
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        string filetoSave = Path.Combine(Server.MapPath("~/"), "Pictures", entity.Name + ext);
                        picture.SaveAs(filetoSave);
                        entity.Picture = "~/Pictures/" + entity.Name + ext;
                       
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please provide Valid picture");
                    }
                }
                else
                {
                    entity.Picture = entity.Picture;
                }
               db.Entry(entity).State=System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }

            }

            return View(entity);
        }
        public ActionResult Delete(int?id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = db.Categories.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    db.Categories.Remove(model);
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.err = "Save failed";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex) {
                ViewBag.err = ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}