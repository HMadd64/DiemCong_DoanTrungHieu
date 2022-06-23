using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiemCong_DoanTrungHieu.Models;

namespace DiemCong_DoanTrungHieu.Controllers
{
    public class HocPhanController : Controller
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public ActionResult Index()
        {
            var hp = from s in db.HocPhans select s;
            return View(hp);
        }
        public ActionResult Details(string id)
        {
            var hp = db.HocPhans.Where(m => m.MaHP == id).First();
            return View(hp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, HocPhan s)
        {
            var mahp = collection["MaHP"];
            var tenhp = collection["TenHP"];
            var tinchi = Convert.ToInt32(collection["SoTinChi"]);
            if (string.IsNullOrEmpty(tenhp))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaHP = mahp;
                s.TenHP = tenhp.ToString();
                s.SoTinChi = tinchi;
                db.HocPhans.InsertOnSubmit(s);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Delete(string id)
        {
            var hp = db.HocPhans.First(m => m.MaHP == id);
            return View(hp);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var hp = db.HocPhans.Where(m => m.MaHP == id).First();
            db.HocPhans.DeleteOnSubmit(hp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}