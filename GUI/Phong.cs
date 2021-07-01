using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Phong : UserControl
    {
        public Phong()
        {
            InitializeComponent();
        }


        private string _tenPhong;
        [Category("Custom Props")]     
        public string TenPhong
        {
            get { return _tenPhong; }
            set { _tenPhong = value; label2.Text=value; }
        }

        private string _maPhong;
        [Category("Custom Props")]
        public string MaPhong
        {
            get { return _maPhong; }
            set { _maPhong = value;}
        }

        private string _maLP;
        [Category("Custom Props")]
        public string MaLP
        {
            get { return _maLP; }
            set { _maLP = value; }
        }

        private string _slTD;
        [Category("Custom Props")]
        public string SLTD
        {
            get { return _slTD; }
            set { _slTD = value; }
        }

        private string _slHT;
        [Category("Custom Props")]
        public string SLHT
        {
            get { return _slHT; }
            set { _slHT = value; }
        }

        private string _Tt;
        [Category("Custom Props")]
        public string TrangThai
        {
            get { return _Tt; }
            set { _Tt = value; }
        }
        private string _Dt;
        [Category("Custom Props")]
        public string DienTich
        {
            get { return _Dt; }
            set { _Dt = value; }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(216, 224, 242);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
           this.BackColor = Color.SkyBlue;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(216, 224, 242);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.SkyBlue;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(216, 224, 242);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.SkyBlue;
        }

        private void Phong_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(216, 224, 242);
        }

        private void Phong_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.SkyBlue;
        }

        private void Phong_Load(object sender, EventArgs e)
        {

        }

        private void Phong_Click(object sender, EventArgs e)
        {

        }
    }
}
