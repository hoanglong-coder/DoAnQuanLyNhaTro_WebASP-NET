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
    public partial class FrmDangKyDichVu : Form
    {
        XuLy xl = new XuLy();
        public FrmDangKyDichVu()
        {
            InitializeComponent();
        }

        private void FrmDangKyDichVu_Load(object sender, EventArgs e)
        {
            comboBox2.DataSource = xl.loadkt ();
            comboBox2.DisplayMember = "HOTEN";
            comboBox2.ValueMember = "MAKT";
            comboBox3.DataSource = xl.LoadPT();
            comboBox3.DisplayMember = "TENPHONG";
            comboBox3.ValueMember = "MAPT";
            comboBox1.DataSource = xl.LoadDVKHONGDIENNUOC();
            comboBox1.DisplayMember = "TENDV";
            comboBox1.ValueMember = "MADV";
            dataGridView1.DataSource = xl.LoadPhieuThuePhieuThuv3();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    comboBox2.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    comboBox3.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textEdit3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    dataGridView2.DataSource = xl.loadDvphieuthuev2(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                }
            }
            catch (Exception)
            {

            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            xl.ThemDichVuv2(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), int.Parse(comboBox1.SelectedValue.ToString()));
            dataGridView2.DataSource = xl.loadDvphieuthuev2(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xl.XoaDichVuv2(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()));
            dataGridView2.DataSource = xl.loadDvphieuthuev2(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
