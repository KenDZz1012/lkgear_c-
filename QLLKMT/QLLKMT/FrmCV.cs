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
    public partial class FrmCV : Form
    {
        Connect conn = new Connect();
        public FrmCV()
        {
            InitializeComponent();
            showData();
            txtSLNV.Enabled = false;
            btnSuaCV.Enabled = false;
            btnXoa.Enabled = false;
            txtMaCV.Enabled = false;
        }

        private void showData()
        {
            try
            {
                String sql = "Select * From ChucVu";
                DataSet ds = conn.getData(sql, "ChucVu", null);
                dataGridView1.DataSource = ds.Tables["ChucVu"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NV frm = new NV();
            frm.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {          
                 string tencv = txtTenCV.Text;
                int a = 0;
                string sql = "Insert into ChucVu values(@tencv,@slnv)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tencv", tencv));
                data.Add(new SqlParameter("@slnv", a));
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaCV.Text = dataGridView1.Rows[index].Cells["MaChucVu"].Value.ToString();
                txtTenCV.Text = dataGridView1.Rows[index].Cells["TenChucVu"].Value.ToString();
                txtSLNV.Text = dataGridView1.Rows[index].Cells["SLNV"].Value.ToString();
            }
            btnSuaCV.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnSuaCV_Click(object sender, EventArgs e)
        {
            try
            {
                string macv = txtMaCV.Text;
                string tencv = txtTenCV.Text;
                string sql = "Update ChucVu set TenChucVu=@tencv Where MaChucVu = @macv";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tencv", tencv));
                data.Add(new SqlParameter("@macv", macv));
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
            string macv = txtMaCV.Text;
            try
            {
                string query = "DELETE FROM ChucVu WHERE MaChucVu = @macv";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@macv", macv));
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
        private void clearData()
        {
            txtMaCV.Text = "";
            txtTenCV.Text = "";
            txtSLNV.Text = "";
            btnXoa.Enabled = false;
            btnSuaCV.Enabled = false;
            btnThem.Enabled = true;
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
                string sql = "Select * From ChucVu Where TenChucVu like '%'+@tencv+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tencv", tk));
                DataSet ds = conn.getData(sql, "ChucVu", data);
                dataGridView1.DataSource = ds.Tables["ChucVu"];
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
    }
}
