using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_DAL;
namespace GUI
{
    public partial class FrmThongTinNhanVien : Form
    {
        XuLy xl = new XuLy();
        int manv;
        public FrmThongTinNhanVien()
        {
            InitializeComponent();
        }
        public FrmThongTinNhanVien(int Manv)
        {
            InitializeComponent();
            this.manv = Manv;
        }

        private void FrmThongTinNhanVien_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            NHANVIEN nv = xl.TaiKhoanNhanVien(manv);
            txtIdNhanvien.Text = nv.MANV.ToString();
            txtChucvu.Text = xl.GetChucVu(manv);
            txtTen.Text = nv.TENNV;
            txtNgaysinh.Text = nv.NGAYSINH.ToString();
            txtGioitinh.Text = nv.GIOITINH;
            txtDiaChi.Text = nv.DIACHI;
            buttonEdit1.Text = nv.HINH;
            try
            {
                Bitmap bit = new Bitmap(@"D:\DoAn\PhanMemThongMinh\DoAnQuanLyNhaTro\GUI\Image\" + buttonEdit1.Text);
                pictureBox1.Image = bit;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                Bitmap bit = new Bitmap(buttonEdit1.Text);
                pictureBox1.Image = bit;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
          
        }

        private void txtIdNhanvien_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnLuu.Enabled == false)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filename;
                filename = dlg.FileName;
                buttonEdit1.Text = filename;
                Bitmap bit = new Bitmap(buttonEdit1.Text);
                pictureBox1.Image = bit;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            btnLuu.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            xl.SuaThongTinNhanVien(int.Parse(txtIdNhanvien.Text), txtNgaysinh.Value, txtGioitinh.Text, txtDiaChi.Text, buttonEdit1.Text);
            simpleButton1.Enabled = true;
            btnLuu.Enabled = false;
            MessageBox.Show("Thay đổi thành công!");
        }

        private void buttonEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
