using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_Quanlybanxedap.Models;

namespace CNPM_Quanlybanxedap.Areas.Admin.Controllers
{
    public class HOADONsController : Controller
    {
        private qlibanxedapEntities db = new qlibanxedapEntities();

        // GET: Admin/HOADONs
        public ActionResult Index()
        {
            var hOADONs = db.HOADONs.Include(h => h.KHACHHANG).Include(h => h.NHANVIEN);
            return View(hOADONs.ToList());
        }

        // GET: Admin/HOADONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HOADON hOADON = db.HOADONs.Find(id);

            if (hOADON == null)
            {
                return HttpNotFound();
            }

            // Truy vấn danh sách chi tiết hóa đơn có MaHD tương ứng
            var chiTietHoaDon = db.CHITIETHOADONs.Include(ct => ct.XE).Where(ct => ct.MaHD == id).ToList();

            ViewBag.ChiTietHoaDon = chiTietHoaDon;

            return View(hOADON);
        }

    }
}
