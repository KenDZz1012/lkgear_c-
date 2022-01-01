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
    public partial class DSTD : Form
    {
        Connect conn = new Connect();
        public DSTD()
        {
            InitializeComponent();
        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void showData()
        {
            try
            {
                String sql = "Select MaTD,TenTD,TenChucVu,GioiTinh,NgSinh,CMND,SDT,GioiThieu From TuyenDung";
                DataSet ds = conn.getData(sql, "TuyenDung", null);
                dataGridView1.DataSource = ds.Tables["TuyenDung"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DSTD_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;

                if (index >= 0)
                {
                    lbMa.Text = dataGridView1.Rows[index].Cells["MaTD"].Value.ToString();
                    lbTen.Text = dataGridView1.Rows[index].Cells["TenTD"].Value.ToString();
                    lbSDT.Text = dataGridView1.Rows[index].Cells["SDT"].Value.ToString();
                    lbCV.Text = dataGridView1.Rows[index].Cells["TenChucVu"].Value.ToString();
                    lbGT.Text = dataGridView1.Rows[index].Cells["GioiTinh"].Value.ToString();
                    string ngsinh = dataGridView1.Rows[index].Cells["NgSinh"].Value.ToString();
                    lbNgSinh.Text = ngsinh;
                    lbCMND.Text = dataGridView1.Rows[index].Cells["CMND"].Value.ToString();
                    richTextBox1.Text = dataGridView1.Rows[index].Cells["GioiThieu"].Value.ToString();
                    string ma = dataGridView1.Rows[index].Cells["MaTD"].Value.ToString();
                    string sql = "Select * from TuyenDung Where MaTD = @ma";
                    List<SqlParameter> data = new List<SqlParameter>();
                    data.Add(new SqlParameter("@ma", ma));
                    DataSet ds = conn.getData(sql, "TuyenDung", data);
                    byte[] b = (byte[])ds.Tables["TuyenDung"].Rows[0]["Avatar"];
                    pictureBox1.Image = ByteArrayToImage(b);
                    lbTextImg.Text = ds.Tables["TuyenDung"].Rows[0]["fileAnh"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] convertImageToBytes()
        {
            FileStream fs = new FileStream(lbTextImg.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                string matd = lbMa.Text;
                string sql = "Delete from TuyenDung Where MaTD = @matd";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@matd", matd));
                conn.Updatedata(sql, data);
                showData();

                string ten = lbTen.Text;
                string sdt = lbSDT.Text;
                string ngsinh = lbNgSinh.Text;
                string cv = lbCV.Text;
                string fa = lbTextImg.Text;
                string cmnd = lbCMND.Text;
                string gt = lbGT.Text; 
                string sql1 = "Insert into NhanVien(TenNV,Avatar,fileAnh,TenChucVu,GioiTinh,NgSinh,CMND,SDT) values(@ten,@avatar,@fileAnh,@cv,@gt,@ngsinh,@cmnd,@sdt)";
                List<SqlParameter> dta = new List<SqlParameter>();
                dta.Add(new SqlParameter("@ten", ten));
                dta.Add(new SqlParameter("@fileanh", fa));
                dta.Add(new SqlParameter("@gt", gt));
                dta.Add(new SqlParameter("@cv", cv));
                dta.Add(new SqlParameter("@ngsinh", ngsinh));
                dta.Add(new SqlParameter("@cmnd", cmnd));
                dta.Add(new SqlParameter("@sdt", sdt));
                dta.Add(new SqlParameter("@avatar", convertImageToBytes()));
                conn.Updatedata(sql1, dta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string matd = lbMa.Text;
                string sql = "Delete from TuyenDung Where MaTD = @matd";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@matd", matd));
                conn.Updatedata(sql,data);
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            saveFileDialog.FileName = "Tuyển Dụng";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            app.Quit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NV frm = new NV();
            frm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
