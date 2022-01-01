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
    public partial class Register : Form
    {
        Connect conn = new Connect();
        public Register()
        {
            InitializeComponent();
            showcbb();
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
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
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


        private void btnDangKi_Click(object sender, EventArgs e)
        {
            try
            {
                string sql1 = "select count(*) as NV from NhanVien";
                DataSet ds = conn.getData(sql1, "TongNV", null);
                int b = Int32.Parse(ds.Tables["TongNV"].Rows[0]["NV"].ToString());
                b += 1;
                string manv = "";
                manv = b < 10 ? "NV0" + b : "NV" + b;
                string tennv = txtTenNV.Text;
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
                string tk = txtTK.Text;
                string mk = txtMK.Text;
                string re_mk = txtmka.Text;
                string text_file = txtImg.Text;
                string sql = "Insert into NhanVien values(@manv,@tennv,@avatar,@fileanh,@chucvu,@gioitinh,@ngsinh,@cmnd,@sdt,@luong,@tk,@mk)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@manv", manv));
                data.Add(new SqlParameter("@tennv", tennv));
                data.Add(new SqlParameter("@fileanh", text_file));
                data.Add(new SqlParameter("@chucvu",cv));
                data.Add(new SqlParameter("@gioitinh", gt));
                data.Add(new SqlParameter("@ngsinh", NgaySinh));
                data.Add(new SqlParameter("@cmnd", cmnd));
                data.Add(new SqlParameter("@sdt", sdt));
                data.Add(new SqlParameter("@luong", ""));
                data.Add(new SqlParameter("@tk", tk));
                data.Add(new SqlParameter("@mk", mk));
                data.Add(new SqlParameter("@avatar", convertImageToBytes()));
                if (tennv.Length == 0 || cmnd.Length == 0 || sdt.Length == 0 || tk.Length == 0 || mk.Length == 0 || text_file.Length == 0)
                {
                    MessageBox.Show("Hãy điền đủ thông tin");
                }
                else if(mk != re_mk)
                {
                    MessageBox.Show("Mật Khẩu Không Trùng Khớp");
                }
                else
                {
                    conn.Updatedata(sql, data);
                    MessageBox.Show("Đăng Ký Tài Khoản thành công");
                    txtImg.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        private void btnAnh_Click_1(object sender, EventArgs e)
        {
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
    }
}
