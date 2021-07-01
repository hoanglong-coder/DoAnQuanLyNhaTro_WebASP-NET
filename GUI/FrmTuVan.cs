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
    public partial class FrmTuVan : Form
    {
        XuLy xl = new XuLy();
        public FrmTuVan()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textEdit1.ReadOnly = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit2.Text==""||comboBox1.Text=="")
            {
                MessageBox.Show("Điền đủ thông tin!");
                return;
            }
            Product pr = new Product();
            pr.Location = comboBox1.SelectedIndex;
            pr.Dientich1 = int.Parse(textEdit2.Text);
            textEdit1.Text = xl.TuvanGia(xl.TimKDuLieuGanNhat(pr, 10)).ToString();
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
