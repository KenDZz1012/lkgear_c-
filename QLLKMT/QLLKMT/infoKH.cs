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
    public partial class infoKH : Form
    {
        Connect conn = new Connect();
        public infoKH()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mahd = frmBanHang.getidHD();
            string sdt = txtSDT.Text;
            string email = txtEmail.Text;
            string sql = "Select Count(*) from KhachHang where SDT = @sdt and Email = @email";
            string sql1 = "select * from KhachHang where SDT = @sdt and Email = @email";
            List<SqlParameter> data = new List<SqlParameter>();
            data.Add(new SqlParameter("@sdt", sdt));
            data.Add(new SqlParameter("@email", email));
            List<SqlParameter> dta = new List<SqlParameter>();
            dta.Add(new SqlParameter("@sdt", sdt));
            dta.Add(new SqlParameter("@email", email));
            DataSet ds = conn.getData(sql1, "KhachHang", dta);
            int rs = (int)conn.CountData(sql, data);
            string sql6 = "select * from HoaDon where MaHD = @mahd";
            List<SqlParameter> x = new List<SqlParameter>();
            x.Add(new SqlParameter("@mahd", mahd));
            DataSet cs = conn.getData(sql6, "HoaDon", x);
            string tongtien = cs.Tables["HoaDon"].Rows[0]["TongTien"].ToString();
            if (rs == 1)
            {
                string makh = ds.Tables["KhachHang"].Rows[0]["MaKH"].ToString();
                MessageBox.Show("Tìm thấy 1 kết quả");
                string sql2 = "Update HoaDon set MaKH = @makh Where MaHD = @mahd";
                List<SqlParameter> dt = new List<SqlParameter>();
                dt.Add(new SqlParameter("@makh", makh));
                dt.Add(new SqlParameter("@mahd", mahd));
                conn.Updatedata(sql2, dt);
                string gtm = ds.Tables["KhachHang"].Rows[0]["GiaTriMua"].ToString();
                int gt = int.Parse(gtm);
                int tt = int.Parse(tongtien);
                int tong = gt + tt;
                string ttGtm = tong.ToString();     
                if(tong > 100000000)
                {
                    string loaikh = "VIP";
                    string sql7 = "Update KhachHang set GiaTriMua = @gtm,LoaiKH = @loaikh Where MaKH = @makh";
                    List<SqlParameter> y = new List<SqlParameter>();
                    y.Add(new SqlParameter("@gtm", ttGtm));
                    y.Add(new SqlParameter("@loaikh", loaikh));
                    y.Add(new SqlParameter("@makh", makh));
                    conn.Updatedata(sql7, y);
                }
                else
                {
                    string sql7 = "Update KhachHang set GiaTriMua = @gtm Where MaKH = @makh";
                    List<SqlParameter> y = new List<SqlParameter>();
                    y.Add(new SqlParameter("@gtm", ttGtm));
                    y.Add(new SqlParameter("@makh", makh));
                    conn.Updatedata(sql7, y);
                }
               
            }
            else
            {
                string tenkh = txtTen.Text;
                float tt = float.Parse(tongtien);
                string a = tt > 100000000 ? "VIP" : "Thường";
                string b = tongtien;
                string sql4 = "Insert into KhachHang values(@tenkh,@sdt,@email,@gt,@loaikh)";
                List<SqlParameter> dat = new List<SqlParameter>();
                dat.Add(new SqlParameter("@tenkh", tenkh));
                dat.Add(new SqlParameter("@sdt", sdt));
                dat.Add(new SqlParameter("@email", email));
                dat.Add(new SqlParameter("@gt", b));
                dat.Add(new SqlParameter("@loaikh", a));
                conn.Updatedata(sql4, dat);
                MessageBox.Show("Thêm mới thành công");
                string sql7 = "select MaKH from KhachHang where MaKH >= all (select MaKH from KhachHang)";
                DataSet ts = conn.getData(sql7, "KhachHang", null);
                string makh = ts.Tables["KhachHang"].Rows[0]["MaKH"].ToString();
                string sql5 = "Update HoaDon set MaKH = @makh Where MaHD = @mahd";
                List<SqlParameter> item = new List<SqlParameter>();
                item.Add(new SqlParameter("@makh", makh));
                item.Add(new SqlParameter("@mahd", mahd));
                conn.Updatedata(sql5, item);
            }
            this.Hide();
        }
    }
}
