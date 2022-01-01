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
    public partial class tkkh : Form
    {
        Connect conn = new Connect();
        public tkkh()
        {
            InitializeComponent();
        }

        private void showData()
        {
            try
            {
                string sql = "Select KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH,sum(CTHoaDon.Qty) as TongSL from HoaDon,CTHoaDon,KhachHang Where HoaDon.MaHD = CTHoaDon.MaHD and KhachHang.MaKH = HoaDon.MaKH group by KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH";
                DataSet ds = conn.getData(sql, "KhachHang", null);
                dataGridView1.DataSource = ds.Tables["KhachHang"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
        private void tkkh_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string ngaybd = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            string ngaykt = dateTimePicker2.Value.ToString("MM/dd/yyyy");
            string sql = "Select KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH,sum(CTHoaDon.Qty) as TongSL from HoaDon,CTHoaDon,KhachHang Where HoaDon.MaHD = CTHoaDon.MaHD and KhachHang.MaKH = HoaDon.MaKH and  NgayHD between @ngaybd and @ngaykt group by KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH";
            List<SqlParameter> data = new List<SqlParameter>();
            data.Add(new SqlParameter("@ngaybd", ngaybd));
            data.Add(new SqlParameter("@ngaykt", ngaykt));
            DataSet ds = conn.getData(sql, "KhachHang", data);
            dataGridView1.DataSource = ds.Tables["KhachHang"];
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
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
            string sql = "Select KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH,sum(CTHoaDon.Qty) as TongSL from HoaDon,CTHoaDon,KhachHang Where HoaDon.MaHD = CTHoaDon.MaHD and KhachHang.MaKH = HoaDon.MaKH and  MONTH(NgayHD) = @month group by KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH";
            List<SqlParameter> data = new List<SqlParameter>();
            data.Add(new SqlParameter("@month", t));
            DataSet ds = conn.getData(sql, "KhachHang", data);
            dataGridView1.DataSource = ds.Tables["KhachHang"];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string  loai = comboBox1.SelectedItem.ToString();
                string sql = "Select KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH,sum(CTHoaDon.Qty) as TongSL from HoaDon,CTHoaDon,KhachHang Where HoaDon.MaHD = CTHoaDon.MaHD and KhachHang.MaKH = HoaDon.MaKH and LoaiKH = @loai group by KhachHang.MaKH,KhachHang.TenKH,KhachHang.SDT,KhachHang.Email,KhachHang.GiaTriMua,KhachHang.LoaiKH";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@loai", loai));
                DataSet ds = conn.getData(sql, "KhachHang", data);
                dataGridView1.DataSource = ds.Tables["KhachHang"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BCTK frm = new BCTK();
            frm.Show();
            this.Hide();
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
            saveFileDialog.FileName = "Khách Hàng";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            app.Quit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chartkh frm = new chartkh();
            frm.ShowDialog();
        }
    }
}
