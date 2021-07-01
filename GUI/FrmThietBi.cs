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
    public partial class FrmThietBi : Form
    {
        XuLy xl = new XuLy();
        public FrmThietBi()
        {
            InitializeComponent();
        }

        private void FrmThietBi_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.LoadLoaiTB();
            //comboBox1.DataSource = xl.LoadLoaiTB();
            //comboBox1.DisplayMember = "TENLOAI";
            //comboBox1.ValueMember = "ID_LOAITHIETBI";

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "")
            {
                MessageBox.Show("Điền đủ thông tin");
                return;
            }
            xl.ThemLoaiTB(textEdit1.Text);
            dataGridView1.DataSource = xl.LoadLoaiTB();
        }
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "")
            {
                MessageBox.Show("Điền đủ thông tin");
                return;
            }
            xl.SuaLoaiTB(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), textEdit1.Text);
            dataGridView1.DataSource = xl.LoadLoaiTB();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.DataSource = xl.LoadLoaiTB();
            comboBox1.DisplayMember = "TENLOAI";
            comboBox1.ValueMember = "MALOAI";

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text == "" | textEdit3.Text == ""|comboBox1.Text=="")
            {
                MessageBox.Show("Điền đủ thông tin");
                return;
            }
            xl.ThemTB(textEdit2.Text, int.Parse(textEdit3.Text), int.Parse(comboBox1.SelectedValue.ToString()));
            dataGridView2.DataSource = xl.LoadTB(int.Parse(comboBox1.SelectedValue.ToString()));
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            xl.XoaTB(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()));
            dataGridView2.DataSource = xl.LoadTB(int.Parse(comboBox1.SelectedValue.ToString()));
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text == "" | textEdit3.Text == "")
            {
                MessageBox.Show("Điền đủ thông tin");
                return;
            }
            xl.SuaTB(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()), textEdit2.Text, int.Parse(textEdit3.Text));
            dataGridView2.DataSource = xl.LoadTB(int.Parse(comboBox1.SelectedValue.ToString()));
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xl.XoaLoaiTB(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            dataGridView1.DataSource = xl.LoadLoaiTB();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            try
            {
                dataGridView2.DataSource = xl.LoadTB(int.Parse(comboBox1.SelectedValue.ToString()));
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentRow != null)
                {
                    textEdit2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    textEdit3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

        private void textEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
