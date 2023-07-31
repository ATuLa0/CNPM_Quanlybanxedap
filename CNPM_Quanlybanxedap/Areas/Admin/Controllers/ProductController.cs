using CNPM_Quanlybanxedap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
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
            var xes = db.XEs.Join(db.NHACUNGCAPs, x => x.MaNCC, n => n.MaNCC, (x, n) => new
            {
                MaXe = x.MaXe,
                TenXe = x.TenXe,
                MoTa = x.MoTa,
                GiaBan = x.GiaBan,
                XuatXu = x.XuatXu,
                MauSac = x.MauSac,
                CDBaoHanh = x.CDBaoHanh,
                TenNCC = n.TenNCC,
                HinhAnh = x.HinhAnh
            }).ToList().Select(xe => new XE
            {
                MaXe = xe.MaXe,
                TenXe = xe.TenXe,
                MoTa = xe.MoTa,
                GiaBan = xe.GiaBan,
                XuatXu = xe.XuatXu,
                MauSac = xe.MauSac,
                CDBaoHanh = xe.CDBaoHanh,
                TenNCC = xe.TenNCC,
                HinhAnh = xe.HinhAnh
            }).ToList();
            return View(xes);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var ncc = db.NHACUNGCAPs.ToList();
            ViewBag.nccList = new SelectList(ncc, "MaNCC", "TenNCC");
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
            var ncc = db.NHACUNGCAPs.ToList();
            ViewBag.nccList = new SelectList(ncc, "MaNCC", "TenNCC");
            var xe = db.XEs.Find(id);
            return View(xe);
        }
        [HttpPost]
        public ActionResult EditProduct(int id, XE updatedProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra xem sản phẩm có tồn tại trong cơ sở dữ liệu hay không
                    var existingProduct = db.XEs.Find(id);
                    if (existingProduct != null)
                    {
                        // Cập nhật thông tin sản phẩm (trừ MaXe)
                        existingProduct.MaNCC = updatedProduct.MaNCC;
                        existingProduct.TenNCC = updatedProduct.TenNCC;
                        existingProduct.CDBaoHanh = updatedProduct.CDBaoHanh;
                        existingProduct.MauSac = updatedProduct.MauSac;
                        existingProduct.MoTa = updatedProduct.MoTa;
                        existingProduct.XuatXu = updatedProduct.XuatXu;
                        existingProduct.HinhAnh = updatedProduct.HinhAnh;
                        existingProduct.GiaBan = updatedProduct.GiaBan;
                        existingProduct.TenXe = updatedProduct.TenXe;

                        db.SaveChanges();
                    }
                    else
                    {
                        return HttpNotFound();
                    }

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Xử lý khi có lỗi đồng thời cập nhật dữ liệu
                    // Ví dụ: hiển thị thông báo lỗi hoặc tái tải dữ liệu từ cơ sở dữ liệu
                    ModelState.AddModelError("", "Lỗi đồng thời cập nhật dữ liệu. Vui lòng thử lại.");
                    return View(updatedProduct);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khác
                    return Content("Lỗi: " + ex.Message);
                }
            }

            return View(updatedProduct);
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
        public ActionResult DetailProduct(int id)
        {
            var xe = db.XEs.Where(c => c.MaXe == id).FirstOrDefault();
            return View(xe);
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DatHang(KHACHHANG cus)
        {
            try
            {
                var existingCustomer = db.KHACHHANGs.FirstOrDefault(c => c.SoDienThoai == cus.SoDienThoai);
                if (existingCustomer == null)
                {
                    db.KHACHHANGs.Add(cus);
                    db.SaveChanges();

                    int newMaKH = db.KHACHHANGs.Max(c => c.MaKH);

                    // Lưu MaKH vào Session
                    Session["MaKH"] = newMaKH;
                }
                else
                {
                    // Lưu MaKH vào Session nếu khách hàng đã tồn tại
                    Session["MaKH"] = existingCustomer.MaKH;
                }

                return RedirectToAction("ChonXe");
            }
            catch
            {
                return Content("Lỗi !!!");
            }
        }

        public ActionResult ChonXe()
        {
            var ncc = db.NHACUNGCAPs.ToList();
            ViewBag.nccList = new SelectList(ncc, "MaNCC", "TenNCC");
            var xes = db.XEs.ToList();
            return View(xes);
        }
        [HttpPost]
        public ActionResult ChonXe(Dictionary<int, int> XeQuantity, Dictionary<int, int> MaXE, Dictionary<int, double> GiaXE)
        {
            try
            {

                // Lấy thông tin MaKH từ Session (nếu đã lưu trước đó)
                int maKH = Convert.ToInt32(Session["MaKH"]);

                // Lấy ngày hiện tại làm NgayTaoHD
                DateTime ngayTaoHD = DateTime.Now;

                // Tạo mới đối tượng HOADON và cập nhật thông tin
                HOADON hoadon = new HOADON
                {
                    MaKH = maKH,
                    NgayTaoHD = ngayTaoHD,
                };
                db.HOADONs.Add(hoadon);
                db.SaveChanges();
                int newhd = db.HOADONs.Max(c => c.MaHD);
                double tongTien = 0;
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("MaXE["))
                    {
                        int maXe = Convert.ToInt32(Request.Form[key]);
                        int soLuong = Convert.ToInt32(Request.Form["SoLuong[" + maXe + "]"]);
                        double giaXe = Convert.ToDouble(Request.Form["GiaXE[" + maXe + "]"]);
                        tongTien += soLuong * giaXe;
                        // Xử lý dữ liệu ở đây, ví dụ: lưu vào database
                        // Ví dụ:
                        if(soLuong > 0)
                        {
                            CHITIETHOADON chiTietHoaDon = new CHITIETHOADON
                            {
                                MaHD = newhd,
                                MaXE = maXe,
                                SoLuong = soLuong
                            };
                            db.CHITIETHOADONs.Add(chiTietHoaDon);
                        }
                    }
                }
                if(tongTien > 0)
                {
                    hoadon.TongTien = tongTien;
                    db.SaveChanges();
                }
                else
                {
                    db.HOADONs.Remove(hoadon);
                    db.SaveChanges();
                    return Content("Số lượng bạn nhập không hợp lệ !!!");
                }

                return RedirectToAction("TenActionKhac");
            }
            catch
            {
                return Content("Lỗi !!!");
            }
        }
        [HttpGet]
        public ActionResult DanhSachXeBanChay()
        {
            // Lấy danh sách 5 xe bán được nhiều nhất từ bảng CHITIETHOADON
            var top5XeBanChay = db.CHITIETHOADONs
                .GroupBy(cthd => cthd.MaXE)
                .Select(group => new
                {
                    MaXe = group.Key,
                    SoLuongBan = group.Sum(cthd => cthd.SoLuong)
                })
                .OrderByDescending(x => x.SoLuongBan)
                .Take(5)
                .ToList();

            // Lấy danh sách các xe nằm ngoài top 5 từ bảng CHITIETHOADON
            var top5XeBanChayIds = top5XeBanChay.Select(x => x.MaXe).ToList();
            var danhSachXeNgoaiTop5 = db.CHITIETHOADONs
                .Where(chiTiet => !top5XeBanChayIds.Contains(chiTiet.MaXE))
                .Select(chiTiet => chiTiet.XE)
                .ToList();

            var tongSoLuongXeBan = new Dictionary<int, int>();
            foreach (var xeBanChay in top5XeBanChay)
            {
                int maXe = xeBanChay.MaXe;
                var tongSoLuongBanXe = db.CHITIETHOADONs
                    .Where(chiTiet => chiTiet.MaXE == maXe)
                    .Sum(chiTiet => chiTiet.SoLuong) ?? 0; // Use the null-coalescing operator to provide a default value of 0
                tongSoLuongXeBan.Add(maXe, tongSoLuongBanXe);
            }
            // Calculate the total quantity for each item in DanhSachXeNgoaiTop5
            var soLuongBanForEachXeNgoaiTop5 = new Dictionary<int, int>();
            foreach (var xe in danhSachXeNgoaiTop5)
            {
                int maXe = xe.MaXe;
                var tongSoLuongBanXe = db.CHITIETHOADONs
                    .Where(chiTiet => chiTiet.MaXE == maXe)
                    .Sum(chiTiet => chiTiet.SoLuong) ?? 0; // Use the null-coalescing operator to provide a default value of 0
                soLuongBanForEachXeNgoaiTop5.Add(maXe, tongSoLuongBanXe);
            }

            // Convert the anonymous type list to a list of XE objects
            var top5XeBanChayDetails = db.XEs
                .Where(x => top5XeBanChayIds.Contains(x.MaXe))
                .ToList();

            var tongslcthd = db.CHITIETHOADONs.Sum(c => c.SoLuong);

            // Gửi dữ liệu cho View thông qua Model hoặc ViewBag
            var viewModel = new XeViewModel
            {
                Top5XeBanChay = top5XeBanChayDetails,
                DanhSachXeNgoaiTop5 = danhSachXeNgoaiTop5,
                SoLuongBanForEachXe = tongSoLuongXeBan,
                SoLuongBanForEachXeNgoaiTop5 = soLuongBanForEachXeNgoaiTop5,
                TongSoLuongBan = (int)tongslcthd
            };
            
            return View(viewModel);
        }

    }
}