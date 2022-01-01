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
    public partial class NV : Form
    {
        public NV()
        {
            InitializeComponent();
        }

        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCV frm = new FrmCV();
            frm.Show();
            this.Hide();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNV frm = new FrmNV();
            frm.Show();
            this.Hide();
        }

        private void tínhLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSalary frm = new frmSalary();
            frm.Show();
            this.Hide();
        }

        private void tuyểnDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void formTuyểnDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTD frm = new frmTD();
            frm.Show();
            this.Hide();
        }

        private void danhSáchTuyểnDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSTD frm = new DSTD();
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
