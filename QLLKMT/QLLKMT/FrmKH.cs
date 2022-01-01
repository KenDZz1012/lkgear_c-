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
    public partial class FrmKH : Form
    {
        Connect conn = new Connect();
        public FrmKH()
        {
            InitializeComponent();
            txtGT.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            showData();
            txtMaKH.Enabled = false;
        }
        private void showData()
        {
            try
            {
                String sql = "Select * From KhachHang";
                DataSet ds = conn.getData(sql, "KhachHang", null);
                dataGridView1.DataSource = ds.Tables["KhachHang"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearData()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtGT.Text = "";
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Main frm = new Main();
            frm.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenkh = txtTenKH.Text;                
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string a = "Thường";
                string b = "0";
                string sql = "Insert into KhachHang values(@tenkh,@sdt,@email,@gt,@loaikh)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenkh", tenkh));
                data.Add(new SqlParameter("@sdt", sdt));
                data.Add(new SqlParameter("@email", email));
                data.Add(new SqlParameter("@gt", b));
                data.Add(new SqlParameter("@loaikh", a));
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
                string makh = txtMaKH.Text;
                string tenkh = txtTenKH.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string lkh = comboBox1.SelectedItem.ToString();
                String sql = "Update KhachHang set TenKH = @tenkh,SDT = @sdt,Email = @email,LoaiKH = @lkh where MaKH = @makh";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenkh", tenkh));
                data.Add(new SqlParameter("@sdt", sdt));
                data.Add(new SqlParameter("@email", email));
                data.Add(new SqlParameter("@lkh", lkh));
                data.Add(new SqlParameter("@makh", makh));
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
            string makh = txtMaKH.Text;
            try
            {
                string query = "DELETE FROM KhachHang WHERE MaKH = @makh";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@makh", makh));
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

        private void btnLM_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaKH.Text = dataGridView1.Rows[index].Cells["MaKH"].Value.ToString();
                txtTenKH.Text = dataGridView1.Rows[index].Cells["TenKH"].Value.ToString();
                txtSDT.Text = dataGridView1.Rows[index].Cells["SDT"].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[index].Cells["Email"].Value.ToString();
                txtGT.Text = dataGridView1.Rows[index].Cells["GiaTriMua"].Value.ToString();
                comboBox1.SelectedItem = dataGridView1.Rows[index].Cells["LoaiKH"].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select * From KhachHang Where TenKH like '%'+@tenkh+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenkh", tk));
                DataSet ds = conn.getData(sql, "KhachHang", data);
                dataGridView1.DataSource = ds.Tables["KhachHang"];
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
