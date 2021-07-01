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
    public partial class FrmThueTro : Form
    {
        XuLy xl = new XuLy();
        List<DICHVU> listdv = new List<DICHVU>();
        int manv;
        public FrmThueTro()
        {
            InitializeComponent();
        }
        public FrmThueTro(int idnhanvien)
        {
            InitializeComponent();
            this.manv = idnhanvien;
        }


        public void Hidetext()
        {
            
            textEdit1.Enabled = false;
            textEdit3.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
            textEdit4.Enabled = false;
            textEdit5.Enabled = false;
            textEdit6.Enabled = false;
            buttonEdit1.Enabled = false;
            txtemail.Enabled = false;
        }
        public void RefestText()
        {
            textEdit1.Text = "";
            textEdit3.Text = "";
            comboBox1.Text = "";
            textEdit4.Text = "";
            textEdit5.Text = "";
            textEdit6.Text = "";
            txtemail.Text = "";
            buttonEdit1.Text = "";
        }
        public void EnaText()
        {
            textEdit1.Enabled = true;
            textEdit3.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;
            textEdit4.Enabled = true;
            textEdit5.Enabled = true;
            textEdit6.Enabled = true;
            txtemail.Enabled = true;
            buttonEdit1.Enabled = true;
        }
        public void LoadCheckBoxDV()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                int soluong = xl.CountDV();
                CheckBox[] check = new CheckBox[soluong];
                for (int i = 0; i < check.Length; i++)
                {
                    check[i] = new CheckBox();
                    check[i].Text = xl.LoadCheckdv()[i].TENDV;
                    flowLayoutPanel1.Controls.Add(check[i]);
                    check[i].CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
                }
            }
            catch (Exception)
            {

            }

        }
        private void FrmThueTro_Load(object sender, EventArgs e)
        {
            Hidetext();
            textEdit7.ReadOnly = true;
            textBox1.ReadOnly = true;
            textEdit8.ReadOnly = true;
            radioButton1.Checked = true;
            textEdit2.ReadOnly = true;
            textEdit9.ReadOnly = true;
            textEdit10.ReadOnly = true;
            radioButton1.Checked = true;         
            LoadCheckBoxDV();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }


        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                EnaText();
                comboBox6.Enabled = false;
                RefestText();
                pictureBox1.Image = null;

            }
            else if (!checkEdit1.Checked)
            {
                Hidetext();
                comboBox6.Enabled = true;
            }
        }

        private void comboBox6_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                textEdit1.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).HOTEN;
                textEdit3.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).CMND;
                dateTimePicker1.Value = DateTime.Parse(xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).NGAYSINH.ToString());
                comboBox1.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).GIOITINH;
                textEdit4.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).NGHENGHIEP;
                textEdit5.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).DIACHI;
                textEdit6.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).SDT;
                buttonEdit1.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).HINH;
                txtemail.Text = xl.khachtro(int.Parse(comboBox6.SelectedValue.ToString())).GMAIL;
                pictureBox1.Image = new Bitmap(buttonEdit1.Text);
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

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == 8) || Char.IsWhiteSpace(e.KeyChar)))
                e.Handled = true;
        }

        private void textEdit6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
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
            }
            pictureBox1.Image = new Bitmap(buttonEdit1.Text);
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    dataGridView1.DataSource = xl.LoadMutilPT2(int.Parse(comboBox2.SelectedValue.ToString()));
                }
                catch (Exception)
                {


                }
            }
            if (radioButton2.Checked)
            {
                try
                {
                    dataGridView1.DataSource = xl.LoadMutilPT1(int.Parse(comboBox2.SelectedValue.ToString()));
                }
                catch (Exception)
                {


                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    textEdit7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textEdit8.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textEdit11.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    textEdit12.Text = xl.GetDientich(int.Parse(comboBox2.SelectedValue.ToString())).ToString();
                    dataGridView2.DataSource = xl.LoadMutilPTTB(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                }
            }
            catch (Exception)
            {


            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
                
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentRow != null)
                {
                    textEdit2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    textEdit9.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    textEdit10.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();

                }
            }
            catch (Exception)
            {


            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
           
        }
        public void ThemCTPHIEUTHU()
        {
            xl.ThemDichVuDienNuoc("Điện");
            xl.ThemDichVuDienNuoc("Nước");
            foreach (CheckBox item in flowLayoutPanel1.Controls)
            {
                if (item.Checked)
                {
                    xl.ThemDichVuDienNuoc(item.Text);

                }
            }
        }


        public bool KiemTraDangKyItNhat1Dv()
        {
            int dem=0;
            foreach (CheckBox item in flowLayoutPanel1.Controls)
            {
                if(item.Checked==true)
                {
                    dem++;
                }
            }
            if(dem==0)
            {
                return false;
            }
            return true;
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "" || textEdit3.Text == "" || textEdit4.Text == "" || textEdit5.Text == "" || textEdit6.Text == ""||txtemail.Text=="")
            {
                MessageBox.Show("Điền đủ thông tin");
                return;
            }
            if (textEdit7.Text == "")
            {
                MessageBox.Show("Chưa chọn phòng thuê");
                return;
            }            
            if (checkEdit1.Checked == false)
            {               
                if (!xl.KTSOLUONGTOIDAVASOLUONGHIENTAI(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                {
                    MessageBox.Show("Phòng đã tối đa người ở !");
                    return;
                }
                if (radioButton1.Checked == true)
                {
                    if (!KiemTraDangKyItNhat1Dv())
                    {
                        MessageBox.Show("Chọn ít nhất một dịch vụ");
                        return;
                    }
                    try
                    {
                        xl.LapPhieuThue(int.Parse(comboBox6.SelectedValue.ToString()), int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), DateTime.Now, true, manv);
                        xl.LapPhieuThu(DateTime.Now);
                        comboBox6.DataSource = xl.loadkt();
                        comboBox6.DisplayMember = "HOTEN";
                        comboBox6.ValueMember = "MAKT";
                        ThemCTPHIEUTHU();
                        xl.CapNhatSoLuongnguoi(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Thuê trọ thành công");
                        this.Close();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Thất bại");
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    try
                    {
                        xl.LapPhieuThue(int.Parse(comboBox6.SelectedValue.ToString()), int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), DateTime.Now, false, manv);
                        xl.CapNhatSoLuongnguoi(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                        MessageBox.Show("Đã thêm khách trọ vào phòng");
                        this.Close();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Thất bại");
                    }
                }
                return;

            }
            if (checkEdit1.Checked == true)
            {
                if (textEdit1.Text == "" || textEdit3.Text == "" || textEdit4.Text == "" || textEdit5.Text == "" || textEdit6.Text == ""||txtemail.Text=="")
                {
                    MessageBox.Show("Điền đủ thông tin");
                    return;
                }
                if (!xl.KTSOLUONGTOIDAVASOLUONGHIENTAI(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                {
                    MessageBox.Show("Phòng đã tối đa người ở !");
                    return;
                }
                try
                {
                    xl.ThemKTV2(dateTimePicker1.Value, textEdit1.Text, textEdit3.Text, comboBox1.Text, textEdit4.Text, textEdit5.Text, textEdit6.Text, ktch(), buttonEdit1.Text,txtemail.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Không thêm được khách trọ");
                    return;
                }

                if (radioButton1.Checked == true)
                {
                    try
                    {
                        if (!KiemTraDangKyItNhat1Dv())
                        {
                            MessageBox.Show("Chọn ít nhất một dịch vụ");
                            return;
                        }
                        xl.LapPhieuThue(xl.GetLastRecordKhachTro(), int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), DateTime.Now, true, manv);
                        xl.LapPhieuThu(DateTime.Now);
                        comboBox6.DataSource = xl.loadkt();
                        comboBox6.DisplayMember = "HOTEN";
                        comboBox6.ValueMember = "MAKT";
                        ThemCTPHIEUTHU();
                        xl.CapNhatSoLuongnguoi(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Thuê trọ thành công");

                        this.Close();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Thất bại");
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    try
                    {
                        xl.LapPhieuThue(xl.GetLastRecordKhachTro(), int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), DateTime.Now, false, manv);
                        xl.CapNhatSoLuongnguoi(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("Thuê trọ thành công");
                        this.Close();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Thất bại");
                    }
                }
                return;
            }
        }
        public bool ktch()
        {
            if (radioButton1.Checked)
            {
                return true;
            }
            return false;
        }
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                flowLayoutPanel1.Enabled = true;
                simpleButton6.Text = radioButton2.Text;
                comboBox2.DataSource = xl.LoadLP();
                comboBox2.DisplayMember = "TENLOAIPHONG";
                comboBox2.ValueMember = "MALP";
            }
            flowLayoutPanel1.Enabled = false;
            simpleButton6.Text = "Lập phiếu thuê";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                flowLayoutPanel1.Enabled = false;
                comboBox2.DataSource = xl.LoadLP();
                comboBox2.DisplayMember = "TENLOAIPHONG";
                comboBox2.ValueMember = "MALP";
            }
            flowLayoutPanel1.Enabled = true;
        }

        private void buttonEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void buttonEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void buttonEdit1_Enter(object sender, EventArgs e)
        {
            
        }

        private void comboBox6_DropDown(object sender, EventArgs e)
        {
            comboBox6.DataSource = xl.loadktphieuthue();
            comboBox6.DisplayMember = "HOTEN";
            comboBox6.ValueMember = "MAKT";
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.DataSource = xl.LoadLP();
            comboBox2.DisplayMember = "TENLOAIPHONG";
            comboBox2.ValueMember = "MALP";
        }
    }
}
