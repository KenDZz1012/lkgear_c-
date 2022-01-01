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
    public partial class frmTD : Form
    {
        Connect conn = new Connect();
        public frmTD()
        {
            InitializeComponent();
        }
        private byte[] convertImageToBytes()
        {
            FileStream fs = new FileStream(txtImg.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = openFileDialog.Filter = "JPG files (*.jpg)| *.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
                txtImg.Text = openFileDialog.FileName;

            }
        }
        public void showcbb()
        {
            try
            {
                string sql = "Select * from ChucVu";
                DataSet ds = new DataSet();
                ds = conn.getData(sql, "ChucVu", null);
                cbbCV.DataSource = ds.Tables["ChucVu"];
                cbbCV.DisplayMember = "TenChucVu";
                cbbCV.ValueMember = "TenChucVu";

            }
            catch (Exception ex)
            {
                MessageBox.Show("err : " + ex.Message);
            }
        }

        private void frmTD_Load(object sender, EventArgs e)
        {
            showcbb();
        }
        private void clearData()
        {
            txtTen.Text = "";
            cbbCV.SelectedItem = null;
            txtCMND.Text = "";
            txtSDT.Text = "";
            txtImg.Text = "";
            dtNgSinh.Text = "";
            rdBtnNam.Checked = false;
            rdBtnNu.Checked = false;
            pictureBox1.Image = null;
            richTextBox1.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string tennv = txtTen.Text;
                string cv = cbbCV.SelectedValue.ToString();
                string gt = "";
                if (rdBtnNam.Checked)
                {
                    gt = "Nam";
                }
                else
                {
                    gt = "Nữ";
                }
                DateTime NgaySinh = DateTime.Parse(dtNgSinh.Value.ToString());
                string cmnd = txtCMND.Text;
                string sdt = txtSDT.Text;
                string text_file = txtImg.Text;
                string gioithieu = richTextBox1.Text;
                string sql = "Insert into TuyenDung values(@ten,@chucvu,@gioitinh,@ngsinh,@cmnd,@sdt,@gt,@avatar,@fileanh)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@ten", tennv));
                data.Add(new SqlParameter("@fileanh", text_file));
                data.Add(new SqlParameter("@chucvu", cv));
                data.Add(new SqlParameter("@gioitinh", gt));
                data.Add(new SqlParameter("@ngsinh", NgaySinh));
                data.Add(new SqlParameter("@cmnd", cmnd));
                data.Add(new SqlParameter("@sdt", sdt));
                data.Add(new SqlParameter("@gt", gioithieu));
                data.Add(new SqlParameter("@avatar", convertImageToBytes()));
                if (tennv.Length == 0 || cmnd.Length == 0 || sdt.Length == 0  || text_file.Length == 0)
                {
                    MessageBox.Show("Hãy điền đủ thông tin");
                }
                else
                {
                    conn.Updatedata(sql, data);
                    MessageBox.Show("Thêm mới thành công");
                    txtImg.Text = "";
                    clearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NV frm = new NV();
            frm.Show();
            this.Hide();
        }
    }
}
