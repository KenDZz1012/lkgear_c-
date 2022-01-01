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
    public partial class chartdt : Form
    {
        Connect conn = new Connect();
        public chartdt()
        {
            InitializeComponent();
        }

        private void chartdt_Load(object sender, EventArgs e)
        {
            fillChart();
        }
        private void fillChart()
        {
            try
            {
                for(int i = 1; i <= 12; i++)
                {
                    string sql = "Select sum(TongTien*1) as Tong from HoaDon Where MONTH(NgayHD) = @month";
                    List<SqlParameter> dt = new List<SqlParameter>();
                    dt.Add(new SqlParameter("@month", i));
                    DataSet rs = conn.getData(sql, "HD", dt);
                    chart1.DataSource = rs;
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    chart1.Series["DoanhThu"].Points.AddXY("T" + i, rs.Tables["HD"].Rows[0]["Tong"].ToString());


                    chart2.DataSource = rs;
                    chart2.Series["DoanhThu"].Points.AddXY("T" + i, rs.Tables["HD"].Rows[0]["Tong"].ToString());

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
