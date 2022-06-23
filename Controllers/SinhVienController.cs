using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiemCong_DoanTrungHieu.Models;

namespace DiemCong_DoanTrungHieu.Controllers
{
    public class SinhVienController : Controller
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public ActionResult Index()
        {
            var sv = from s in db.SinhViens select s;
            return View(sv);
        }
        public ActionResult Details(string id)
        {
            var sv = db.SinhViens.Where(m => m.MaSV == id).First();
            return View(sv);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var masv = collection["MaSV"];
            var tensv = collection["HoTen"];
            var gioitinh = collection["GioiTinh"];
            var ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["Hinh"];
            if (string.IsNullOrEmpty(tensv))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = masv;
                s.HoTen = tensv.ToString();
                s.Hinh = E_hinh.ToString();
                s.GioiTinh = gioitinh;
                s.NgaySinh = ngaysinh;
                db.SinhViens.InsertOnSubmit(s);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Delete(string id)
        {
            var sv = db.SinhViens.First(m => m.MaSV == id);
            return View(sv);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var sv = db.SinhViens.Where(m => m.MaSV == id).First();
            db.SinhViens.DeleteOnSubmit(sv);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }

            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));

            return "/Content/images/" + file.FileName;
        }

    }
}