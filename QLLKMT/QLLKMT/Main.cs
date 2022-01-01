using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLLKMT
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NV frm = new NV();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BanHang frm = new BanHang();
            frm.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            BCTK frm = new BCTK();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SP frm = new SP();
            frm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gthieu frm = new gthieu();
            frm.Show();
            this.Hide();
        }
        bool check = false;

        private void Main_Load(object sender, EventArgs e)
        {
            //if(check == false)
            //{
            //    Login frm = new Login();
            //    DialogResult rs = frm.ShowDialog();
            //    if (rs != DialogResult.OK)
            //    {
            //        Application.Exit();
            //        return; 
            //    }
            //    else
            //    {
            //        check = true;
            //    }
            //}
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BCTK frm = new BCTK();
            frm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmKH frm = new FrmKH();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
