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
using System.Globalization;

namespace QLLKMT
{
    public partial class frmBanHang : Form
    {
        Connect conn = new Connect();
        private static string idHD;
        public static string getidHD()
        {
            return idHD;
        }

        public static void setidHD(string idHD)
        {
            frmBanHang.idHD = idHD;
        }
        public frmBanHang()
        {
            InitializeComponent();
        }
        private void cleardata()
        {
            pictureBox1.Image = null;
            lbMaSP.Text = "MaSP";
            lbTenSP.Text = "TenSP";
            lbGia.Text = "Giá";
            numericUpDown1.Value = 1;
        }
        private void showcbb()
        {
            string sql = "Select * from LoaiSP";
            DataSet ds = new DataSet();
            ds = conn.getData(sql, "LoaiSP", null);
            cbbTK.DataSource = ds.Tables["LoaiSP"];
            cbbTK.DisplayMember = "TenLSP";
            cbbTK.ValueMember = "TenLSP";
        }
        private void frmBanHang_Load(object sender, EventArgs e)
        {

            button8.Enabled = false;
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            label3.Text = date;
            string role = Login.getRole();
            string name = Login.getName();
            string id = Login.getId();
            lbTenNV.Text = name;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BanHang frm = new BanHang();
            frm.Show();
            this.Hide();
        }
        private void showData()
        {
            try
            {
                String sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,DonGia,GiaNhap,BaoHanh from SanPham Where SoLuong>SoLuongHong";
                DataSet ds = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = ds.Tables["SanPham"];
                string mahd = lbMaHD.Text;
                string sql1 = "Select SanPham.MaSP, SanPham.TenSP, SanPham.DonGia, CTHoaDon.Qty, Sum(CTHoaDon.Qty*SanPham.DonGia) as 'ThanhTien'\n" +
                                "from HoaDon, CTHoaDon, SanPham\n" +
                                "where HoaDon.MaHD = CTHoaDon.MaHD and SanPham.MaSP = CTHoaDon.MaSP and HoaDon.MaHD = @mahd\n" +
                                "group by HoaDon.MaHD, SanPham.MaSP, SanPham.TenSP, SanPham.DonGia, CTHoaDon.Qty";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mahd", mahd));
                DataSet rs = conn.getData(sql1, "CTHoaDon", data);
                dataGridView2.DataSource = rs.Tables["CTHoaDon"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
        private void btnTHD_Click(object sender, EventArgs e)
        {
                button8.Enabled = true;
                showcbb();
                string id = Login.getId();
                lbMaNV.Text = id;
                string ngayhd = DateTime.Today.ToString("MM/dd/yyyy");
                string manv = id;
                int a = 0;
                string tt = "Chưa Thanh Toán";
                string sql1 = "Insert into HoaDon(MaNV,NgayHD,TongTien,TrangThai) values(@manv,@ngayhd,@tongtien,@trangthai)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@manv", manv));
                data.Add(new SqlParameter("@ngayhd", ngayhd));
                data.Add(new SqlParameter("@tongtien", a));
                data.Add(new SqlParameter("@trangthai", tt));
                conn.Updatedata(sql1, data);
                showData();
                string sql = "select MaHD from HoaDon where MaHD >= all (select MaHD from HoaDon)";
                DataSet rs = conn.getData(sql, "HoaDon", null);
                string mahd = rs.Tables["HoaDon"].Rows[0]["MaHD"].ToString();
                setidHD(mahd);
                lbMaHD.Text = mahd;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CultureInfo cul = new CultureInfo("vi-VN");
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
                    lbGia.Text = float.Parse(ds.Tables["SanPham"].Rows[0]["DonGia"].ToString()).ToString("#,###", cul.NumberFormat);
                    button2.Enabled = true;         
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
        public void getTT()
        {
            try
            {
                CultureInfo cul = new CultureInfo("vi-VN");
                String mahd = lbMaHD.Text;
                string sql = "Select * from HoaDon Where MaHD = @mahd ";
                DataSet rs = new DataSet();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", mahd));
                rs = conn.getData(sql, "HoaDon", data);
                string makh = rs.Tables["HoaDon"].Rows[0]["MaKH"].ToString();
                string sql2 = "Select Count(*) from KhachHang where MaKH = @makh";
                List<SqlParameter> dt = new List<SqlParameter>();
                dt.Add(new SqlParameter("@makh", makh));
                int cs = (int)conn.CountData(sql2, dt);
                if (cs == 1)
                {
                    string sql1 = "Select * from KhachHang where MaKH = @makh";
                    DataSet ds = new DataSet();
                    List<SqlParameter> dta = new List<SqlParameter>();
                    dta.Add(new SqlParameter("@makh", makh));
                    ds = conn.getData(sql1, "KhachHang", dta);
                    string lkh = ds.Tables["KhachHang"].Rows[0]["LoaiKH"].ToString();
                    if (lkh == "VIP")
                    {
                        double tt = float.Parse(rs.Tables["HoaDon"].Rows[0]["TongTien"].ToString());
                        double tongtien = tt - (tt * 10 / 100);
                        string a = tongtien.ToString("#,###", cul.NumberFormat);
                        lbTong.Text = a;
                        string tong = tongtien.ToString();
                        string sql3 = "Update HoaDon set TongTien=@tt where MaHD = @mahd";
                        List<SqlParameter> dat = new List<SqlParameter>();
                        dat.Add(new SqlParameter("@tt", tong));
                        dat.Add(new SqlParameter("@mahd", mahd));
                        conn.Updatedata(sql3, dat);

                    }
                    else if (lkh == "Thường")
                    {
                        string a = float.Parse(rs.Tables["HoaDon"].Rows[0]["TongTien"].ToString()).ToString("#,###", cul.NumberFormat);
                        lbTong.Text = a;
                    }
                }
                else
                {

                    string a = float.Parse(rs.Tables["HoaDon"].Rows[0]["TongTien"].ToString()).ToString("#,###", cul.NumberFormat);
                    lbTong.Text = a;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string mahd = lbMaHD.Text;
            string masp = lbMaSP.Text;
            int qty = Convert.ToInt32(numericUpDown1.Value);
            string sql = "Insert into CTHoaDon values(@mahd,@masp,@qty)";
            List<SqlParameter> data = new List<SqlParameter>();
            data.Add(new SqlParameter("@mahd", mahd));
            data.Add(new SqlParameter("@masp", masp));
            data.Add(new SqlParameter("@qty", qty));
            conn.Updatedata(sql, data);
            showData();
            getTT();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                lbMaSP.Text = dataGridView2.Rows[index].Cells["MaSP"].Value.ToString();
                lbTenSP.Text = dataGridView2.Rows[index].Cells["TenSP"].Value.ToString();
                string masp = dataGridView2.Rows[index].Cells["MaSP"].Value.ToString();
                string sql = "Select * from SanPham Where MaSP = @masp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", masp));
                DataSet ds = conn.getData(sql, "SanPham", data);
                byte[] b = (byte[])ds.Tables["SanPham"].Rows[0]["AnhSP"];
                pictureBox1.Image = ByteArrayToImage(b);
                lbGia.Text = ds.Tables["SanPham"].Rows[0]["DonGia"].ToString(); 
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void lbGia_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CultureInfo cul = new CultureInfo("vi-VN");
            int masp = int.Parse(lbMaSP.Text);
            string sql = "Select * from SanPham Where MaSP = @masp";
            List<SqlParameter> data = new List<SqlParameter>();
            data.Add(new SqlParameter("@masp", masp));
            DataSet ds = conn.getData(sql, "SanPham", data);
            double gia = Convert.ToDouble(ds.Tables["SanPham"].Rows[0]["DonGia"].ToString()) * Convert.ToInt32(numericUpDown1.Value);
            lbGia.Text = gia.ToString("#,###", cul.NumberFormat);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int masp = int.Parse(lbMaSP.Text);
                string mahd = lbMaHD.Text;
                string sql = "Delete from CTHoaDon Where MaHD=@mahd and MaSP = @masp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mahd", mahd));
                data.Add(new SqlParameter("@masp", masp));
                conn.Updatedata(sql, data);
                showData();
                getTT();
                cleardata();

            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int mahd = int.Parse(lbMaHD.Text);
                string masp = lbMaSP.Text;
                int qty = Convert.ToInt32(numericUpDown1.Value);
                string sql = "Update CTHoaDon set Qty = @qty where MaHD = @mahd and MaSP = @masp";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@qty", qty));
                data.Add(new SqlParameter("@mahd", mahd));
                data.Add(new SqlParameter("@masp", masp));
                conn.Updatedata(sql, data);
                showData();
                getTT();
                cleardata();


            }
            catch (Exception ex)
            {
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbbTK_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string tk = cbbTK.SelectedValue.ToString();
                string sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,DonGia,GiaNhap,BaoHanh from SanPham Where SoLuong>SoLuongHong and TenLSP = @tenlsp";
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select MaSP,TenSP,TenLSP,TenNhaCC,DonGia,GiaNhap,BaoHanh From SanPham Where MaSP like '%'+@masp+'%' or TenSP like '%'+@tensp+'%' and SoLuong>SoLuongHong";
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

        private void button5_Click(object sender, EventArgs e)
        {
            infoKH frm = new infoKH();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showData();
            getTT();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CultureInfo cul = new CultureInfo("vi-VN");
            int index = e.RowIndex;
            if (index >= 0)
            {
                lbMaSP.Text = dataGridView2.Rows[index].Cells["MaSP"].Value.ToString();
                lbTenSP.Text = dataGridView2.Rows[index].Cells["TenSP"].Value.ToString();
                string masp = dataGridView2.Rows[index].Cells["MaSP"].Value.ToString();
                string sql = "Select * from SanPham Where MaSP = @masp";
                numericUpDown1.Value = int.Parse(dataGridView2.Rows[index].Cells["Qty"].Value.ToString());
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@masp", masp));
                DataSet ds = conn.getData(sql, "SanPham", data);
                if(ds.Tables["SanPham"].Rows.Count <= 0)
                {
                    MessageBox.Show("Not found");
                }
                else
                {
                    byte[] b = (byte[])ds.Tables["SanPham"].Rows[0]["AnhSP"];
                    pictureBox1.Image = ByteArrayToImage(b);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    lbGia.Text = (float.Parse(ds.Tables["SanPham"].Rows[0]["DonGia"].ToString()) * float.Parse(numericUpDown1.Value.ToString())).ToString("#,###", cul.NumberFormat);
                }
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                MessageBox.Show("Not found");
            }    
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string mahd = lbMaHD.Text;
            buildCase frm = new buildCase();
            frm.ShowDialog();
            DialogResult rs = frm.ShowDialog();
            if(rs == DialogResult.Cancel)
            {
                showData();
                getTT();
            }    
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
