using CNPM_Quanlybanxedap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CNPM_Quanlybanxedap.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        qlibanxedapEntities db = new qlibanxedapEntities();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var xes = db.XEs.ToList();
            return View(xes);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(XE xe)
        {
            try
            {
                db.XEs.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi thêm mới Nhà cung cấp");
            }
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var xe = db.XEs.Find(id);
            return View(xe);
        }
        [HttpPost]
        public ActionResult EditProduct(int id, XE xe)
        {
            db.Entry(xe).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var xe = db.XEs.Where(c => c.MaXe == id).FirstOrDefault();
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }
        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var product = db.XEs.Find(id);
                db.XEs.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
    }
}