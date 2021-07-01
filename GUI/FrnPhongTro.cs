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
    public partial class FrnPhongTro : Form
    {
        XuLy xl = new XuLy();
        public FrnPhongTro()
        {
            InitializeComponent();
        }

        private void FrnPhongTro_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.LoadLP();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtDientich.Text == "" || txtTen.Text == "" || txtGia.Text == "")
            {
                MessageBox.Show("Không được để trống!");
                return;
            }
            xl.ThemLP(txtTen.Text, int.Parse(txtGia.Text), int.Parse(txtDientich.Text));
            dataGridView1.DataSource = xl.LoadLP();
        }

        private void txtDientich_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                xl.XoaLP(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                dataGridView1.DataSource = xl.LoadLP();
            }
            catch (Exception)
            {

                MessageBox.Show("Không thể xóa loại phòng do có phòng có loại phòng!");
                return;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xl.SuaLP(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), txtTen.Text, int.Parse(txtGia.Text), int.Parse(txtDientich.Text));
            dataGridView1.DataSource = xl.LoadLP();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FrmTuVan frm = new FrmTuVan();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Selected == true)
                {
                    txtTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtGia.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtDientich.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    comboBox1.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());

                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            cbLoaiphong.DataSource = xl.LoadLP();
            cbLoaiphong.DisplayMember = "TENLOAIPHONG";
            cbLoaiphong.ValueMember = "MALP";
            comboBox1.DataSource = xl.LoadLP();
            comboBox1.DisplayMember = "TENLOAIPHONG";
            comboBox1.ValueMember = "MALP";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            try
            {
                dataGridView2.DataSource = xl.LoadPT(int.Parse(cbLoaiphong.SelectedValue.ToString()));
            }
            catch (Exception)
            {

            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if(txtTenphong.Text==""||cbLoaiphong.Text==""||cbSoluong.Text=="")
            {
                MessageBox.Show("Điền đủ thông tin!");
                return;
            }
            xl.ThemPhong(int.Parse(cbLoaiphong.SelectedValue.ToString()), txtTenphong.Text, int.Parse(cbSoluong.Text));
            dataGridView2.DataSource = xl.LoadPT(int.Parse(cbLoaiphong.SelectedValue.ToString()));
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            xl.XoaPhong(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()));
            dataGridView2.DataSource = xl.LoadPT(int.Parse(cbLoaiphong.SelectedValue.ToString()));
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtTenphong.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                cbSoluong.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                
            }
        }

        private void cbLoaiphong_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedValue = cbLoaiphong.SelectedValue;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTenphong.Text == "" || cbLoaiphong.Text == "" || cbSoluong.Text == "")
                {
                    MessageBox.Show("Điền đủ thông tin!");
                    return;
                }
                xl.SuaPhong(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()), int.Parse(comboBox1.SelectedValue.ToString()), txtTenphong.Text, int.Parse(cbSoluong.Text));
                dataGridView2.DataSource = xl.LoadPT(int.Parse(cbLoaiphong.SelectedValue.ToString()));
            }
            catch (Exception)
            {

            }
        }
    }
}
