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
    public partial class tkdt : Form
    {
        Connect conn = new Connect();
        public tkdt()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                String sql = "Select * from HoaDon Where TrangThai='Đã Thanh Toán'";
                DataSet ds = conn.getData(sql, "HoaDon" ,null);
                dataGridView1.DataSource = ds.Tables["HoaDon"];
                string sql1 = "Select sum(TongTien*1) as Tong from HoaDon Where TrangThai='Đã Thanh Toán'";
                DataSet rs = conn.getData(sql1, "HD", null);
                double sum = 0;
                if (double.TryParse(rs.Tables["HD"].Rows[0]["Tong"].ToString(), out sum))
                {
                    sum = int.Parse(rs.Tables["HD"].Rows[0]["Tong"].ToString());
                }
                lbTong.Text = sum.ToString();
                string sql2 = "Select sum(TongTienNhap*1) as Tong from HoaDon Where TrangThai='Đã Thanh Toán'";
                DataSet ts = conn.getData(sql2, "HD", null);
                double sumGn = 0;
                if (double.TryParse(ts.Tables["HD"].Rows[0]["Tong"].ToString(), out sumGn))
                {
                    sumGn = double.Parse(ts.Tables["HD"].Rows[0]["Tong"].ToString());
                }
                lbGiaNhap.Text = sumGn.ToString();

                lbTDT.Text = (sum - sumGn).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi"+ex);
            }
        }
        private void Tong()
        {
            long sum = 0;
            for(int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                long t = Convert.ToInt64(dataGridView1.Rows[i].Cells["TongTien"].Value);
                sum += t;
            }
            Console.WriteLine(sum);
            lbTong.Text = sum.ToString();
        }

        private void tkdt_Load(object sender, EventArgs e)
        {
            showData();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ngaybd = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                string ngaykt = dateTimePicker2.Value.ToString("MM/dd/yyyy");
                string sql = "Select * From HoaDon Where TrangThai='Đã Thanh Toán' and  NgayHD between @ngaybd and @ngaykt ";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@ngaybd", ngaybd));
                data.Add(new SqlParameter("@ngaykt", ngaykt));
                DataSet ds = conn.getData(sql, "HoaDon", data);
                dataGridView1.DataSource = ds.Tables["HoaDon"];
                string sql1 = "Select sum(TongTien*1) as Tong from HoaDon Where TrangThai='Đã Thanh Toán' and NgayHD between @ngaybd and @ngaykt";
                List<SqlParameter> dt = new List<SqlParameter>();
                dt.Add(new SqlParameter("@ngaybd", ngaybd));
                dt.Add(new SqlParameter("@ngaykt", ngaykt));
                DataSet rs = conn.getData(sql1, "HD", dt);
                double sum = 0;
                if (double.TryParse(rs.Tables["HD"].Rows[0]["Tong"].ToString(), out sum))
                {
                    sum = int.Parse(rs.Tables["HD"].Rows[0]["Tong"].ToString());
                }
                lbTong.Text = sum.ToString();
                string sql2 = "Select sum(TongTienNhap*1) as Tong from HoaDon Where TrangThai='Đã Thanh Toán' and NgayHD between @ngaybd and @ngaykt";
                List<SqlParameter> ct = new List<SqlParameter>();
                ct.Add(new SqlParameter("@ngaybd", ngaybd));
                ct.Add(new SqlParameter("@ngaykt", ngaykt));
                DataSet ts = conn.getData(sql2, "HD", ct);
                double sumGn = 0;
                if (double.TryParse(ts.Tables["HD"].Rows[0]["Tong"].ToString(), out sumGn))
                {
                    sumGn = double.Parse(ts.Tables["HD"].Rows[0]["Tong"].ToString());
                }
                lbGiaNhap.Text = sumGn.ToString();

                lbTDT.Text = (sum - sumGn).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string ngaybd = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            //    string ngaykt = dateTimePicker2.Value.ToString("MM/dd/yyyy");
            //    string sql = "Select * From HoaDon Where NgayHD between @ngaybd and @ngaykt";
            //    List<SqlParameter> data = new List<SqlParameter>();
            //    data.Add(new SqlParameter("@ngaybd", ngaybd));
            //    data.Add(new SqlParameter("@ngaykt", ngaykt));
            //    DataSet ds = conn.getData(sql, "HoaDon", data);
            //    dataGridView1.DataSource = ds.Tables["HoaDon"];
            //    string sql1 = "Select sum(TongTien*1) as Tong from HoaDon Where NgayHD between @ngaybd and @ngaykt";
            //    List<SqlParameter> dt = new List<SqlParameter>();
            //    dt.Add(new SqlParameter("@ngaybd", ngaybd));
            //    dt.Add(new SqlParameter("@ngaykt", ngaykt));
            //    DataSet rs = conn.getData(sql1, "HD", dt);
            //    int sum = int.Parse(rs.Tables["HD"].Rows[0]["Tong"].ToString());
            //    lbTong.Text = sum.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi" + ex);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Salary";
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                Console.WriteLine(dataGridView1.Columns[0].HeaderText);
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            worksheet.Columns.AutoFit();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "DoanhThu";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            app.Quit();
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
                string sql = "Select * from HoaDon where MONTH(NgayHD) = @month";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@month", t));
                DataSet ds = conn.getData(sql, "HoaDon", data);
                dataGridView1.DataSource = ds.Tables["HoaDon"];
                string sql1 = "Select sum(TongTien*1) as Tong from HoaDon Where MONTH(NgayHD) = @month and TrangThai='Đã Thanh Toán'";
                List<SqlParameter> dt = new List<SqlParameter>();
                dt.Add(new SqlParameter("@month", t));
                DataSet rs = conn.getData(sql1, "HD", dt);
                double sum = 0;
                if (double.TryParse(rs.Tables["HD"].Rows[0]["Tong"].ToString(), out sum))
                {
                    sum = double.Parse(rs.Tables["HD"].Rows[0]["Tong"].ToString());
                }
                lbTong.Text = sum.ToString();
                string sql2 = "Select sum(TongTienNhap*1) as Tong from HoaDon Where MONTH(NgayHD) = @month and TrangThai='Đã Thanh Toán'";
                List<SqlParameter> ct = new List<SqlParameter>();
                ct.Add(new SqlParameter("@month", t));
                DataSet ts = conn.getData(sql2, "HD", ct);
                double sumGn = 0;
                if (double.TryParse(ts.Tables["HD"].Rows[0]["Tong"].ToString(), out sumGn))
                {
                    sumGn = double.Parse(ts.Tables["HD"].Rows[0]["Tong"].ToString());
                }
                lbGiaNhap.Text = sumGn.ToString();
                lbTDT.Text = (sum - sumGn).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi"+ex);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BCTK frm = new BCTK();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chartdt frm = new chartdt();
            frm.ShowDialog();
        }
    }
}
