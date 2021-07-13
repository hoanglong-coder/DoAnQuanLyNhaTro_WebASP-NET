using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BLL_DAL;
namespace GUI
{
    public partial class FrmDanhSachKhachTro : Form
    {
        XuLy xl = new XuLy();
        public FrmDanhSachKhachTro()
        {
            InitializeComponent();
        }
        private void ReadOnly()
        {
            txthoten.Enabled = false;
            txtcmnd.Enabled = false;
            txtngaysinh.Enabled = false;
            txtgioitinh.Enabled = false;
            txtngenghiep.Enabled = false;
            txtdiachi.Enabled =false;
            txtsdt.Enabled = false;
            txturl.Enabled = false;
            txtemail.Enabled = false;
        }
        private void Refest()
        {
            txthoten.Text = "";
            txtcmnd.Text = "";
            txtngenghiep.Text = "";
            txtdiachi.Text = "";
            txtgioitinh.Text = "";
            txtsdt.Text = "";
            txturl.Text = "";
            txtemail.Text = "";
            txthoten.Focus();
        }
        private void FrmDanhSachKhachTro_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            dataGridView1.DataSource = xl.loadkt();

            for (int i = 0; i < xl.CountChuHo(); i++)
            {
                TreeNode node = new TreeNode(xl.TreeKt()[i].HOTEN);
                treeView1.Nodes.Add(node);

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Selected == true)
                {
                    txthoten.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtcmnd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtngaysinh.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[9].Value.ToString());
                    txtgioitinh.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    txtngenghiep.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtdiachi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtsdt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txturl.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtemail.Text = xl.GetEmail(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    Bitmap bit = new Bitmap(dataGridView1.CurrentRow.Cells[7].Value.ToString());
                    pictureBox1.Image = bit;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filename;
                filename = dlg.FileName;
                txturl.Text = filename;
                Bitmap bit = new Bitmap(txturl.Text);
                pictureBox1.Image = bit;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ReadOnly();
            simpleButton1.Enabled = false;
            simpleButton2.Enabled = false;
            simpleButton3.Enabled = false;
            try
            {
                if (dataGridView1.CurrentRow.Selected == true)
                {
                    txthoten.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtcmnd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtngaysinh.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[9].Value.ToString());
                    txtgioitinh.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    txtngenghiep.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtdiachi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtsdt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txturl.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtemail.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    Bitmap bit = new Bitmap(dataGridView1.CurrentRow.Cells[7].Value.ToString());
                    pictureBox1.Image = bit;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
            catch (Exception)
            {
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = true;
            simpleButton3.Enabled = true;

            txthoten.Enabled = true;
            txtcmnd.Enabled = true;
            txtngaysinh.Enabled = true;
            txtgioitinh.Enabled = true;
            txtngenghiep.Enabled = true;
            txtdiachi.Enabled = true;
            txtsdt.Enabled = true;
            txturl.Enabled = true;
            txtemail.Enabled = true;
            Refest();
        }
        private static Regex email_validation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }
        static Regex validate_emailaddress = email_validation();

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txthoten.Text == "" || txtcmnd.Text == "" || txtgioitinh.Text == "" || txtngenghiep.Text == "" || txtdiachi.Text == "" || txtsdt.Text == "" || txtemail.Text == "" || validate_emailaddress.IsMatch(txtemail.Text) != true)
            {
                MessageBox.Show("Chưa điền đủ thông tin hoặc sai!");
                return;
            }
            xl.ThemKT(txtngaysinh.Value, txthoten.Text, txtcmnd.Text, txtgioitinh.Text, txtngenghiep.Text, txtdiachi.Text, txtsdt.Text, txturl.Text,txtemail.Text);
            dataGridView1.DataSource = xl.loadkt();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            xl.XoaKT(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            dataGridView1.DataSource = xl.loadkt();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xl.SuaKT(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), txtngaysinh.Value, txthoten.Text, txtcmnd.Text, txtgioitinh.Text, txtngenghiep.Text, txtdiachi.Text, txtsdt.Text, txturl.Text,txtemail.Text);
            dataGridView1.DataSource = xl.loadkt();
        }

        private void txtcmnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit8_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var result = xl.loadkt(textEdit8.Text);
                dataGridView1.DataSource = result;
            }
            catch (Exception)
            {

            }
        }

        private void txtemail_Validating(object sender, CancelEventArgs e)
        {
                       
        }
    }
}
