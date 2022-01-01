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
    public partial class Ycbh : Form
    {
        Connect conn = new Connect();
        public Ycbh()
        {
            InitializeComponent();
        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
        private void showData()
        {
            try
            {
                string sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham";
                DataSet ds = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
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
        private void Ycbh_Load(object sender, EventArgs e)
        {
            showData();
            showcbb();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string masp = lbMaSP.Text;
                string tenncc = comboBox1.SelectedValue.ToString();
                int qty = Convert.ToInt32(numericUpDown1.Value);
                string tt = "Đang Bảo Hành";
                string ngaybh = DateTime.Today.ToString("MM/dd/yyyy");
                string sql = "Insert into BaoHanh values(@masp,@tenncc,@qty,@ngaybh,@tt)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", masp));
                data.Add(new SqlParameter("@tenncc", tenncc));
                data.Add(new SqlParameter("@qty", qty));
                data.Add(new SqlParameter("@ngaybh", ngaybh));
                data.Add(new SqlParameter("@tt", tt));
                conn.Updatedata(sql, data);
                string sql2 = "Update SanPham set SoLuongHong = @qty where MaSP = @masp";
                List<SqlParameter> dta = new List<SqlParameter>();
                dta.Add(new SqlParameter("@masp", masp));
                dta.Add(new SqlParameter("@qty", qty));
                conn.Updatedata(sql2, dta);
                MessageBox.Show("Đăng kí bảo hành thành công");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,DonGia,GiaNhap,BaoHanh From SanPham Where MaSP like '%'+@masp+'%' or TenSP like '%'+@tensp+'%' or TenLSP like '%'+@tenlsp+'%' or TenNhaCC like '%'+@tenncc+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", tk));
                data.Add(new SqlParameter("@tensp", tk));
                data.Add(new SqlParameter("@tenlsp", tk));
                data.Add(new SqlParameter("@tenncc", tk));
                DataSet ds = conn.getData(sql, "SanPham", data);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ncc = comboBox1.SelectedValue.ToString();
                string sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,DonGia,GiaNhap,BaoHanh From SanPham Where TenNhaCC = @tenncc";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenncc", ncc));
                DataSet ds = conn.getData(sql, "SanPham", data);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    lbMaSP.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                    lbTenSP.Text = dataGridView1.Rows[index].Cells["TenSP"].Value.ToString();
                    string masp = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                    string sql = "Select * from SanPham Where MaSP = @masp";
                    List<SqlParameter> data = new List<SqlParameter>();
                    data.Add(new SqlParameter("@masp", masp));
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SP frm = new SP();
            frm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
