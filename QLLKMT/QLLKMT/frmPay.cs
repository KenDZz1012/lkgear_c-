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
    public partial class frmPay : Form
    {
        Connect conn = new Connect();
        String mahd;
        public frmPay()
        {
            InitializeComponent();
        }
        public frmPay(String MaHD)
        {
            InitializeComponent();
            this.MaHD = MaHD;
        }
        public string MaHD { get => mahd; set => mahd = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("Bạn Chắc Chắn Muốn Thanh Toán ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                string sql = "Update HoaDon set TrangThai = 'Đã Thanh Toán' Where MaHD = @mahd";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mahd", MaHD));
                conn.Updatedata(sql,data);
                MessageBox.Show("Thanh Toán Thành Công !!!");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
        private void showData()
        {
            try
            {
                CultureInfo cul = new CultureInfo("vi-VN");
                string sql = "Select HoaDon.NgayHD,HoaDon.TongTien,NhanVien.MaNV,NhanVien.TenNV,KhachHang.TenKH,KhachHang.SDT from HoaDon,NhanVien,KhachHang Where HoaDon.MaNV = NhanVien.MaNV and HoaDon.MaKH = KhachHang.MaKh and HoaDon.MaHD = @mahd group by HoaDon.NgayHD,NhanVien.MaNV,NhanVien.TenNV,KhachHang.TenKH,KhachHang.SDT,HoaDon.TongTien";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mahd", MaHD));
                DataSet ds = conn.getData(sql,"HoaDon",data);
                lbNgayHD.Text = ds.Tables["HoaDon"].Rows[0]["NgayHD"].ToString();
                lbMaNV.Text = ds.Tables["HoaDon"].Rows[0]["MaNV"].ToString();
                lbTenNV.Text = ds.Tables["HoaDon"].Rows[0]["TenNV"].ToString();
                lbTenKH.Text = ds.Tables["HoaDon"].Rows[0]["TenKH"].ToString();
                lbSDT.Text = ds.Tables["HoaDon"].Rows[0]["SDT"].ToString();
                lbTongTien.Text = int.Parse(ds.Tables["HoaDon"].Rows[0]["TongTien"].ToString()).ToString("#,###", cul.NumberFormat);
                lbMaHD.Text = MaHD;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
        private void showSP()
        {
            try
            {
                string sql1 = "Select SanPham.MaSP, SanPham.TenSP, SanPham.DonGia, CTHoaDon.Qty, Sum(CTHoaDon.Qty*SanPham.DonGia) as 'ThanhTien'\n" +
                                "from HoaDon, CTHoaDon, SanPham\n" +
                                "where HoaDon.MaHD = CTHoaDon.MaHD and SanPham.MaSP = CTHoaDon.MaSP and HoaDon.MaHD = @mahd\n" +
                                "group by HoaDon.MaHD, SanPham.MaSP, SanPham.TenSP, SanPham.DonGia, CTHoaDon.Qty";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@mahd", MaHD));
                DataSet rs = conn.getData(sql1, "CTHoaDon", data);
                dataGridView1.DataSource = rs.Tables["CTHoaDon"];
            }
            catch(Exception ex)
            {

            }
        }
        private void frmPay_Load(object sender, EventArgs e)
        {
            showData();
            showSP();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HoaDon frm = new HoaDon();
            frm.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
