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
    public partial class FrmSP : Form
    {
        Connect conn = new Connect();
        public FrmSP()
        {
            InitializeComponent();
            showcbb();
            showData();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaSP.Enabled = false;
        }

        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }

        private byte[] convertImageToBytes()
        {
            FileStream fs = new FileStream(txtImg.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SP frm = new SP();
            frm.Show();
            this.Hide();
        }

        public void showcbb()
        {
            try
            {
                string sql = "Select * from LoaiSP";
                DataSet ds = new DataSet();
                ds = conn.getData(sql, "LoaiSP", null);
                cbbLSP.DataSource = ds.Tables["LoaiSP"];
                cbbLSP.DisplayMember = "TenLSP";
                cbbLSP.ValueMember = "TenLSP";

                string sql1 = "Select * from LoaiSP";
                DataSet ts = new DataSet();
                ts = conn.getData(sql1, "LoaiSP", null);
                cbbTK.DataSource = ts.Tables["LoaiSP"];
                cbbTK.DisplayMember = "TenLSP";
                cbbTK.ValueMember = "TenLSP";

                String query = "Select * from NhaCC";
                DataSet rs = new DataSet();
                rs = conn.getData(query, "NhaCC", null);
                cbbNCC.DataSource = rs.Tables["NhaCC"];
                cbbNCC.DisplayMember = "TenNhaCC";
                cbbNCC.ValueMember = "TenNhaCC";
            }
            catch (Exception ex)
            {
                MessageBox.Show("err : " + ex.Message);
            }
        }

        private void showData()
        {
            try
            {
                String sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,SoLuong,SoLuongHong,DonGia,GiaNhap,BaoHanh From SanPham";
                DataSet ds = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearData()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            cbbLSP.SelectedItem = null;
            cbbNCC.SelectedItem = null;
            txtSL.Text = "";
            txtSLH.Text = "";
            txtDG.Text = "";
            txtBH.Text = "";
            txtGN.Text = "";
            txtImg.Text = "";
            pictureBox1.Image = null;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tensp = txtTenSP.Text;
                string lsp = cbbLSP.SelectedValue.ToString();
                string ncc = cbbNCC.SelectedValue.ToString();
                string a = txtSL.Text;
                int sl = int.Parse(a);
                string b = txtSLH.Text;
                int slh = int.Parse(b);
                string dg = txtDG.Text;
                string bh = txtBH.Text;
                string gn = txtGN.Text;
                string text_file = txtImg.Text;
                string sql = "Insert into SanPham values(@anhsp,@fileanh,@tensp,@lsp,@ncc,@sl,@slh,@dg,@gn,@bh)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tensp", tensp));
                data.Add(new SqlParameter("@fileanh", text_file));
                data.Add(new SqlParameter("@lsp", lsp));
                data.Add(new SqlParameter("@ncc", ncc));
                data.Add(new SqlParameter("@sl", sl));
                data.Add(new SqlParameter("@slh", slh));
                data.Add(new SqlParameter("@dg", dg));
                data.Add(new SqlParameter("@gn", gn));
                data.Add(new SqlParameter("@bh", bh));
                data.Add(new SqlParameter("@anhsp", convertImageToBytes()));
                conn.Updatedata(sql, data);
                MessageBox.Show("Thêm mới thành công");
                showData();
                txtImg.Text = "";
                clearData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = openFileDialog.Filter = "JPG files (*.jpg)| *.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = openFileDialog.FileName;
                txtImg.Text = openFileDialog.FileName;

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string masp = txtMaSP.Text;
                string tensp = txtTenSP.Text;
                string lsp = cbbLSP.SelectedValue.ToString();
                string ncc = cbbNCC.SelectedValue.ToString();
                string a = txtSL.Text;
                int sl = int.Parse(a);
                string b = txtSLH.Text;
                int slh = int.Parse(b);
                string dg = txtDG.Text;
                string bh = txtBH.Text;
                string gn = txtGN.Text;
                string text_file = txtImg.Text;
                string sql = "Update SanPham set AnhSP = @anhsp,fileAnh = @fileanh,TenSP = @tensp,TenLSP = @lsp,TenNhaCC=@ncc,SoLuong = @sl,SoLuongHong = @slh,DonGia = @dg,GiaNhap = @gn,BaoHanh = @bh Where MaSP = @masp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", masp));
                data.Add(new SqlParameter("@tensp", tensp));
                data.Add(new SqlParameter("@fileanh", text_file));
                data.Add(new SqlParameter("@lsp", lsp));
                data.Add(new SqlParameter("@ncc", ncc));
                data.Add(new SqlParameter("@sl", sl));
                data.Add(new SqlParameter("@slh", slh));
                data.Add(new SqlParameter("@dg", dg));
                data.Add(new SqlParameter("@gn", gn));
                data.Add(new SqlParameter("@bh", bh));
                data.Add(new SqlParameter("@anhsp", convertImageToBytes()));
                conn.Updatedata(sql, data);
                MessageBox.Show("Sửa thành công");
                showData();
                txtImg.Text = "";
                clearData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String masp = txtMaSP.Text;
            try
            {
                string query = "DELETE FROM SanPham WHERE MaSP = @masp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", masp));
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
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    txtMaSP.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                    txtTenSP.Text = dataGridView1.Rows[index].Cells["TenSP"].Value.ToString();
                    cbbLSP.SelectedValue = dataGridView1.Rows[index].Cells["TenLSP"].Value.ToString();
                    cbbNCC.SelectedValue = dataGridView1.Rows[index].Cells["TenNhaCC"].Value.ToString();
                    txtSL.Text = dataGridView1.Rows[index].Cells["SoLuong"].Value.ToString();
                    txtSLH.Text = dataGridView1.Rows[index].Cells["SoLuongHong"].Value.ToString();
                    txtDG.Text = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                    txtGN.Text = dataGridView1.Rows[index].Cells["GiaNhap"].Value.ToString();
                    txtBH.Text = dataGridView1.Rows[index].Cells["BaoHanh"].Value.ToString();
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    string masp = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                    string sql = "Select * from SanPham Where MaSP = @masp";
                    List<SqlParameter> data = new List<SqlParameter>();
                    data.Add(new SqlParameter("@masp", masp));
                    DataSet ds = conn.getData(sql, "SanPham", data);
                    byte[] b = (byte[])ds.Tables["SanPham"].Rows[0]["AnhSP"];
                    pictureBox1.Image = ByteArrayToImage(b);
                    txtImg.Text = ds.Tables["SanPham"].Rows[0]["fileAnh"].ToString();
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    MessageBox.Show("Ko thấy");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = txtTK.Text; 
                string sql = "Select * From SanPham Where MaSP like '%'+@masp+'%' or TenSP like '%'+@tensp+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", tk));
                data.Add(new SqlParameter("@tensp", tk));
                DataSet ds = conn.getData(sql, "SanPham", data);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void cbbTK_SelectedValueChanged(object sender, EventArgs e)
        {
               
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cbbTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tk = cbbTK.SelectedValue.ToString();
                string sql = "Select * From SanPham Where TenLSP = @tenlsp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tenlsp", tk));
                DataSet ds = conn.getData(sql, "SanPham", data);
                dataGridView1.DataSource = ds.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }    
    }
}
