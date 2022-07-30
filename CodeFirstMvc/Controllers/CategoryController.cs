using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstMvc.Models;
using PagedList;
using CodeFirstMvc.Data;

namespace CodeFirstMvc.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            List<Category> ListCategory = db.Categories.ToList();
            db.Dispose();
            return View(ListCategory);
           
        }
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Add(Category c)
        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            db.Categories.Add(c);
            db.SaveChanges();
            db.Dispose();

            return Redirect("Index");
        }
        public ActionResult Edit(int id)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Category e = db.Categories.Where(i => i.Id == id).FirstOrDefault();
            db.Dispose();
            return View(e);

        }

        public ActionResult Save(Category s)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Category e = db.Categories.Where(i => i.Id == s.Id).FirstOrDefault();
            e.CategoryName = s.CategoryName;
            db.SaveChanges();
            db.Dispose();

            return Redirect("Index");


        }

        public ActionResult Details(int id)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Category e = db.Categories.Where(i => i.Id == id).FirstOrDefault();

            db.Dispose();

            return View(e);


        }
        public ActionResult Delete(int id)

        {

            CodeFirstMvcContext db = new CodeFirstMvcContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category e = db.Categories.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);


        }
        public ActionResult Remove(int id)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Category e = db.Categories.Find(id);
            db.Categories.Remove(e);
            db.SaveChanges();


            return Redirect("Index");


        }
    }
}