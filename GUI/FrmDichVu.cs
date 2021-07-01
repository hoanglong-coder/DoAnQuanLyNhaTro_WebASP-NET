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
    public partial class FrmDichVu : Form
    {
        XuLy xl = new XuLy();
        public FrmDichVu()
        {
            InitializeComponent();
        }

        private void FrmDichVu_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.LoadDV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    textEdit1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textEdit2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textEdit3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "" || textEdit2.Text == "" || textEdit3.Text == "")
            {
                MessageBox.Show("Không được để trống!");
                return;
            }
            xl.ThemDV(textEdit1.Text, int.Parse(textEdit2.Text), textEdit3.Text);
            dataGridView1.DataSource = xl.LoadDV();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xl.XoaDV(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            
            dataGridView1.DataSource = xl.LoadDV();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "" || textEdit2.Text == "" || textEdit3.Text == "")
            {
                MessageBox.Show("Không được để trống!");
                return;
            }
            xl.SuaDV(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), int.Parse(textEdit2.Text), textEdit3.Text);
            dataGridView1.DataSource = xl.LoadDV();
        }

    }
}
