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

namespace QLLKMT
{
    public partial class FrmNCC : Form
    {
        Connect conn = new Connect();
        public FrmNCC()
        {
            InitializeComponent();
            txtSLSP.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            showData();
            txtMaNCC.Enabled = false;
        }

        private void showData()
        {
            try
            {
                String sql = "Select * From NhaCC";
                DataSet ds = conn.getData(sql, "NhaCC", null);
                dataGridView1.DataSource = ds.Tables["NhaCC"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearData()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtSLSP.Text = "";
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SP frm = new SP();
            frm.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenncc = txtTenNCC.Text;
                int a = 0;
                string sql = "Insert into NhaCC values(@tenncc,@slsp)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenncc", tenncc));
                data.Add(new SqlParameter("@slsp", a));
                conn.Updatedata(sql, data);
                MessageBox.Show("Thêm mới thành công");
                showData();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string mancc = txtMaNCC.Text;
                string tenncc = txtTenNCC.Text;
                string sql = "Update NhaCC set TenNhaCC=@tenncc Where MaNhaCC = @mancc";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenncc", tenncc));
                data.Add(new SqlParameter("@mancc", mancc));
                conn.Updatedata(sql, data);
                MessageBox.Show("Sửa thành công");
                showData();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mancc = txtMaNCC.Text;
            try
            {
                string query = "DELETE FROM NhaCC WHERE MaNhaCC = @mancc";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mancc", mancc));
                conn.Updatedata(query, data);
                MessageBox.Show("Xóa thành công");
                showData();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaNCC.Text = dataGridView1.Rows[index].Cells["MaNhaCC"].Value.ToString();
                txtTenNCC.Text = dataGridView1.Rows[index].Cells["TenNhaCC"].Value.ToString();
                txtSLSP.Text = dataGridView1.Rows[index].Cells["SLSP"].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select * From NhaCC Where TenNhaCC like '%'+@tenncc+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenncc", tk));
                DataSet ds = conn.getData(sql, "NhaCC", data);
                dataGridView1.DataSource = ds.Tables["NhaCC"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void FrmNCC_Load(object sender, EventArgs e)
        {

        }
    }
}
