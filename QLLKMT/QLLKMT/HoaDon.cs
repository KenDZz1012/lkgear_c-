using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using QLLKMT.src.Database;
using System.IO;


namespace QLLKMT
{
    public partial class HoaDon : Form
    {
        Connect conn = new Connect();
        public HoaDon()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BanHang frm = new BanHang();
            frm.Show();
            this.Hide();
        }
        private void showData()
        {
            try
            {
                String sql = "Select * from HoaDon";
                DataSet ds = conn.getData(sql, "HoaDon", null);
                dataGridView1.DataSource = ds.Tables["HoaDon"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    string mahd = dataGridView1.Rows[index].Cells["MaHD"].Value.ToString();
                    frmPay frm = new frmPay(mahd);
                    frm.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showData();
        }
    }
}
