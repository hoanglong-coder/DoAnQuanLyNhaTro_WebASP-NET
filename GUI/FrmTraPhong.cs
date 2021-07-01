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
    public partial class FrmTraPhong : Form
    {
        XuLy xl = new XuLy();
        public FrmTraPhong()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            //Thông tin phiếu thuê

            radioButton1.Checked = true;
            textBox1.ReadOnly = true;
            textEdit2.ReadOnly = true;
            textEdit1.ReadOnly = true;
            textEdit4.ReadOnly = true;
            textEdit3.ReadOnly = true;
            textEdit5.ReadOnly = true;
            comboBox2.DataSource = xl.loadkt();
            comboBox2.DisplayMember = "HOTEN";
            comboBox2.ValueMember = "MAKT";
            comboBox1.DataSource = xl.LoadPT();
            comboBox1.DisplayMember = "TENPHONG";
            comboBox1.ValueMember = "MAPT";

            //Thông tin khách hàng
            comboBox4.DataSource = xl.loadkt();
            comboBox4.DisplayMember = "HOTEN";
            comboBox4.ValueMember = "MAKT";
            comboBox5.DataSource = xl.loadkt();
            comboBox5.DisplayMember = "CMND";
            comboBox5.ValueMember = "MAKT";
            comboBox6.DataSource = xl.loadkt();
            comboBox6.DisplayMember = "GIOITINH";
            comboBox6.ValueMember = "MAKT";
            comboBox7.DataSource = xl.loadkt();
            comboBox7.DisplayMember = "NGHENGHIEP";
            comboBox7.ValueMember = "MAKT";
            comboBox8.DataSource = xl.loadkt();
            comboBox8.DisplayMember = "DIACHI";
            comboBox8.ValueMember = "MAKT";
            comboBox9.DataSource = xl.loadkt();
            comboBox9.ValueMember = "MAKT";
            comboBox9.DisplayMember = "SDT";
            comboBox10.DataSource = xl.loadkt();
            comboBox10.DisplayMember = "NGAYSINH";
            comboBox10.ValueMember = "MAKT";

            //Thông tin phòng

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                try
                {
                    if (dataGridView3.CurrentRow != null)
                    {
                        comboBox2.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox4.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox1.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString());
                        textEdit2.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                        textBox1.Text = dataGridView3.CurrentRow.Cells[5].Value.ToString();
                        comboBox5.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox6.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox7.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox8.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox9.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox10.SelectedValue = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                        textEdit1.Text = xl.TenLoaiPhong(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        textEdit3.Text = xl.TenPhong(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        textEdit4.Text = xl.Soluongtoida(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        textEdit5.Text = xl.Soluonghientai(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        dataGridView1.DataSource = xl.LoadCTTBPHONG(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        dataGridView2.DataSource = xl.LoadDVPHIEUTHUEv2(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()));
                    }
                }
                catch (Exception)
                {

                }              
            }  
            if(radioButton2.Checked)
            {
                try
                {
                    if (dataGridView3.CurrentRow != null)
                    {
                        comboBox2.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox4.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox1.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString());
                        textEdit2.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                        textBox1.Text = dataGridView3.CurrentRow.Cells[5].Value.ToString();
                        comboBox5.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox6.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox7.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox8.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox9.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        comboBox10.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                        textEdit1.Text = xl.TenLoaiPhong(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        textEdit3.Text = xl.TenPhong(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        textEdit4.Text = xl.Soluongtoida(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        textEdit5.Text = xl.Soluonghientai(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                        dataGridView1.Rows.Clear();
                        dataGridView2.Rows.Clear();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                dataGridView3.DataSource = xl.lOADROIPHONGFalse();
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dataGridView3.Rows.Clear();
                dataGridView3.DataSource = xl.lOADROIPHONGTrue();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                if (dataGridView3.CurrentRow.Cells[5].Value.ToString() == "Ngừng thuê")
                {
                    MessageBox.Show("Khách thuê này đã rời phòng!!");
                    return;
                }
                xl.RoiPhong(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()));
                xl.CapNhatSoLuongnguoi(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                textEdit5.Text = xl.Soluonghientai(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                dataGridView3.DataSource = xl.lOADROIPHONGFalse();
                MessageBox.Show("Rời phòng thành công!!");
            }
            if (radioButton1.Checked)
            {
                if(textBox1.Text=="")
                {
                    MessageBox.Show("Không có phiếu thuê");
                    return;
                }
                if (dataGridView3.CurrentRow.Cells[5].Value.ToString() == "Ngừng thuê")
                {
                    MessageBox.Show("Khách thuê này đã trả phòng!!");
                    return;
                }

                if (!xl.KIEMTRATHANHTOANHET(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString())))
                {
                    MessageBox.Show("Không thể trả phòng do chưa thanh toán hết khoản nợ!!");
                    return;
                }
                xl.TRAPHONG(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()), int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                xl.CapNhatSoLuongnguoi(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                textEdit5.Text = xl.Soluonghientai(int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                dataGridView3.DataSource = xl.lOADROIPHONGTrue();
            }
            MessageBox.Show("Trả phòng thành công!!");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
