using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL_DAL;
namespace WEB.Controllers
{
    public class DashboardController : Controller
    {
        dbQuanLyNhaTroDataContext db = new dbQuanLyNhaTroDataContext();
        //
        // GET: /Dashboard/
        XuLy xl = new XuLy();
        public ActionResult Home()
        {
            if (Session["Username"] != null)
            {
                var dt = xl.DsNhanvien();
                return View(dt);
            }
            return RedirectToAction("Login", "Dashboard"); ;
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            
            string tk = Request.Form["tk"];
            string mk = Request.Form["mk"];
            if (xl.KiemTraAdmin(tk, mk))
            {
                Session["Username"] = tk;
                return RedirectToAction("Home", "Dashboard");
            }
            return View();
        }
        public JsonResult DsDoanhThuTungThang(string nam)
        {
            int Rs = int.Parse(nam);
            try
            {
                var data = (from t in db.PHIEUTHUTIENs
                            where t.NGAYBATDAU.Value.Year == Rs
                            group t by new { month = t.NGAYBATDAU.Value.Month } into d
                            select new { Thang = d.Key.month, DOANTHU = d.Sum(t => t.TONGTIEN) }).ToList();
                return Json(new { code = 200, ds = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DsDoanhThuTungNam()
        {
            try
            {
                var data = (from t in db.PHIEUTHUTIENs
                            group t by new { month = t.NGAYBATDAU.Value.Year } into d
                            select new { Thang = d.Key.month, DOANTHU = d.Sum(t => t.TONGTIEN) }).ToList();
                return Json(new { code = 200, ds = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult TongCongKhachThue()
        {
            try
            {
                var data = (from t in db.KHACHTROs
                            select new { Tongcong = t.MAKT}).Count();
                return Json(new { code = 200, dskt = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult TongCongKhachDangThue()
        {
            try
            {
                var data = (from t in db.KHACHTROs
                       from i in db.PHIEUTHUEPHONGs
                       from q in db.PHONGTROs
                       where t.MAKT == i.MAKT && i.MAPT == q.MAPT && i.TRANGTHAI == "Đang thuê"
                       select new {TENKT=t.HOTEN,TP=q.TENPHONG,NGAYTHUE=i.NGAYTHUE}).Count();
                return Json(new {code = 200,TC = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SoPhongTrongDangThue()
        {
            try
            {
                var data = (from t in db.PHONGTROs
                            where t.TRANGTHAI == "Đã cho thuê" && t.TRANGTHAIAN == true
                            select t).Count();
                return Json(new { code = 200, TC = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SoPhongTrong()
        {
            try
            {
                var data = (from t in db.PHONGTROs
                            where t.TRANGTHAI == "Chưa cho thuê" && t.TRANGTHAIAN == true
                            select t).Count();
                return Json(new { code = 200, TC = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Logout()
        {
            if (Session["Username"] != null)
            {
                Session.Clear();         
            }
            return RedirectToAction("Home", "Dashboard");
        }
        public ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Home", "Dashboard");
            }
            return View();
        }
        public ActionResult PhanQuyen()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            return RedirectToAction("Home", "Dashboard");
        }
        public JsonResult LoadPhanQuyen()
        {
            try
            {
                var data = (from t in db.PHANQUYENs
                            from q in db.TAIKHOANs
                            from c in db.NHANVIENs
                            where t.MAPQ == q.MAPQ && q.MANV == c.MANV
                            select new { MANV = q.MANV, TENNV = c.TENNV, USERNAME = q.USERNAME, PASSWORD = q.PASSWORD, QUYEN = t.MAPQ }).ToList();
                return Json(new { code = 200, pq = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DoiQuyen(int manv,int mapq)
        {
            try
            {
                var TK = db.TAIKHOANs.SingleOrDefault(t => t.MANV == manv);
                TK.MAPQ = mapq;
                db.SubmitChanges();
                return Json(new { code = 200, msg = "Thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                 return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LoadTenQuyen()
        {
            try
            {
                var data = (from t in db.PHANQUYENs select new {ID=t.MAPQ,TEN=t.TENQUYEN }).ToList();
                return Json(new { code = 200, dsq = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
