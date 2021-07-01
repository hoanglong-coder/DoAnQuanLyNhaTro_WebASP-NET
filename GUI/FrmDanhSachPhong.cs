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
    public partial class FrmDanhSachPhong : Form
    {
        XuLy xl = new XuLy();
        public FrmDanhSachPhong()
        {
            InitializeComponent();
        }

        private void phong1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phong 1");
        }

        private void phong3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phong 2");
        }


        private void FrmDanhSachPhong_Load(object sender, EventArgs e)
        {
            textEdit1.ReadOnly = true;
            textEdit2.ReadOnly = true;
            textEdit3.ReadOnly = true;
            textEdit4.ReadOnly = true;
            textEdit5.ReadOnly = true;
            textEdit6.ReadOnly = true;
            DynamicUserControls();
            for (int i = 0; i < xl.CountPhong(); i++)
            {
                TreeNode node = new TreeNode(xl.LoadMenuPT()[i].TENPHONG);
                treeView1.Nodes.Add(node);
                
            }
        }
        //Tải control từ sql lên gui
        private void DynamicUserControls()
        {
            flowLayoutPanel1.Controls.Clear();

            int soluong = xl.CountPhong();
            Phong[] phongs = new Phong[soluong];          
            for (int i = 0; i < phongs.Length; i++)
            {
                phongs[i] = new Phong();
                phongs[i].MaPhong = xl.LoadMenuPT()[i].MAPT.ToString();
                phongs[i].MaLP = xl.LoadMenuPT()[i].MALP.ToString();
                phongs[i].TenPhong = xl.LoadMenuPT()[i].TENPHONG;
                phongs[i].SLHT = xl.LoadMenuPT()[i].SONGUOIHIENTAI.ToString();
                phongs[i].SLTD = xl.LoadMenuPT()[i].SLTOIDA.ToString();
                phongs[i].TrangThai = xl.LoadMenuPT()[i].TRANGTHAI;
                phongs[i].DienTich = xl.GetDientich(int.Parse(xl.LoadMenuPT()[i].MALP.ToString())).ToString();
                flowLayoutPanel1.Controls.Add(phongs[i]);
                phongs[i].Click += new System.EventHandler(this.Phong_Click);
            }
        }
        private void Phong_Click(object sender, EventArgs e)
        {
            Phong obj = (Phong)sender;
            textEdit1.Text = obj.TenPhong;
            textEdit2.Text = xl.TenLoai(int.Parse(obj.MaLP));
            textEdit3.Text = obj.SLTD;
            textEdit4.Text = obj.SLHT;
            textEdit5.Text = obj.DienTich;
            textEdit6.Text = obj.TrangThai;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DynamicUserControls();
            for (int i = 0; i < xl.CountPhong(); i++)
            {
                TreeNode node = new TreeNode(xl.LoadMenuPT()[i].TENPHONG);
                treeView1.Nodes.Add(node);

            }
        }

        private void textEdit4_KeyPress(object sender, KeyPressEventArgs e)
        {
        }


    }
}
