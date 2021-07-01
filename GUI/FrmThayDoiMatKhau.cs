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
    public partial class FrmThayDoiMatKhau : Form
    {
        XuLy xl = new XuLy();
        int manv;
        public FrmThayDoiMatKhau()
        {
            InitializeComponent();
        }
        public FrmThayDoiMatKhau(int idnhanvien)
        {
            InitializeComponent();
            this.manv = idnhanvien;
        }

        private void FrmThayDoiMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "")
            {
                MessageBox.Show("Điền đủ thông tin");
                return;
            }
            xl.DoiMatKhau(manv, textBox1.Text, textBox2.Text);
            MessageBox.Show("Thay đổi thành công");
            this.Close();
        }
    }
}
