using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Threading;
using BLL_DAL;
namespace GUI
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Thread th;
        XuLy xl = new XuLy();
        int IdNhanvien;
        public FrmMain()
        {
            InitializeComponent();
        }
        public FrmMain(int idnhanvien)
        {
            InitializeComponent();
            this.IdNhanvien = idnhanvien;
        }
        public bool IsFormActive(Form form)
        {
            bool Isopend = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        Isopend = true;
                    }

                }
            }
            return Isopend;
        }
        public void ViewChildForm(Form _form)
        {
            if (!IsFormActive(_form))
            {
                _form.MdiParent = this;
                _form.Show();
            }
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (xl.KiemTraThue_ThanhToan(IdNhanvien))
            {
                ribbonPage4.Dispose();
                ribbonPage5.Dispose();
                return;
            }
            else if (xl.KiemTraQuanLyPhong(IdNhanvien))
            {
                ribbonPage1.Dispose();
                ribbonPage3.Dispose();
                ribbonPage4.Dispose();
                return;
            }
            else if(xl.KiemTraQuanLyKhachHang(IdNhanvien))
            {
                ribbonPage5.Dispose();
                ribbonPage3.Dispose();
                ribbonPage1.Dispose();
                return;
            }
            else if (xl.KiemTrachua(IdNhanvien))
            {
                ribbonPage5.Dispose();
                ribbonPage3.Dispose();
                ribbonPage1.Dispose();
                ribbonPage4.Dispose();
            }
            else if (xl.KiemTraToanQuyen(IdNhanvien))
            {
                return;
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmThueTro frm = new FrmThueTro(IdNhanvien);
            frm.Name = "Lập phiếu thuê";
            ViewChildForm(frm);
        }
        private void opennewform(object obj)
        {
            Application.Run(new FrmDangNhap());
        }
        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                FrmDangNhap frm = new FrmDangNhap();
                frm.Show();
                this.Hide();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmDangNhap frm = new FrmDangNhap();
            frm.Show();
            this.Hide();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDanhSachPhong frm = new FrmDanhSachPhong();
            frm.Name = "Danh sách phòng trọ";
            ViewChildForm(frm);
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmThietBi frm = new FrmThietBi();
            frm.Name = "Quản lý thiết bị";
            ViewChildForm(frm);
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrnPhongTro frm = new FrnPhongTro();
            frm.Name = "Quản lý phòng";
            ViewChildForm(frm);
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPhongThietbi frm = new FrmPhongThietbi();
            frm.Name = "Phòng - thiết bị";
            ViewChildForm(frm);
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDanhSachKhachTro frm = new FrmDanhSachKhachTro();
            frm.Name = "Khách trọ";
            ViewChildForm(frm);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDichVu frm = new FrmDichVu();
            frm.Name = "Dịch vụ";
            ViewChildForm(frm);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPhieuThue frm = new FrmPhieuThue();
            frm.Name = "Danh sách phiếu thuê";
            ViewChildForm(frm);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDangKyDichVu frm = new FrmDangKyDichVu();
            frm.Name = "Đăng ký dịch vụ";
            ViewChildForm(frm);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmTraPhong frm=new FrmTraPhong();
            frm.Name = "Trả phòng";
            ViewChildForm(frm);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmThanhToan frm = new FrmThanhToan();
            frm.Name = "Thanh toán";
            ViewChildForm(frm);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmThongTinNhanVien frm = new FrmThongTinNhanVien(IdNhanvien);
            frm.Name = "Thông tin nhân viên";
            ViewChildForm(frm);
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmThayDoiMatKhau frm = new FrmThayDoiMatKhau(IdNhanvien);
            frm.Name = "Đổi mật khẩu";
            frm.ShowDialog();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }
    }
}