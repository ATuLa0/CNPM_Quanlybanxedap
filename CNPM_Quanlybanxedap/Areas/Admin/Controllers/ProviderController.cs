using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_Quanlybanxedap;
using CNPM_Quanlybanxedap.Models;

namespace CNPM_Quanlybanxedap.Areas.Admin.Controllers
{
    public class ProviderController : Controller
    {
        qlibanxedapEntities db = new qlibanxedapEntities();
        // GET: Provider
        public ActionResult Index()
        {
            var ncc = db.NHACUNGCAPs.ToList();
            return View(ncc);
        }

        [HttpGet]
        public ActionResult CreateProvider()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProvider(NHACUNGCAP provider)
        {
            try
            {
                db.NHACUNGCAPs.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi thêm mới Nhà cung cấp");
            }
        }
        [HttpGet]
        public ActionResult EditProvider(int id)
        {
            var ncc = db.NHACUNGCAPs.Find(id);
            return View(ncc);
        }
        [HttpPost]
        public ActionResult EditProvider(int id, NHACUNGCAP ncc)
        {
            db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteProvider(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ncc = db.NHACUNGCAPs.Where(c => c.MaNCC == id).FirstOrDefault();
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        [HttpPost, ActionName("DeleteProvider")]
        public ActionResult DeleteProvider(int id)
        {
            try
            {
                var category = db.NHACUNGCAPs.Find(id);
                db.NHACUNGCAPs.Remove(category);
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