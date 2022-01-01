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
    public partial class frmSalary : Form
    {
        string[] mang = { "Mã Nhân Viên","Tên Nhân Viên","Chức Vụ","Lương/Ngày","Số Ngày Nghỉ","Tông Lương"};
        string[] data = { "MaNV", "TenNV", "TenChucVu", "Luong", "SoNgayLam" ,"TongLuong"};
        Connect conn = new Connect();
        public frmSalary()
        {
            InitializeComponent(); 
        }

        private void frmSalary_Load(object sender, EventArgs e)
        {
            showData();
        }
        private void showData()
        {
            try
            {
                String sql = "Select MaNV,TenNV,TenChucVu,Luong,SoNgayLam From NhanVien";
                DataSet ds = conn.getData(sql, "NhanVien", null);
                dataGridView1.DataSource = ds.Tables["NhanVien"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                string a = dataGridView1.Rows[i].Cells["Luong"].Value.ToString();
                int c = int.Parse(a);
                string b = dataGridView1.Rows[i].Cells["SoNgayLam"].Value.ToString();
                int d = int.Parse(b);
                int t = 0;
                string thang = cbbThang.SelectedItem.ToString();
                switch(thang)
                {
                    case "Tháng 1":
                        t = 31;
                        break;
                    case "Tháng 2":
                        t = 28;
                        break;
                    case "Tháng 3":
                        t = 31;
                        break;
                    case "Tháng 4":
                        t = 30;
                        break;
                    case "Tháng 5":
                        t = 31;
                        break;
                    case "Tháng 6":
                        t = 30;
                        break;
                    case "Tháng 7":
                        t = 31;
                        break;
                    case "Tháng 8":
                        t = 31;
                        break;
                    case "Tháng 9":
                        t = 30;
                        break;
                    case "Tháng 10":
                        t = 31;
                        break;
                    case "Tháng 11":
                        t = 30;
                        break;
                    case "Tháng 12":
                        t = 31;
                        break;
                }    
                int g = c * (t-d);
                dataGridView1.Rows[i].Cells["TongLuong"].Value = g;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Salary";
            //for(int i = 1; i < dataGridView1.Columns.Count+1; i++)
            //{
            //    worksheet.Cells[1, i] = dataGridView1.Columns[i-1].HeaderText;
            //    Console.WriteLine(dataGridView1.Columns[0].HeaderText);
            //}
            for(int i = 1; i < mang.Length + 1; i++)
            {
                worksheet.Cells[1, i] = mang[i - 1];
            }    

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[data[j]].Value;
                }
            }
            worksheet.Columns.AutoFit();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Salary";
            saveFileDialog.DefaultExt = ".xlsx";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            app.Quit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int snl = 0;
                string manv = dataGridView1.Rows[i].Cells["MaNV"].Value.ToString();
                string sql = "UPDATE NhanVien set SoNgayLam = @snl Where MaNV = @manv";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@snl", snl));
                data.Add(new SqlParameter("@manv", manv));
                conn.Updatedata(sql, data);
                showData();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NV frm = new NV();
            frm.Show();
            this.Hide();
        }
    }
}
