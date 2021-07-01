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
        public List<DoanhThu> DoanhThu(int test)
        {
            List<DoanhThu> lst = xl.LoadDanhThuThang(test);
            return lst;


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
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult DanhSachTaiKhoan()
        {
            var ds = xl.DsTaiKhoan();
            return View(ds);
        }
    }
}
