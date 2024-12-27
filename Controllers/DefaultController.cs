using R61M614_Mid06Evd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R61M614_Mid06Evd.Controllers
{
    public class DefaultController : Controller
    {
        private readonly dbmidExam06Entities db = new dbmidExam06Entities();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Category()
        {
            var model = db.Categories.Include("Products").ToList();
            return PartialView("_Category",model);
        }
        public ActionResult Create()
        {
            var model = new Category();
            model.Products.Add(new Product { });
           
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Category model, HttpPostedFileBase Picture, string operation = "")
        {

            if (operation == "add")
            {
                model.Products.Add(new Product { });
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (operation.StartsWith("del"))
            {
                int pos = operation.IndexOf("_");
                int index = int.Parse(operation.Substring(pos + 1));
                model.Products.RemoveAt(index);
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (operation == "insert")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                       
                        string ext = Path.GetExtension(Picture.FileName);
                        string f = model.Name + ext;
                        string savePath = Path.Combine(Server.MapPath("~/Pictures"), f);
                        Picture.SaveAs(savePath);
                        model.Picture = "~/Pictures/" + f;
                        
                        var newProducts = model.Products.Select(s => new Product { Name = s.Name, Price = s.Price }).ToList();
                        model.Products.AddRange(newProducts);
                        db.Categories.Add(model);
                        if (db.SaveChanges() > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch(Exception ex)
                    {
                         ModelState.AddModelError("",ex.Message);
                    }
                }
            }

            
            return PartialView("_CreateDetail", model);


        }
        public PartialViewResult CreateDetail(Category model)
        {
            model=model ?? new Category();
            model.Products.Add(new Product { });
           
            return PartialView("_CreateDetail", model);
        }
        public ActionResult Delete(int id)
        {
            var cat = db.Categories.FirstOrDefault(x => x.Id == id);
            if (cat == null) { return new HttpNotFoundResult(); }
            db.Products.RemoveRange(cat.Products.ToList());
            db.Categories.Remove(cat );
            db.SaveChanges();
            return RedirectToAction("Index");  
        }
    }
}