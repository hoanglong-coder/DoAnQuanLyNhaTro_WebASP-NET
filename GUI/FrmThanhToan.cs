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
using DevExpress.XtraReports.UI;
namespace GUI
{
    public partial class FrmThanhToan : Form
    {
        XuLy xl = new XuLy();
        public FrmThanhToan()
        {
            InitializeComponent();
        }
        
        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            //Thông tin phiếu thuê
            textEdit12.ReadOnly = true;
            textBox1.ReadOnly = true;
            textEdit2.ReadOnly = true;
            textEdit1.ReadOnly = true;
            textEdit4.ReadOnly = true;
            textEdit3.ReadOnly = true;
            textEdit5.ReadOnly = true;
            textEdit8.ReadOnly = true;
            textEdit10.ReadOnly = true;
            dataGridView3.DataSource = xl.LoadPhieuThuePhieuThu();
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
            comboBox3.DataSource = xl.loadkt();
            comboBox3.DisplayMember = "GMAIL";
            comboBox3.ValueMember = "MAKT";


        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            textEdit6.Text = "";
            textEdit7.Text = "";
            textBox2.Text = "";
            textEdit9.Text = "";
            try
            {
                if(dataGridView3.CurrentRow!=null)
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
                    comboBox3.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString());
                    dataGridView1.DataSource = xl.loadphieuthuv2(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()));
                    
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi ở chọn phiếu thuê" + dataGridView3.CurrentRow.Cells[0].Value.ToString());
            }
            
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.CurrentRow!=null)
                {
                    
                   dataGridView2.DataSource=xl.loadcttphieuthu(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                   if (int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) == 0)
                   {
                       textEdit12.Text = "Chưa thanh toán";
                   }
                   else if (int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) == 1)
                   {
                       textEdit12.Text = "Đã thanh toán";
                   }

                 
                }
            }
            catch (Exception)
            {

            }
        }

        private void textEdit6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (int.Parse(textEdit7.Text) < int.Parse(textEdit6.Text))
            {
                MessageBox.Show("Số mới phải lớn hơn số củ");
                return;
            }
            try
            {
                 if(textEdit6.Text==""||textEdit7.Text=="")
                    {
                        MessageBox.Show("Điền đủ thông tin số điện");
                        return;
                    }
            //tính tiền điện
            textEdit8.Text = xl.TinhTienDien(int.Parse(textEdit6.Text), int.Parse(textEdit7.Text));
            xl.CapNhattienDienNuocvaochitietphieuthu(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()), 1, int.Parse(textEdit6.Text), int.Parse(textEdit7.Text), int.Parse(textEdit8.Text));
            xl.CapNhatTongTien(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()),int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
            }
            catch (Exception)
            {

                //MessageBox.Show("Chỉ cho nhập số nguyên");
            }
            

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (int.Parse(textEdit9.Text) < int.Parse(textBox2.Text))
            {
                MessageBox.Show("Số mới phải lớn hơn số củ");
                return;
            }
            try
            {
                if (textBox2.Text == "" || textEdit9.Text == "")
                {
                    MessageBox.Show("Điền đủ thông tin số nước");
                    return;
                }
                //tính tiền nước
                textEdit10.Text = xl.TinhTienNuoc(int.Parse(textBox2.Text), int.Parse(textEdit9.Text));
                xl.CapNhattienDienNuocvaochitietphieuthu(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()), 2, int.Parse(textBox2.Text), int.Parse(textEdit9.Text), int.Parse(textEdit10.Text));
                xl.CapNhatTongTien(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()), int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString()));
                
            }
            catch (Exception)
            {
                //MessageBox.Show("Chỉ cho nhập số nguyên");
            }
           
        
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells[5].Value.ToString()=="1")
            {
                MessageBox.Show("Hóa đơn này đã thanh toán");
                return;
            }
            if (!xl.KIEMTRATHANHTOAN(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
            {
                MessageBox.Show("Không thể thanh toán do chưa tính tiền điện và nước");
                return;
            }
            xl.ThanhToan(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

            


            DialogResult dialogResult = MessageBox.Show("Bạn có muốn ở trọ tiếp", "Ở trọ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
               
                xl.Otrotieptuc(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()));
                xl.TaoCTTPTHU(1);
                xl.TaoCTTPTHU(2);
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    xl.TaoCTTPTHU(int.Parse(row.Cells[1].Value.ToString()));
                }

                MessageBox.Show("Bạn đã tiếp tục ở trọ");
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Bạn đã ngưng thuê trọ");
            }
            dataGridView1.DataSource = xl.loadphieuthu(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()));
            MessageBox.Show("Thanh toán thành công");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            textEdit11.Text = xl.gettongtien(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            dataGridView1.DataSource = xl.loadphieuthu(int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString()));
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            XtraReport1 report = new XtraReport1();
            report.DataSource = xl.loadcttphieuthu2(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            report.ShowPreviewDialog();
            if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "0")
            {
                xl.GuiMail(xl.GetFile().FirstOrDefault().Filename, xl.GetEmail(int.Parse(dataGridView3.CurrentRow.Cells[1].Value.ToString())));
            }

        }
    }
}
