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
    public partial class SP : Form
    {
        public SP()
        {
            InitializeComponent();
        }

        private void quảnLýToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FrmLSP frm = new FrmLSP();
            frm.Show();
            this.Hide();
        }

        private void ngườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNCC frm = new FrmNCC();
            frm.Show();
            this.Hide();
        }

        private void aloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSP frm = new FrmSP();
            frm.Show();
            this.Hide();
        }

        private void bảoHànhToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yêuCầuBảoHànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ycbh frm = new Ycbh();
            frm.Show();
            this.Hide();
        }

        private void danhSáchBảoHànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoHanh frm = new frmBaoHanh();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main frm = new Main();
            frm.Show();
            this.Hide();
        }
    }
}
