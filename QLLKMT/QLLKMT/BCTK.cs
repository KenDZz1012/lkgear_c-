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
    public partial class BCTK : Form
    {
        public BCTK()
        {
            InitializeComponent();
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tkdt frm = new tkdt();
            frm.Show();
            this.Hide();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tkenhanvien frm = new tkenhanvien();
            frm.Show();
            this.Hide();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tkkh frm = new tkkh();
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
