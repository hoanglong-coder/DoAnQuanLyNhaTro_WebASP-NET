using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using BLL_DAL;
namespace GUI
{
    public partial class FrmDangNhap : Form
    {
        XuLy xl = new XuLy();
        Thread th;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
             int nLeft,
             int nTop,
             int nRight,
             int nBottom,
             int nWidth,
             int nHeigh   
        );
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("Không được bỏ trống!");
                return;
            }
            if(!xl.DangNhap(textBox1.Text,textBox2.Text))
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu !");
                return;
            }
            MessageBox.Show("Đăng nhập thành công !");
            FrmWellcome frm = new FrmWellcome(textBox1.Text);
            frm.Show();
            this.Hide();
            
        }
        private void opennewform(object obj)
        {
            Application.Run(new FrmWellcome(textBox1.Text));
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 15, 15));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 15, 15));
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Không được bỏ trống!");
                    return;
                }
                if (!xl.DangNhap(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu !");
                    return;
                }
                MessageBox.Show("Đăng nhập thành công !");
                FrmWellcome frm = new FrmWellcome(textBox1.Text);
                frm.Show();
                this.Hide();
            }

        }
    }
}
