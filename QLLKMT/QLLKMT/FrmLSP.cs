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
    public partial class FrmLSP : Form
    {
        Connect conn = new Connect();
        public FrmLSP()
        {
            InitializeComponent();
            showData();
            txtMaLSP.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtSLSP.Enabled = false;
        }


        public void showData()
        {
            try
            {
                String sql = "Select * From LoaiSP";
                DataSet ds = conn.getData(sql, "LoaiSP", null);
                dataGridView1.DataSource = ds.Tables["LoaiSP"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearData()
        {
            txtMaLSP.Text = "";
            txtTenLSP.Text = "";
            txtSLSP.Text = "";
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenlsp = txtTenLSP.Text;
                int a = 0;
                string sql = "Insert into LoaiSP values(@tenlsp,@slsp)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenlsp", tenlsp));
                data.Add(new SqlParameter("@slsp", a));
                if (tenlsp.Length == 0)
                {
                    MessageBox.Show("Khong de thong tin trong");
                }
                else
                {
                    conn.Updatedata(sql, data);
                    MessageBox.Show("Thêm mới thành công");
                    showData();
                    clearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SP frm = new SP();
            frm.Show();
            this.Hide();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string malsp = txtMaLSP.Text;
                string tenlsp = txtTenLSP.Text;
                string sql = "Update LoaiSP set TenLSP=@tenlsp Where MaLSP = @malsp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenlsp", tenlsp));
                data.Add(new SqlParameter("@malsp", malsp));
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
            string malsp = txtMaLSP.Text;
            try
            {
                string query = "DELETE FROM LoaiSP WHERE MaLSP = @malsp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@malsp", malsp));
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaLSP.Text = dataGridView1.Rows[index].Cells["MaLSP"].Value.ToString();
                txtTenLSP.Text = dataGridView1.Rows[index].Cells["TenLSP"].Value.ToString();
                txtSLSP.Text = dataGridView1.Rows[index].Cells["SLSP"].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select * From LoaiSP Where TenLSP like '%'+@tenlsp+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenlsp", tk));
                DataSet ds = conn.getData(sql, "LoaiSP", data);
                dataGridView1.DataSource = ds.Tables["LoaiSP"];
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
