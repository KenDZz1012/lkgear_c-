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
    public partial class chartkh : Form
    {
        Connect conn = new Connect();
        public chartkh()
        {
            InitializeComponent();
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.DataSource = null;
                chart1.Series[0].Points.Clear();
                chart2.DataSource = null;
                chart2.Series[0].Points.Clear();
                int t = 0;
                string thang = cbbThang.SelectedItem.ToString();
                switch (thang)
                {
                    case "Tháng 1":
                        t = 1;
                        break;
                    case "Tháng 2":
                        t = 2;
                        break;
                    case "Tháng 3":
                        t = 3;
                        break;
                    case "Tháng 4":
                        t = 4;
                        break;
                    case "Tháng 5":
                        t = 5;
                        break;
                    case "Tháng 6":
                        t = 6;
                        break;
                    case "Tháng 7":
                        t = 7;
                        break;
                    case "Tháng 8":
                        t = 8;
                        break;
                    case "Tháng 9":
                        t = 9;
                        break;
                    case "Tháng 10":
                        t = 10;
                        break;
                    case "Tháng 11":
                        t = 11;
                        break;
                    case "Tháng 12":
                        t = 12;
                        break;
                }
                string sql = "Select KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH,sum(CTHoaDon.Qty) as TongSL from HoaDon,CTHoaDon,KhachHang Where HoaDon.MaHD = CTHoaDon.MaHD and KhachHang.MaKH = HoaDon.MaKH and  MONTH(NgayHD) = @month group by KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@month", t));
                DataSet rs = conn.getData(sql, "SanPham", data);
                chart1.DataSource = rs;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                for (int i = 0; i < rs.Tables["SanPham"].Rows.Count; i++)
                {
                    chart1.Series["Sản Phẩm"].Points.AddXY(rs.Tables["SanPham"].Rows[i]["TenKH"].ToString(), rs.Tables["SanPham"].Rows[i]["GiaTriMua"].ToString());

                    chart2.DataSource = rs;
                    chart2.Series["Sản Phẩm"].Points.AddXY(rs.Tables["SanPham"].Rows[i]["TenKH"].ToString(), rs.Tables["SanPham"].Rows[i]["GiaTriMua"].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
    }
}
