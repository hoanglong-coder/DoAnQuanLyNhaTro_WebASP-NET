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
    public partial class FrmWellcome : Form
    {
        XuLy xl = new XuLy();
        int IDNHANVIEN;
        public FrmWellcome()
        {
            InitializeComponent();
        }
        public FrmWellcome(string username)
        {
            InitializeComponent();
            this.IDNHANVIEN = xl.GetIDNhanVien(username);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Width += 2;
            if(panel1.Width>=panel2.Width)
            {
                timer1.Stop();
                FrmMain frm = new FrmMain(IDNHANVIEN);
                frm.Show();
                this.Hide();            
            }
        }

        private void FrmWellcome_Load(object sender, EventArgs e)
        {

        }
    }
}
