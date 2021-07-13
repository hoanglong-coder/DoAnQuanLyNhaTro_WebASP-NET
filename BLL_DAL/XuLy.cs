using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
namespace BLL_DAL
{
    public class XuLy
    {
        dbQuanLyNhaTroDataContext db = new dbQuanLyNhaTroDataContext();
        public XuLy() {}  
        
        private readonly string[] VietnameseSigns = new string[]{"aAeEoOuUiIdDyY","áàạảãâấầậẩẫăắằặẳẵ","ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ","éèẹẻẽêếềệểễ","ÉÈẸẺẼÊẾỀỆỂỄ","óòọỏõôốồộổỗơớờợởỡ","ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ","úùụủũưứừựửữ","ÚÙỤỦŨƯỨỪỰỬỮ","íìịỉĩ","ÍÌỊỈĨ","đ","Đ","ýỳỵỷỹ","ÝỲỴỶỸ"};
        //Đăng nhập
        public string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
        public bool DangNhap(string tk,string mk)
        {
            byte[] pass = ASCIIEncoding.ASCII.GetBytes(mk);
            byte[] hasdata = new MD5CryptoServiceProvider().ComputeHash(pass);

            string hasspass = "";
            foreach (byte item in hasdata)
            {
                hasspass += item;
            }

            try
            {
                var dn = db.TAIKHOANs.Count(t => t.USERNAME == tk && t.PASSWORD == hasspass);
                if (dn == 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("loo");
            }
            return true;
        }
        public int GetIDNhanVien(string username)
        {
            var id =  db.TAIKHOANs.Single(t => t.USERNAME == username).MANV;
            return int.Parse(id.ToString());
        }
        public NHANVIEN TaiKhoanNhanVien(int id)
        {
            NHANVIEN nv = db.NHANVIENs.Single(t => t.MANV == id);
            return nv;
        }
        public void SuaThongTinNhanVien(int manv,DateTime ngaysinh,string gt, string diachi,string hinh)
        {
            var nhanvien = db.NHANVIENs.Single(t => t.MANV == manv);
            nhanvien.NGAYSINH = ngaysinh;
            nhanvien.GIOITINH = gt;
            nhanvien.DIACHI = diachi;
            nhanvien.HINH = hinh;
            db.SubmitChanges();
        }
        public void DoiMatKhau(int idnhanvien, string mkcu, string mkmoi)
        {
            byte[] pass = ASCIIEncoding.ASCII.GetBytes(mkcu);
            byte[] hasdata = new MD5CryptoServiceProvider().ComputeHash(pass);
            string hasspass = "";
            foreach (byte item in hasdata)
            {
                hasspass += item;
            }

            byte[] pass1 = ASCIIEncoding.ASCII.GetBytes(mkmoi);
            byte[] hasdata1 = new MD5CryptoServiceProvider().ComputeHash(pass1);
            string hasspass5 = "";
            foreach (byte item in hasdata1)
            {
                hasspass5 += item;
            }


            var nv = db.TAIKHOANs.SingleOrDefault(t => t.MANV == idnhanvien && t.PASSWORD == hasspass);
            if (nv == null)
            {
                MessageBox.Show("Sai mật khẩu củ");
                return;
            }
            nv.PASSWORD = hasspass5;
            db.SubmitChanges();
            MessageBox.Show("Thay đổi thành công");
        }
        public string GetChucVu(int manv)
        {
            var data = (from t in db.TAIKHOANs
                        from q in db.PHANQUYENs
                        where t.MAPQ == q.MAPQ
                        select t).Single(t => t.MANV == manv).PHANQUYEN.TENQUYEN;

            return data;
        }
        public List<TAIKHOAN> DsTaiKhoan()
        {
            return db.TAIKHOANs.Select(t => t).ToList();
        }
        public List<NHANVIEN> DsNhanvien()
        {
            return db.NHANVIENs.Select(t => t).ToList();
        }
        public void ThemTaiKhoan(string Username,string Password, int Mapq , int manv)
        {
            byte[] pass = ASCIIEncoding.ASCII.GetBytes(Password);
            byte[] hasdata = new MD5CryptoServiceProvider().ComputeHash(pass);
            string hasspass = "";
            foreach (byte item in hasdata)
            {
                hasspass += item;
            }
            TAIKHOAN tk = new TAIKHOAN();
            tk.USERNAME = Username;
            tk.PASSWORD = hasspass;
            tk.MAPQ = Mapq;
            tk.MANV = manv;
            db.TAIKHOANs.InsertOnSubmit(tk);
            db.SubmitChanges();
        }
        public void ThemNhanvien(string ten, string ngay, string gioitinh, string diachi, string hinh, string sdt, string cmnd)
        {
            NHANVIEN nv = new NHANVIEN();
            nv.TENNV = ten;
            nv.NGAYSINH = DateTime.Parse(ngay.ToString());
            nv.GIOITINH = gioitinh;
            nv.DIACHI = diachi;
            nv.HINH = hinh;
            nv.SDT = sdt;
            nv.CMND = cmnd;
            db.NHANVIENs.InsertOnSubmit(nv);
            db.SubmitChanges();
        }
        public void XoaNhanVien(int id)
        {
            var nv = db.NHANVIENs.SingleOrDefault(t => t.MANV == id);
            db.NHANVIENs.DeleteOnSubmit(nv);
            db.SubmitChanges();
        }
        public void SuaNhanVien(int id,string ten,string gt,string diachi,string sdt,string cmnd)
        {
            var nv = db.NHANVIENs.SingleOrDefault(t => t.MANV == id);
            nv.TENNV = ten;
            nv.GIOITINH = gt;
            nv.DIACHI = diachi;
            nv.SDT = sdt;
            nv.CMND = cmnd;
            db.SubmitChanges();
        }
        public NHANVIEN ThongTinNhanVien(int nv)
        {
            return db.NHANVIENs.SingleOrDefault(t => t.MANV == nv);
        }
        //Kiểm Tra quyền
        public bool KiemTraThue_ThanhToan(int manv)
        {
            var kt = db.TAIKHOANs.SingleOrDefault(t => t.MANV == manv && t.MAPQ == 1);
            if (kt == null)
            {
                return false;
            }
            return true;
        }
        public bool KiemTrachua(int manv)
        {
            var kt = db.TAIKHOANs.SingleOrDefault(t => t.MANV == manv && t.MAPQ == 6);
            if (kt == null)
            {
                return false;
            }
            return true;
        }
        public bool KiemTraQuanLyPhong(int manv)
        {
            var kt = db.TAIKHOANs.SingleOrDefault(t => t.MANV == manv && t.MAPQ == 2);
            if (kt == null)
            {
                return false;
            }
            return true;
        }
        public bool KiemTraQuanLyKhachHang(int manv)
        {
            var kt = db.TAIKHOANs.SingleOrDefault(t => t.MANV == manv && t.MAPQ == 3);
            if (kt == null)
            {
                return false;
            }
            return true;
        }
        public bool KiemTraToanQuyen(int manv)
        {
            var kt = db.TAIKHOANs.SingleOrDefault(t => t.MANV == manv && t.MAPQ == 5);
            if (kt == null)
            {
                return false;
            }
            return true;
        }
        public bool KiemTraAdmin(string tk,string mk)
        {
            var kt = db.TAIKHOANs.SingleOrDefault(t => t.USERNAME == tk && t.PASSWORD == mk);
            if (kt == null)
            {
                return false;
            }
            return true;
        }
        //Loại phòng
        public IQueryable LoadLP()
        {
            return db.LOAIPHONGs.Select(t => t);
        }
        public void ThemLP(string TenLp, int Dongia,int dientich)
        {
            LOAIPHONG loaiphong = new LOAIPHONG();
            loaiphong.TENLOAIPHONG = TenLp;
            loaiphong.DONGIA = Dongia;
            loaiphong.DIENTICH = dientich;
            db.LOAIPHONGs.InsertOnSubmit(loaiphong);
            db.SubmitChanges();
        }
        public void XoaLP(int Malp)
        {
            var loaiphong = db.LOAIPHONGs.Where(t => t.MALP == Malp);
            db.LOAIPHONGs.DeleteAllOnSubmit(loaiphong);
            db.SubmitChanges();
        }
        public void SuaLP(int Malp, string TenLp, int Dongia, int dientich)
        {
            LOAIPHONG loaiphong = db.LOAIPHONGs.Single(t => t.MALP == Malp);
            loaiphong.TENLOAIPHONG = TenLp;
            loaiphong.DONGIA = Dongia;
            loaiphong.DIENTICH = dientich;
            db.SubmitChanges();
        }
        //Áp dụng KNN tư vấn giá phòng
        public double KhoanCach(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
        }
        public int Count(List<Result> list, float x)
        {
            int dem = 0;
            for (int i = 0; i < list.Count - 1; i++)
                if (list[i].Gia == x)
                    dem++;
            return dem;
        }
        public float TuvanGia(List<Result> list)
        {
            int dem1, dem = Count(list, list[0].Gia), index = 0;
            for (int i = 1; i < list.Count - 1; i++)
            {
                dem1 = Count(list, list[i].Gia);
                if (dem < dem1)
                {
                    dem = dem1;
                    index = i;
                }
            }
            return list[index].Gia;
        }
        public List<Result> TimKDuLieuGanNhat(Product pd, int k)
        {
            var list = db.SOLIEUTHONGKEs.Select(s => new { Location = s.Location, Dientich1 = s.DienTich, Gia = s.Gia }).ToList();
            List<Result> listrs = new List<Result>();
            for (int i = 0; i < list.Count; i++)
            {
                Result rs = new Result();
                rs.Distance = KhoanCach((double)list[i].Location, (double)list[i].Dientich1, pd.Location, pd.Dientich1);
                rs.Gia = (float)list[i].Gia;
                listrs.Add(rs);
            }
            listrs.Sort();
            List<Result> kq = listrs.Take(k).ToList();

            return kq;
        }
        //Thiết bị - Loại thiết bị
        public IQueryable LoadLoaiTB()
        {
            return db.LOAITHIETBIs.Select(t => t);
        }
        public void ThemLoaiTB(string tenloai)
        {
            LOAITHIETBI ltb = new LOAITHIETBI();
            ltb.TENLOAI = tenloai;
            db.LOAITHIETBIs.InsertOnSubmit(ltb);
            db.SubmitChanges();
        }
        public void XoaLoaiTB(int Maloai)
        {
            var loai = db.LOAITHIETBIs.Where(t => t.MALOAI == Maloai);
            db.LOAITHIETBIs.DeleteAllOnSubmit(loai);
            db.SubmitChanges();
        }
        public void SuaLoaiTB(int Maloai,string ten)
        {
            var loai = db.LOAITHIETBIs.Single(t => t.MALOAI == Maloai);
            loai.TENLOAI = ten;
            db.SubmitChanges();
        }
        public IQueryable LoadTB(int maloai)
        {
            var loai = from t in db.LOAITHIETBIs
                       from q in db.THIETBIs
                       where t.MALOAI == q.MALOAI && q.MALOAI == maloai
                       select new { MATB=q.MATB, TENTB = q.TENTB, GIA =q.GIA,MALOAI=q.MALOAI};
            return loai;
        }
        public void ThemTB(string tentb, int dongia, int maloai)
        {
            THIETBI tb = new THIETBI();
            tb.TENTB = tentb;
            tb.GIA = dongia;
            tb.MALOAI = maloai;
            db.THIETBIs.InsertOnSubmit(tb);
            db.SubmitChanges();
        }
        public void XoaTB(int mathietbi)
        {
            var thietbi = db.THIETBIs.Where(t => t.MATB == mathietbi);
            db.THIETBIs.DeleteAllOnSubmit(thietbi);
            db.SubmitChanges();
        }
        public void SuaTB(int mathietbi, string tenthietbi, int gia)
        {
            THIETBI thietbi = db.THIETBIs.Single(t => t.MATB == mathietbi);
            thietbi.TENTB = tenthietbi;
            thietbi.GIA = gia;
            db.SubmitChanges();
        }
        //Phòng
        public IQueryable LoadPT(int Maloaiphong)
        {
            var pt = from t in db.PHONGTROs
                     from q in db.LOAIPHONGs
                     where t.MALP == q.MALP && t.MALP == Maloaiphong &&t.TRANGTHAIAN==true
                     select new {MAPT = t.MAPT,MALP=t.MALP,TENPHONG=t.TENPHONG,SLTOIDA=t.SLTOIDA,TRANGTHAI=t.TRANGTHAI,SONGUOIHIENTAI=t.SONGUOIHIENTAI};
            return pt;
        }
        public IQueryable LoadPT()
        {
            var pt = from t in db.PHONGTROs
                     from q in db.LOAIPHONGs
                     where t.MALP == q.MALP && t.TRANGTHAIAN == true
                     select new { MAPT = t.MAPT, MALP = t.MALP, TENPHONG = t.TENPHONG, SLTOIDA = t.SLTOIDA, TRANGTHAI = t.TRANGTHAI, SONGUOIHIENTAI = t.SONGUOIHIENTAI };
            return pt;
        }
        public IQueryable LoadPT(int Maloaiphong,string keys)
        {
            var pt = from t in db.PHONGTROs
                     from q in db.LOAIPHONGs
                     where t.MALP == q.MALP && t.MALP == Maloaiphong && t.TRANGTHAIAN == true && t.TENPHONG.Contains(keys)
                     select new { MAPT = t.MAPT, MALP = t.MALP, TENPHONG = t.TENPHONG, SLTOIDA = t.SLTOIDA, TRANGTHAI = t.TRANGTHAI, SONGUOIHIENTAI = t.SONGUOIHIENTAI };
            return pt;
        }
        public void ThemPhong(int malp, string tenphong, int sltd)
        {
            PHONGTRO phongtro = new PHONGTRO();
            phongtro.MALP = malp;
            phongtro.TENPHONG = tenphong;
            phongtro.SLTOIDA = sltd;
            phongtro.SONGUOIHIENTAI = 0;
            phongtro.TRANGTHAI = "Chưa cho thuê";
            phongtro.TRANGTHAIAN = true;
            db.PHONGTROs.InsertOnSubmit(phongtro);
            db.SubmitChanges();
        }
        public void XoaPhong(int maphong)
        {
            var phong = db.PHONGTROs.Single(t => t.MAPT == maphong);
            phong.TRANGTHAIAN = false;
            db.SubmitChanges();
        }
        public void SuaPhong(int mapt,int malp, string tenphong, int sltd)
        {
            var phong = db.PHONGTROs.Single(t => t.MAPT == mapt);
            if(phong.TRANGTHAI== "Đã cho thuê")
            {
                MessageBox.Show("Phòng đang cho thuê không được sửa!");
                return;
            }
            phong.MALP = malp;
            phong.TENPHONG = tenphong;
            phong.SLTOIDA = sltd;
            db.SubmitChanges();
        }
        public int CountPhong()
        {
            return db.PHONGTROs.Where(t=>t.TRANGTHAIAN==true).Count();
        }
        public List<PHONGTRO> LoadMenuPT()
        {
            //return db.PHONGTROs.Select(t => t).ToList();
            var pt = (from t in db.PHONGTROs
                     where t.TRANGTHAIAN == true
                     select t).ToList();

            return pt;
        }
        public string TenLoai(int MaLoai)
        {
            var tenloai = db.LOAIPHONGs.Single(t => t.MALP == MaLoai).TENLOAIPHONG;
            return tenloai.ToString();
        }
        public int GetDientich(int maloai)
        {
            var dt = int.Parse(db.LOAIPHONGs.Where(t => t.MALP == maloai).Single().DIENTICH.ToString());
            return dt;
        }
        public IQueryable LoadCTTB(int mapt)
        {
            var cctb = from u in db.CTTHIETBIs
                       from t in db.THIETBIs
                       where u.MATB == t.MATB && u.MAPT == mapt
                       select new { MATB = u.MATB, TENTB = t.TENTB, SOLUONG = u.SOLUONG, TINHTRANG = u.TINHTRANG,MALOAI=t.MALOAI };
            return cctb;
        }
        public bool KTTB(int mapt, int matb)
        {
            var thietbi = db.CTTHIETBIs.Where(t => t.MAPT == mapt && t.MATB == matb);
            if (thietbi.Count() == 0)
            {
                return true;
            }
            return false;
        }
        public void ThemThietBiVaoPhong(int mapt, int matb, int soluong, string tinhtrang)
        {
            if (!KTTB(mapt, matb))
            {
                MessageBox.Show("Thiết bị đã có trong phòng!");
                return;
            }
            CTTHIETBI cttb = new CTTHIETBI();
            cttb.MAPT = mapt;
            cttb.MATB = matb;
            cttb.SOLUONG = soluong;
            cttb.TINHTRANG = tinhtrang;
            db.CTTHIETBIs.InsertOnSubmit(cttb);
            db.SubmitChanges();
        }
        public void XoaThietBiTrongPhong(int mapt, int matb)
        {
            var thietbi = db.CTTHIETBIs.Where(t => t.MAPT == mapt && t.MATB == matb);
            db.CTTHIETBIs.DeleteAllOnSubmit(thietbi);
            db.SubmitChanges();

        }
        public void SuaThietBiTrongPhong(int mapt, int matb, int soluong, string tinhtrang)
        {
            CTTHIETBI cctb = db.CTTHIETBIs.SingleOrDefault(t => t.MAPT == mapt && t.MATB == matb);
            cctb.SOLUONG = soluong;
            cctb.TINHTRANG = tinhtrang;
            db.SubmitChanges();
        }

        //Khách trọ
        public IQueryable loadkt()
        {
            var rs = from c in db.KHACHTROs where c.TRANGTHAI == true select new { MAKT = c.MAKT, HOTEN = c.HOTEN, CMND = c.CMND, GIOITINH = c.GIOITINH, NGHENGHIEP = c.NGHENGHIEP, DIACHI = c.DIACHI, SDT = c.SDT, HINH = c.HINH, CHUHO = c.CHUHO, NGAYSINH = c.NGAYSINH,GMAIL=c.GMAIL };
            return rs;
        }
        public IQueryable loadkt(string name)
        {
            var rs = from c in db.KHACHTROs where c.TRANGTHAI == true && c.HOTEN.Contains(name) || c.CMND.Contains(name) || c.SDT.Contains(name) select new { MAKT = c.MAKT, HOTEN = c.HOTEN, CMND = c.CMND, GIOITINH = c.GIOITINH, NGHENGHIEP = c.NGHENGHIEP, DIACHI = c.DIACHI, SDT = c.SDT, HINH = c.HINH, CHUHO = c.CHUHO, NGAYSINH = c.NGAYSINH, GMAIL = c.GMAIL };
            return rs;
        }
        public IQueryable loadktphieuthue()
        {
            var kt = (from u in db.PHIEUTHUEPHONGs select u.MAKT).ToList();
            var rs = from c in db.KHACHTROs where c.TRANGTHAI == true&&!kt.Contains(c.MAKT) select c;
            return rs;
        }
        public List<KHACHTRO> TreeKt()
        {
            var data = db.KHACHTROs.Where(t => t.CHUHO == true&&t.TRANGTHAI==true).ToList();
            return data;
        }
        public int CountChuHo()
        {
            return db.KHACHTROs.Count(t => t.CHUHO == true && t.TRANGTHAI == true);
        }
        public void ThemKT(DateTime ngaysinh, string Hoten, string cmnd, string gt, string nghenghiep, string diachi, string sdt, string hinh,string email)
        {
            KHACHTRO khachtro = new KHACHTRO();
            khachtro.HOTEN = Hoten;
            khachtro.NGAYSINH = ngaysinh;
            khachtro.CMND = cmnd;
            khachtro.GIOITINH = gt;
            khachtro.NGHENGHIEP = nghenghiep;
            khachtro.DIACHI = diachi;
            khachtro.SDT = sdt;
            khachtro.CHUHO = false;
            khachtro.HINH = hinh;
            khachtro.TRANGTHAI = true;
            khachtro.GMAIL = email;
            db.KHACHTROs.InsertOnSubmit(khachtro);
            db.SubmitChanges();
        }
        public void XoaKT(int makt)
        {
            var kt = db.KHACHTROs.Single(t => t.MAKT == makt);
            kt.TRANGTHAI = false;
            db.SubmitChanges();
        }
        public void SuaKT(int makt, DateTime ngaysinh, string Hoten, string cmnd, string gt, string nghenghiep, string diachi, string sdt, string hinh,string email)
        {
            var khachtro = db.KHACHTROs.Single(t => t.MAKT == makt);
            khachtro.HOTEN = Hoten;
            khachtro.NGAYSINH = ngaysinh;
            khachtro.CMND = cmnd;
            khachtro.GIOITINH = gt;
            khachtro.NGHENGHIEP = nghenghiep;
            khachtro.DIACHI = diachi;
            khachtro.SDT = sdt;
            khachtro.HINH = hinh;
            khachtro.GMAIL = email;
            db.SubmitChanges();
        }
        public string GetMail(int makt)
        {
            var kt = db.KHACHTROs.Single(t => t.MAKT == makt).GMAIL.ToString();
            return kt;
        }
        //Dịch vụ
        public IQueryable LoadDV()
        {
            var dichvu = from u in db.DICHVUs
                         select new { MADV = u.MADV, TENDV = u.TENDV, DONGIA = u.DONGIA, DONVITINH = u.DONVITINH, TRANGTHAI = u.TRANGTHAI };
            return dichvu;
        }
        public bool KTDV(string ten)
        {
            var tendv = from u in db.DICHVUs
                        where u.TENDV == ten
                        select u;

            if (tendv.Count() == 0)
            {
                return true;
            }
            return false;
        }
        public void ThemDV(string tendv, int dg, string dvt)
        {
            if (!KTDV(tendv))
            {
                MessageBox.Show("Dịch vụ này đã có!");
                return;
            }
            DICHVU dv = new DICHVU();
            dv.TENDV = tendv;
            dv.DONGIA = dg;
            dv.DONVITINH = dvt;
            dv.TRANGTHAI = "False";
            db.DICHVUs.InsertOnSubmit(dv);
            db.SubmitChanges();
        }
        public bool KTMADV(int madv)
        {
            var dv = from u in db.CTPHIEUTHUs
                     where u.MADV == madv
                     select u;

            if (dv.Count() == 0)
            {
                return true;
            }
            return false;
        }
        public bool KTDVDIENNUOC(int madv)
        {
            var dv = from u in db.DICHVUs
                     where u.MADV == madv && u.TRANGTHAI == "True"
                     select u;

            if (dv.Count() == 0)
            {
                return true;
            }
            return false;
        }
        public void XoaDV(int madv)
        {
            if (!KTDVDIENNUOC(madv))
            {
                MessageBox.Show("Điện và nước không thể xóa!!");
                return;
            }
            if (!KTMADV(madv))
            {
                MessageBox.Show("Không thể xóa dịch vụ này!!");
                return;
            }
            var dv = db.DICHVUs.Where(t => t.MADV == madv);
            db.DICHVUs.DeleteAllOnSubmit(dv);
            db.SubmitChanges();
        }
        public void SuaDV(int madv, int dg, string dvt)
        {
            DICHVU dv = db.DICHVUs.Single(t => t.MADV == madv);
            dv.DONGIA = dg;
            dv.DONVITINH = dvt;
            db.SubmitChanges();
        }

        //Xử lý lập phiếu thuê
        public int CountDV()
        {
            return db.DICHVUs.Count();
        }
        public List<DICHVU> LoadCheckdv()
        {
            return db.DICHVUs.Where(t => t.TRANGTHAI == "False").ToList();
        }
        public KHACHTRO khachtro(int makt)
        {
            KHACHTRO kt = new KHACHTRO();
            kt.HOTEN = db.KHACHTROs.Single(t => t.MAKT == makt).HOTEN;
            kt.CMND = db.KHACHTROs.Single(t => t.MAKT == makt).CMND;
            kt.NGAYSINH = db.KHACHTROs.Single(t => t.MAKT == makt).NGAYSINH;
            kt.GIOITINH = db.KHACHTROs.Single(t => t.MAKT == makt).GIOITINH;
            kt.NGHENGHIEP = db.KHACHTROs.Single(t => t.MAKT == makt).NGHENGHIEP;
            kt.DIACHI = db.KHACHTROs.Single(t => t.MAKT == makt).DIACHI;
            kt.SDT = db.KHACHTROs.Single(t => t.MAKT == makt).SDT;
            kt.HINH = db.KHACHTROs.Single(t => t.MAKT == makt).HINH;
            kt.GMAIL = db.KHACHTROs.Single(t => t.MAKT == makt).GMAIL;
            return kt;
        }
        public IQueryable LoadMutilPT2(int malp)
        {
            var pt = from u in db.LOAIPHONGs
                     from t in db.PHONGTROs
                     where u.MALP == t.MALP && t.MALP == malp && t.TRANGTHAI == "Chưa cho thuê"
                     select new
                     {
                         MAPT = t.MAPT,
                         TENPT = t.TENPHONG,
                         SLTD = t.SLTOIDA,
                         SLHT = t.SONGUOIHIENTAI,
                         DONGIA = u.DONGIA,
                         TRANGTHAI = t.TRANGTHAI
                     };
            return pt;
        }
        public IQueryable LoadMutilPT1(int malp)
        {
            var pt = from u in db.LOAIPHONGs
                     from t in db.PHONGTROs
                     where u.MALP == t.MALP && t.MALP == malp && t.TRANGTHAI == "Đã cho thuê"
                     select new
                     {
                         MAPT = t.MAPT,
                         TENPT = t.TENPHONG,
                         SLTD = t.SLTOIDA,
                         SLHT = t.SONGUOIHIENTAI,
                         DONGIA = u.DONGIA,
                         TRANGTHAI = t.TRANGTHAI                      
                     };
            return pt;
        }
        public IQueryable LoadMutilPTTB(int maphongtro)
        {
            var tb = from t in db.CTTHIETBIs
                     from u in db.THIETBIs
                     where t.MATB == u.MATB && t.MAPT == maphongtro
                     select new { MATB = t.MATB, TENTB = u.TENTB, SL = t.SOLUONG, TT = t.TINHTRANG };
            return tb;
        }
        public int getMadv(string ten)
        {
            return db.DICHVUs.Single(t => t.TENDV == ten).MADV;
        }
        public bool KiemTraDichVuChinh(int Madv)
        { 
            var dv = db.DICHVUs.Where(t => t.MADV == Madv && t.TRANGTHAI == "False");
            if (dv.Count() != 0)
            {
                return true;
            }
            return false;
        }
        public void ThemDichVuDienNuoc(string tendv)
        {
            CTPHIEUTHU ccpt = new CTPHIEUTHU();
            ccpt.MATHU = db.PHIEUTHUTIENs.Select(t => t).OrderByDescending(e => e.MATHU).FirstOrDefault().MATHU;
            ccpt.MADV = getMadv(tendv);
            if (KiemTraDichVuChinh(getMadv(tendv)))
            {
                ccpt.THANHTIEN = db.DICHVUs.Where(t => t.MADV == getMadv(tendv)).Single().DONGIA;
            }
            db.CTPHIEUTHUs.InsertOnSubmit(ccpt);
            db.SubmitChanges();

        }
        public bool KTSOLUONGTOIDAVASOLUONGHIENTAI(int maphongtro)
        {
            int sltd = int.Parse(db.PHONGTROs.Single(t => t.MAPT == maphongtro).SLTOIDA.ToString());
            int slht = int.Parse(db.PHONGTROs.Single(t => t.MAPT == maphongtro).SONGUOIHIENTAI.ToString());
            if (slht == sltd)
            {
                return false;
            }
            return true;
        }
        public void CapNhatSauKhiLapPhieuThue(int maphong, int makt, bool ch)
        {
            PHONGTRO pt = db.PHONGTROs.Single(t => t.MAPT == maphong);
            pt.TRANGTHAI = "Đã cho thuê";
            KHACHTRO KT = db.KHACHTROs.Single(t => t.MAKT == makt);
            KT.CHUHO = ch;
            db.SubmitChanges();
        }
        public void LapPhieuThue(int makt, int maphongtro, DateTime ngaythue, bool ch,int manv)
        {
            PHIEUTHUEPHONG ptt = new PHIEUTHUEPHONG();
            ptt.MAKT = makt;
            ptt.MAPT = maphongtro;
            ptt.NGAYTHUE = ngaythue;
            ptt.TRANGTHAI = "Đang thuê";
            ptt.MANV = manv;
            db.PHIEUTHUEPHONGs.InsertOnSubmit(ptt);
            CapNhatSauKhiLapPhieuThue(maphongtro, makt, ch);
            db.SubmitChanges();
        }
        public void LapPhieuThu(DateTime ngaybatdau)
        {
            PHIEUTHUTIEN ptt = new PHIEUTHUTIEN();
            ptt.MAPHIEUTHUE = db.PHIEUTHUEPHONGs.Select(t => t).OrderByDescending(e => e.MAPHIEUTHUE).FirstOrDefault().MAPHIEUTHUE;
            ptt.NGAYBATDAU = ngaybatdau;
            ptt.DATTHANHTOAN = 0;
            db.PHIEUTHUTIENs.InsertOnSubmit(ptt);
            db.SubmitChanges();
        }
        public void ThemKTV2(DateTime ngaysinh, string Hoten, string cmnd, string gt, string nghenghiep, string diachi, string sdt, bool chuho, string hinh,string mail)
        {
            KHACHTRO khachtro = new KHACHTRO();
            khachtro.HOTEN = Hoten;
            khachtro.NGAYSINH = ngaysinh;
            khachtro.CMND = cmnd;
            khachtro.GIOITINH = gt;
            khachtro.NGHENGHIEP = nghenghiep;
            khachtro.DIACHI = diachi;
            khachtro.SDT = sdt;
            khachtro.CHUHO = chuho;
            khachtro.HINH = hinh;
            khachtro.GMAIL = mail;
            khachtro.TRANGTHAI = true;
            db.KHACHTROs.InsertOnSubmit(khachtro);
            db.SubmitChanges();
        }
        public int countdemslkt(int maphongtro)
        {
            return db.PHIEUTHUEPHONGs.Count(t => t.MAPT == maphongtro && t.TRANGTHAI == "Đang thuê");
        }
        public void CapNhatSoLuongnguoi(int maphongtro)
        {
            PHONGTRO pt = db.PHONGTROs.Single(t => t.MAPT == maphongtro);
            pt.SONGUOIHIENTAI = countdemslkt(maphongtro);
            db.SubmitChanges();
            if (countdemslkt(maphongtro) == 0)
            {
                PHONGTRO phongtro = db.PHONGTROs.Single(t => t.MAPT == maphongtro);
                phongtro.TRANGTHAI = "Chưa cho thuê";
                db.SubmitChanges();
            }
        }
        public int GetLastRecordKhachTro()
        {
            var kq = db.KHACHTROs.Select(t => t).OrderByDescending(e => e.MAKT).FirstOrDefault().MAKT;
            return kq;
        }

        //Danh sách phiếu thuê
        public IQueryable LoadPhieuThuePhieuThuv2DangThue()
        {
            var kq = (from t in db.PHIEUTHUTIENs
                      from u in db.PHIEUTHUEPHONGs
                      from e in db.KHACHTROs
                      where t.MAPHIEUTHUE == u.MAPHIEUTHUE && u.MAKT == e.MAKT && e.CHUHO == true && u.TRANGTHAI == "Đang thuê"
                      select new { MAPHIEUTHUE = u.MAPHIEUTHUE, MAKT = u.MAKT, MAPT = u.MAPT, NGAYTHUE = u.NGAYTHUE, NGAYTRA = u.NGAYTRA, TRANGTHAI = u.TRANGTHAI }).Distinct();

            return kq;
        }
        public string TenLoaiPhong(int Maphong)
        {
            int maloai;
            maloai = int.Parse(db.PHONGTROs.Single(t => t.MAPT == Maphong).MALP.ToString());
            return TenLoai(maloai);
        }
        public string TenPhong(int maphong)
        {
            return db.PHONGTROs.Single(t => t.MAPT == maphong).TENPHONG;
        }
        public string Soluongtoida(int maphong)
        {
            return db.PHONGTROs.Single(t => t.MAPT == maphong).SLTOIDA.ToString();
        }
        public string Soluonghientai(int maphong)
        {
            return db.PHONGTROs.Single(t => t.MAPT == maphong).SONGUOIHIENTAI.ToString();
        }
        public IQueryable LoadCTTBPHONG(int maphongtro)
        {
            var thietbi = from u in db.CTTHIETBIs
                          from t in db.THIETBIs
                          where u.MATB == t.MATB && u.MAPT == maphongtro
                          select new
                          {
                              TTB = t.TENTB,
                              Sl = u.SOLUONG,
                              TT = u.TINHTRANG
                          };
            return thietbi;
        }
        public IQueryable LoadDVPHIEUTHUEv2(int maphieuthue)
        {
            var kq = db.PHIEUTHUTIENs.Where(t => t.MAPHIEUTHUE == maphieuthue).OrderByDescending(e => e.MATHU).FirstOrDefault().MATHU;

            var phieuthuechitiet = from u in db.CTPHIEUTHUs
                                   from e in db.DICHVUs
                                   where u.MADV == e.MADV && u.MATHU == kq && e.TRANGTHAI == "False"
                                   select new { MADV = e.MADV, TENDV = e.TENDV, DVT = e.DONVITINH, DONGIA = e.DONGIA };
            return phieuthuechitiet;

        }
        public IQueryable loadDvphieuthuev2(int maphieuthue)
        {
            var kq = db.PHIEUTHUTIENs.Where(t => t.MAPHIEUTHUE == maphieuthue).OrderByDescending(e => e.MATHU).FirstOrDefault().MATHU;

            var phieuthuechitiet = from u in db.CTPHIEUTHUs
                                   from e in db.DICHVUs
                                   where u.MADV == e.MADV && u.MATHU == kq && e.TRANGTHAI == "False"
                                   select new { MADV = e.MADV, TENDV = e.TENDV, DVT = e.DONVITINH, DONGIA = e.DONGIA };
            return phieuthuechitiet;

        }
        public IQueryable LoadPhieuThuePhieuThuv2NgungThue()
        {
            var kq = (from t in db.PHIEUTHUTIENs
                      from u in db.PHIEUTHUEPHONGs
                      from e in db.KHACHTROs
                      where t.MAPHIEUTHUE == u.MAPHIEUTHUE && u.MAKT == e.MAKT && e.CHUHO == true && u.TRANGTHAI == "Ngừng thuê"
                      select new { MAPHIEUTHUE = u.MAPHIEUTHUE, MAKT = u.MAKT, MAPT = u.MAPT, NGAYTHUE = u.NGAYTHUE, NGAYTRA = u.NGAYTRA, TRANGTHAI = u.TRANGTHAI }).Distinct();

            return kq;
        }

        //Đăng ký dịch vụ
        public IQueryable LoadDVKHONGDIENNUOC()
        {
            return db.DICHVUs.Where(t => t.TRANGTHAI == "False");
        }
        public IQueryable LoadPhieuThuePhieuThuv3()
        {
            var kq = (from t in db.PHIEUTHUTIENs
                      from u in db.PHIEUTHUEPHONGs
                      from e in db.KHACHTROs
                      where t.MAPHIEUTHUE == u.MAPHIEUTHUE && u.MAKT == e.MAKT && e.CHUHO == true &&e.TRANGTHAI==true && u.TRANGTHAI == "Đang thuê"
                      select new { MAPHIEUTHUE = u.MAPHIEUTHUE, MAKT = u.MAKT, MAPT = u.MAPT, NGAYTHUE = u.NGAYTHUE, NGAYTRA = u.NGAYTRA, TRANGTHAI = u.TRANGTHAI }).Distinct();
            return kq;
        }
        public bool KTDVCOTRONGHETHONG(int maphieuthu, int madv)
        {
            var kq = db.CTPHIEUTHUs.Where(t => t.MATHU == maphieuthu && t.MADV == madv);
            if (kq.Count() != 0)
            {
                //có giá trị            
                return false;
            }
            return true;
        }
        public void ThemDichVuv2(int maphieuthue, int madichvu)
        {
            var kq = db.PHIEUTHUTIENs.Where(t => t.MAPHIEUTHUE == maphieuthue).OrderByDescending(e => e.MATHU).FirstOrDefault().MATHU;
            if (!KTDVCOTRONGHETHONG(kq, madichvu))
            {
                MessageBox.Show("Dịch vụ này đã có");
                return;
            }
            CTPHIEUTHU ctpt = new CTPHIEUTHU();
            ctpt.MATHU = kq;
            ctpt.MADV = madichvu;
            if (KiemTraDichVuChinh(madichvu))
            {
                ctpt.THANHTIEN = db.DICHVUs.Where(t => t.MADV == madichvu).Single().DONGIA;
            }
            db.CTPHIEUTHUs.InsertOnSubmit(ctpt);
            db.SubmitChanges();

        }
        public void XoaDichVuv2(int maphieuthue, int madichvu)
        {
            var kq = db.PHIEUTHUTIENs.Where(t => t.MAPHIEUTHUE == maphieuthue).OrderByDescending(e => e.MATHU).FirstOrDefault().MATHU;
            var dv = db.CTPHIEUTHUs.Single(t => t.MATHU == kq && t.MADV == madichvu);
            db.CTPHIEUTHUs.DeleteOnSubmit(dv);
            db.SubmitChanges();

        }

        //Trả phòng
        public IQueryable lOADROIPHONGFalse()
        {
            var kq = from t in db.PHIEUTHUEPHONGs
                     from e in db.KHACHTROs
                     where t.MAKT == e.MAKT && e.CHUHO == false
                     select new { MAPHIEUTHUE = t.MAPHIEUTHUE, MAKT = t.MAKT, MAPT = t.MAPT, NGAYTHUE = t.NGAYTHUE, NGAYTRA = t.NGAYTRA, TRANGTHAI = t.TRANGTHAI };
            return kq;
        }
        public IQueryable lOADROIPHONGTrue()
        {
            var kq = from t in db.PHIEUTHUEPHONGs
                     from e in db.KHACHTROs
                     where t.MAKT == e.MAKT && e.CHUHO == true
                     select new { MAPHIEUTHUE = t.MAPHIEUTHUE, MAKT = t.MAKT, MAPT = t.MAPT, NGAYTHUE = t.NGAYTHUE, NGAYTRA = t.NGAYTRA, TRANGTHAI = t.TRANGTHAI };
            return kq;
        }
        public void RoiPhong(int maphieuthue)
        {
            PHIEUTHUEPHONG phieuthue = db.PHIEUTHUEPHONGs.Single(t => t.MAPHIEUTHUE == maphieuthue);
            phieuthue.TRANGTHAI = "Ngừng thuê";
            phieuthue.NGAYTRA = DateTime.Now;
            db.SubmitChanges();
        }
        public bool KIEMTRATHANHTOANHET(int maphieuthue)
        {
            var phieuthu = db.PHIEUTHUTIENs.Where(t => t.MAPHIEUTHUE == maphieuthue && t.DATTHANHTOAN == 0);
            if (phieuthu.Count() != 0)
            {
                return false;//không cho thanh toán
            }
            return true;//cho thanh toán
        }
        public void TRAPHONG(int maphieuthue, int maphong)
        {
            PHIEUTHUEPHONG ptt = db.PHIEUTHUEPHONGs.Single(t => t.MAPHIEUTHUE == maphieuthue);
            ptt.TRANGTHAI = "Ngừng thuê";
            ptt.NGAYTRA = DateTime.Now;
            (from p in db.PHIEUTHUEPHONGs where p.MAPT == maphong select p).ToList().ForEach(x => x.TRANGTHAI = "Ngừng thuê");
            db.SubmitChanges();
        }
        public IQueryable LoadPhieuThuePhieuThu()
        {
            var kq = (from t in db.PHIEUTHUTIENs
                      from u in db.PHIEUTHUEPHONGs
                      where t.MAPHIEUTHUE == u.MAPHIEUTHUE && u.TRANGTHAI == "Đang thuê"
                      select new { MAPHIEUTHUE = u.MAPHIEUTHUE, MAKT = u.MAKT, MAPT = u.MAPT, NGAYTHUE = u.NGAYTHUE, NGAYTRA = u.NGAYTRA, TRANGTHAI = u.TRANGTHAI }).Distinct();

            return kq;
        }
        public IQueryable loadphieuthuv2(int maphieuthue)
        {
            var pt = from t in db.PHIEUTHUTIENs
                     from e in db.PHIEUTHUEPHONGs
                     from a in db.KHACHTROs
                     where e.MAPHIEUTHUE == t.MAPHIEUTHUE && a.MAKT == e.MAKT && t.MAPHIEUTHUE == maphieuthue && a.CHUHO == true
                     select new { MATHU = t.MATHU, MAPHIEUTHUE = t.MAPHIEUTHUE, NGAYBATDAU = t.NGAYBATDAU, NGAYKETHUC = t.NGAYKETTHUC, TONGTIEN = t.TONGTIEN, DATHANHTOAN = t.DATTHANHTOAN };
            return pt;
        }
        public IQueryable loadcttphieuthu(int maphieuthu)
        {
            var ctpt = from t in db.CTPHIEUTHUs
                       from e in db.DICHVUs
                       where t.MADV == e.MADV && t.MATHU == maphieuthu && e.TRANGTHAI == "False"
                       select new { MATHU = t.MATHU, MADV = t.MADV, TENDV = e.TENDV, THANHTIEN = t.THANHTIEN };
            return ctpt;
        }
        public string TinhTienDien(int socu, int somoi)
        {
            //tính tiền điện
            int tb = somoi - socu;
            int giatiendien = int.Parse(db.DICHVUs.Where(t => t.MADV == 1).Single().DONGIA.ToString());
            int result = tb * giatiendien;
            return result.ToString();

        }
        public string TinhTienNuoc(int socu, int somoi)
        {
            //Tính tiền nước
            int tb = somoi - socu;
            int giatiennuoc = int.Parse(db.DICHVUs.Where(t => t.MADV == 2).Single().DONGIA.ToString());
            int result = tb * giatiennuoc;
            return result.ToString();
        }
        public void CapNhattienDienNuocvaochitietphieuthu(int maphieuthu, int madv, int csc, int csm, int thanhtien)
        {
            CTPHIEUTHU ctpt = db.CTPHIEUTHUs.Single(t => t.MATHU == maphieuthu && t.MADV == madv);
            ctpt.CHISOCU = csc;
            ctpt.CHISOMOI = csm;
            ctpt.THANHTIEN = thanhtien;
            db.SubmitChanges();
        }
        public void CapNhatTongTien(int maphieuthu, int maphongtro)
        {
            int giaphongtro = int.Parse((from t in db.LOAIPHONGs
                                         from e in db.PHONGTROs
                                         where t.MALP == e.MALP && e.MAPT == maphongtro
                                         select t).Single().DONGIA.ToString());
            var data = db.CTPHIEUTHUs.Where(t => t.MATHU == maphieuthu);
            int tongtien = int.Parse(data.Sum(t => t.THANHTIEN).ToString());
            PHIEUTHUTIEN ptt = db.PHIEUTHUTIENs.Single(t => t.MATHU == maphieuthu);
            ptt.TONGTIEN = tongtien + giaphongtro;
            db.SubmitChanges();
        }
        public bool KIEMTRATHANHTOAN(int maphieuthu)
        {
            var tiendien = db.CTPHIEUTHUs.Where(t => t.MADV == 1 && t.MATHU == maphieuthu && t.THANHTIEN.Equals(null));
            var tiennuoc = db.CTPHIEUTHUs.Where(t => t.MADV == 2 && t.MATHU == maphieuthu && t.THANHTIEN.Equals(null));
            if (tiendien.Count() != 0 || tiennuoc.Count() != 0)
            {
                return false;
            }
            return true;
        }
        public void ThanhToan(int maphieuthu)
        {
            PHIEUTHUTIEN ptt = db.PHIEUTHUTIENs.Single(t => t.MATHU == maphieuthu);
            ptt.DATTHANHTOAN = 1;
            ptt.NGAYKETTHUC = DateTime.Now;
            db.SubmitChanges();
        }
        public void Otrotieptuc(int maphieuthue)
        {
            PHIEUTHUTIEN phieuthutien = new PHIEUTHUTIEN();
            phieuthutien.MAPHIEUTHUE = maphieuthue;
            phieuthutien.NGAYBATDAU = DateTime.Now;
            phieuthutien.DATTHANHTOAN = 0;
            db.PHIEUTHUTIENs.InsertOnSubmit(phieuthutien);
            db.SubmitChanges();
        }
        public void TaoCTTPTHU(int madv)
        {
            CTPHIEUTHU ctpt = new CTPHIEUTHU();
            ctpt.MATHU = db.PHIEUTHUTIENs.Select(t => t).OrderByDescending(e => e.MATHU).FirstOrDefault().MATHU;
            ctpt.MADV = madv;
            if (KiemTraDichVuChinh(madv))
            {
                ctpt.THANHTIEN = db.DICHVUs.Where(t => t.MADV == madv).Single().DONGIA;
            }
            db.CTPHIEUTHUs.InsertOnSubmit(ctpt);
            db.SubmitChanges();
        }
        public IQueryable loadphieuthu(int maphieuthue)
        {

            var pt = from t in db.PHIEUTHUTIENs
                     where t.MAPHIEUTHUE == maphieuthue
                     select new { MATHU = t.MATHU, MAPHIEUTHUE = t.MAPHIEUTHUE, NGAYBATDAU = t.NGAYBATDAU, NGAYKETHUC = t.NGAYKETTHUC, TONGTIEN = t.TONGTIEN, DATHANHTOAN = t.DATTHANHTOAN };
            return pt;
        }
        public string gettongtien(int maphieuthu)
        {
            return db.PHIEUTHUTIENs.Single(t => t.MATHU == maphieuthu).TONGTIEN.ToString();
        }
        public IQueryable InPhieuThue(int maphieuthue)
        {
            return db.PHIEUTHUEPHONGs.Where(t => t.MAPHIEUTHUE == maphieuthue);
        }
        public IQueryable loadcttphieuthu2(int maphieuthu)
        {
            return db.CTPHIEUTHUs.Where(t => t.MATHU == maphieuthu);

        }
        //Gửi Report Mail
        public void GuiMail(string filename,string email)
        {
                string str2 = filename;
                // tạo một tin nhắn và thêm những thông tin cần thiết như: ai gửi, người nhận, tên tiêu đề
                MailMessage mail = new MailMessage("longlehoang212@gmail.com", email, "Thanh toán", "Thanh toán kỳ hóa đơn tiếp theo"); //
                mail.IsBodyHtml = true;
                //gửi tin nhắn
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.UseDefaultCredentials = false;
                client.Port = 587; // dùng port 587
                                   // thêm vào credential vì SMTP server cần nó để biết được email + password của email
                client.Credentials = new System.Net.NetworkCredential("longlehoang212@gmail.com", "HLNT1105");
                client.EnableSsl = true; //thiết lập kết nối SSL với SMTP server nên cần gán nó bằng true
                if (str2 != null)
                {
                    mail.Attachments.Add(new Attachment(str2));
                }
            client.Send(mail);
            MessageBox.Show("Gửi mail thành công");
        }
        public List<FileNameReportcs> GetFile()
        {
            string root = "..//..//HoaDonHangThang";
            string[] fileEntries = Directory.GetFiles(root);
            List<FileNameReportcs> filenal = new List<FileNameReportcs>();
            foreach (string fileName in fileEntries)
            {
                string date = fileName;
                DateTime time = Directory.GetCreationTime(date);
                FileNameReportcs fn = new FileNameReportcs();
                fn.Filename = date;
                fn.Time = time;
                filenal.Add(fn);
            }
            return filenal.OrderByDescending(x => x.Time).ToList();
        }
        public string GetEmail(int makt)
        {
            var email = db.KHACHTROs.Single(t => t.MAKT == makt).GMAIL;
            return email.ToString();
        }

    }
}
