using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        public ActionResult EditProvider(int id, NHACUNGCAP updatedProvider)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra xem nhà cung cấp có tồn tại trong cơ sở dữ liệu hay không
                    var existingProvider = db.NHACUNGCAPs.Find(id);
                    if (existingProvider != null)
                    {
                        // Cập nhật thông tin nhà cung cấp (trừ MaNCC)
                        existingProvider.TenNCC = updatedProvider.TenNCC;
                        existingProvider.DiaChi = updatedProvider.DiaChi;
                        existingProvider.SoDienThoai = updatedProvider.SoDienThoai;
                        existingProvider.Email = updatedProvider.Email;
                        existingProvider.ThongTinMoTa = updatedProvider.ThongTinMoTa;

                        // Cập nhật các thuộc tính khác tùy theo yêu cầu

                        db.SaveChanges();
                    }
                    else
                    {
                        // Nhà cung cấp không tồn tại trong cơ sở dữ liệu
                        return HttpNotFound();
                    }

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Xử lý khi có lỗi đồng thời cập nhật dữ liệu
                    // Ví dụ: hiển thị thông báo lỗi hoặc tái tải dữ liệu từ cơ sở dữ liệu
                    ModelState.AddModelError("", "Lỗi đồng thời cập nhật dữ liệu. Vui lòng thử lại.");
                    return View(updatedProvider);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khác
                    return Content("Lỗi: " + ex.Message);
                }
            }

            return View(updatedProvider);
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