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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? page, int? pageSize)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


            //Ddefault size is 5 otherwise take pageSize value
            int defaSize = (pageSize ?? 5);

            ViewBag.psize = defaSize;

            //Dropdownlist code for PageSize selection
            //In View Attach this
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };
            //Create a instance of our DataContext  
            Product _dbContext = new Product();
            IPagedList<Product> productlst = null;

            productlst = db.Products.OrderBy(p => p.CategoryId).ToPagedList(pageIndex, defaSize);
            return View(productlst);



        }
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Add(Product c)
        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            db.Products.Add(c);
            db.SaveChanges();
            db.Dispose();

            return Redirect("Index");
        }
        public ActionResult Edit(int id)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Product e = db.Products.Where(i => i.ProductId == id).FirstOrDefault();
            db.Dispose();
            return View(e);

        }
        public ActionResult Save(Product p)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Product e = db.Products.Where(i => i.ProductId == p.ProductId).FirstOrDefault();
            e.ProductName = p.ProductName;
            e.CategoryId = p.CategoryId;
            db.SaveChanges();

            db.Dispose();
            return Redirect("Index");

        }
        public ActionResult Details(int id)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Product e = db.Products.Where(i => i.ProductId == id).FirstOrDefault();
            db.Dispose();
            return View(e);

        }
        public ActionResult Delete(int Id)

        {

            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Product p = db.Products.Where(x => x.ProductId ==Id).FirstOrDefault();
            return View(p);


        }
        public ActionResult Remove(Product p)

        {
            CodeFirstMvcContext db = new CodeFirstMvcContext();
            Product e = db.Products.Where(x => x.ProductId == p.ProductId).FirstOrDefault();
            db.Products.Remove(e);
            db.SaveChanges();
            db.Dispose();


            return Redirect("Index");


        }
    }
}