using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL_DAL;
namespace WEB.Controllers
{
    public class NhanVienController : Controller
    {
        //
        // GET: /NhanVien/
        dbQuanLyNhaTroDataContext db = new dbQuanLyNhaTroDataContext();
        XuLy xl = new XuLy();
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {               
                return RedirectToAction("Home", "Dashboard");
            }
            var dt = xl.DsNhanvien();
            return View(dt);
            
        }

        public ActionResult DanhSachNhanVien()
        {
            if (Session["Username"] != null)
            {
                var dt = xl.DsNhanvien();
                return View(dt);
            }
            return RedirectToAction("Home", "Dashboard");

        }
        public ActionResult Delete(int id)
        {
            xl.XoaNhanVien(id);
            return RedirectToAction("DanhSachNhanVien", "NhanVien");

        }
        public ActionResult Edit(int id)
        {
            NHANVIEN nv = xl.ThongTinNhanVien(id);
            return View(nv);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            xl.SuaNhanVien(id, form["Ten"],form["GT"], form["DC"], form["SDT"], form["CMND"]);
            return RedirectToAction("DanhSachNhanVien", "NhanVien");
        }
        public ActionResult CreateNhanvien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNhanvien(FormCollection form)
        {
            string TENNV = form["Ten"];
            string NGAYSINH = form["Ngay"];
            string GIOITINH = form["GT"];
            string DIACHI = form["DC"];
            string HINH = form["HINH"];
            string SDT = form["SDT"];
            string CMND = form["CMND"];
            try
            {
                if (ModelState.IsValid)
                {
                    string str = xl.RemoveSign4VietnameseString(TENNV);
                    string Tk = str.Replace(" ", String.Empty).ToLower();
                    //Thêm nhân viên
                    xl.ThemNhanvien(TENNV, NGAYSINH, GIOITINH, DIACHI, HINH, SDT, CMND);
                    
                    //Thêm tài khoản
                     var manv = db.NHANVIENs.Select(t => t).OrderByDescending(e => e.MANV).FirstOrDefault().MANV;
                     xl.ThemTaiKhoan(Tk, SDT, 6, manv);
                    return RedirectToAction("DanhSachNhanVien");
                }

            }
            catch
            {
                
            }
            return View();
        }
    }
}
