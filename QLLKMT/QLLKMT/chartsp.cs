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
    public partial class chartsp : Form
    {
        Connect conn = new Connect();
        public chartsp()
        {
            InitializeComponent();
        }

        private void chartsp_Load(object sender, EventArgs e)
        {
        }
        private void fillChart()
        {
            try
            {
                for (int i = 1; i <= 12; i++)
                {
                    string sql = "Select CTHoaDon.MaSP, SanPham.TenSP,SanPham.TenLSP,SanPham.TenNhaCC,SanPham.DonGia,SanPham.GiaNhap,sum(CTHoaDon.Qty) as TongSoLuong,sum(CTHoaDon.Qty)*SanPham.DonGia as TongGiaTri \n" +
                                           "from CTHoaDon,HoaDon,SanPham\n" +
                                           "where HoaDon.MaHD = CTHoaDon.MaHD and SanPham.MaSP = CTHoaDon.MaSP and  MONTH(NgayHD) = @month\n" +
                                           "group by CTHoaDon.MaSP ,  SanPham.TenSP,SanPham.TenLSP,SanPham.TenNhaCC,SanPham.DonGia,SanPham.GiaNhap order by TongSoLuong DESC"; List<SqlParameter> dt = new List<SqlParameter>();
                    dt.Add(new SqlParameter("@month", i));
                    DataSet rs = conn.getData(sql, "HD", dt);
                    chart1.DataSource = rs;
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    chart1.Series["Sản Phẩm"].Points.AddXY(rs.Tables["HD"].Rows[0]["SanPham.TenSP"].ToString(), rs.Tables["HD"].Rows[0]["SanPham.TenSP"].ToString());


                    chart2.DataSource = rs;
                    chart1.Series["Sản Phẩm"].Points.AddXY("T" + i, rs.Tables["HD"].Rows[0]["SanPham.TenSP"].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void clearData()
        {
            try
            {
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                string sql = "Select CTHoaDon.MaSP, SanPham.TenSP,SanPham.TenLSP,SanPham.TenNhaCC,SanPham.DonGia,SanPham.GiaNhap,sum(CTHoaDon.Qty) as TongSoLuong,sum(CTHoaDon.Qty)*SanPham.DonGia as TongGiaTri \n" +
                        "from CTHoaDon,HoaDon,SanPham\n" +
                        "where HoaDon.MaHD = CTHoaDon.MaHD and SanPham.MaSP = CTHoaDon.MaSP and  MONTH(NgayHD) = @month\n" +
                        "group by CTHoaDon.MaSP ,  SanPham.TenSP,SanPham.TenLSP,SanPham.TenNhaCC,SanPham.DonGia,SanPham.GiaNhap order by TongSoLuong DESC";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@month", t));
                DataSet rs = conn.getData(sql, "SanPham", data);
                chart1.DataSource = rs;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                for(int i = 0; i < rs.Tables["SanPham"].Rows.Count;i++)
                {
                    chart1.Series["Sản Phẩm"].Points.AddXY(rs.Tables["SanPham"].Rows[i]["TenSP"].ToString(), rs.Tables["SanPham"].Rows[i]["TongSoLuong"].ToString());

                    chart2.DataSource = rs;
                    chart2.Series["Sản Phẩm"].Points.AddXY(rs.Tables["SanPham"].Rows[i]["TenSP"].ToString(), rs.Tables["SanPham"].Rows[i]["TongSoLuong"].ToString());
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
    }
}
