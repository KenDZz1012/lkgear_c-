using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class frmBaoHanh : Form
    {
        Connect conn = new Connect();
        public frmBaoHanh()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                string sql = "Select BaoHanh.MaBH,SanPham.TenSP,BaoHanh.TenNhaCC,BaoHanh.Qty,BaoHanh.NgayBH,BaoHanh.TinhTrang from BaoHanh,SanPham Where BaoHanh.MaSP = SanPham.MaSP group by BaoHanh.MaBH,SanPham.TenSP,BaoHanh.TenNhaCC,BaoHanh.Qty,BaoHanh.NgayBH,BaoHanh.TinhTrang";
                DataSet ds = conn.getData(sql, "BaoHanh", null);
                dataGridView1.DataSource = ds.Tables["BaoHanh"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void frmBaoHanh_Load(object sender, EventArgs e)
        {
            showData();
            showcbb();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
        private void showcbb()
        {
            try
            {
                String query = "Select * from NhaCC";
                DataSet rs = new DataSet();
                rs = conn.getData(query, "NhaCC", null);
                comboBox1.DataSource = rs.Tables["NhaCC"];
                comboBox1.DisplayMember = "TenNhaCC";
                comboBox1.ValueMember = "TenNhaCC";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select BaoHanh.MaBH,SanPham.TenSP,BaoHanh.TenNhaCC,BaoHanh.Qty,BaoHanh.NgayBH,BaoHanh.TinhTrang from BaoHanh,SanPham Where BaoHanh.MaSP = SanPham.MaSP and BaoHanh.MaSP like '%'+@masp+'%' or SanPham.TenSP like '%'+@tensp+'%' or SanPham.TenLSP like '%'+@tenlsp+'%' or SanPham.TenNhaCC like '%'+@tenncc+'%' or BaoHanh.MaBH like '%'+@mabh+'%' group by BaoHanh.MaBH,SanPham.TenSP,BaoHanh.TenNhaCC,BaoHanh.Qty,BaoHanh.NgayBH,BaoHanh.TinhTrang";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", tk));
                data.Add(new SqlParameter("@tensp", tk));
                data.Add(new SqlParameter("@tenlsp", tk));
                data.Add(new SqlParameter("@tenncc", tk));
                data.Add(new SqlParameter("@mabh", tk));
                DataSet ds = conn.getData(sql, "SanPham", data);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ncc = comboBox1.SelectedValue.ToString();
                string sql = "Select BaoHanh.MaBH,SanPham.TenSP,BaoHanh.TenNhaCC,BaoHanh.Qty,BaoHanh.NgayBH,BaoHanh.TinhTrang from BaoHanh,SanPham Where BaoHanh.MaSP = SanPham.MaSP and SanPham.TenNhaCC = @tenncc group by BaoHanh.MaBH,SanPham.TenSP,BaoHanh.TenNhaCC,BaoHanh.Qty,BaoHanh.NgayBH,BaoHanh.TinhTrang";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenncc", ncc));
                DataSet ds = conn.getData(sql, "SanPham", data);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    lbMaSP.Text = dataGridView1.Rows[index].Cells["MaBH"].Value.ToString();
                    lbTenSP.Text = dataGridView1.Rows[index].Cells["TenSP"].Value.ToString();
                    numericUpDown1.Value = int.Parse(dataGridView1.Rows[index].Cells["Qty"].Value.ToString());
                    string tensp = dataGridView1.Rows[index].Cells["TenSP"].Value.ToString();
                    string sql = "Select * from SanPham Where TenSP = @tensp";
                    List<SqlParameter> data = new List<SqlParameter>();
                    data.Add(new SqlParameter("@tensp", tensp));
                    DataSet ds = conn.getData(sql, "SanPham", data);
                    byte[] b = (byte[])ds.Tables["SanPham"].Rows[0]["AnhSP"];
                    pictureBox1.Image = ByteArrayToImage(b);
                    
                }
                else
                {
                    MessageBox.Show("Ko thấy");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mabh = lbMaSP.Text;
                string tensp = lbTenSP.Text;
                int qty = Convert.ToInt32(numericUpDown1.Value);
                string sql = "Update BaoHanh set Qty = @qty where MaBH = @mabh";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@qty", qty));
                data.Add(new SqlParameter("@mabh", mabh));
                conn.Updatedata(sql, data);
                string sql2 = "Update SanPham set SoLuongHong = @qty where TenSP = @tensp";
                List<SqlParameter> dta = new List<SqlParameter>();
                dta.Add(new SqlParameter("@tensp", tensp));
                dta.Add(new SqlParameter("@qty", qty));
                conn.Updatedata(sql2, dta);
                MessageBox.Show("Sửa thông tin thành công");
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                string mabh = lbMaSP.Text;
                string tensp = lbTenSP.Text;
                string sql = "Delete from BaoHanh where MaBH = @mabh";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mabh", mabh));
                conn.Updatedata(sql, data);
                int a = 0;
                string sql1 = "Update SanPham set SoLuongHong = @qty where TenSP = @tensp";
                List<SqlParameter> dta = new List<SqlParameter>();
                dta.Add(new SqlParameter("@tensp", tensp));
                dta.Add(new SqlParameter("@qty", a));
                conn.Updatedata(sql1, dta);
                MessageBox.Show("Sản phẩm đã được bảo hành");
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SP frm = new SP();
            frm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
