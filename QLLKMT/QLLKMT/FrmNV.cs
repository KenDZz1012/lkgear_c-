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
    public partial class FrmNV : Form
    {
        Connect conn = new Connect();
        public FrmNV()
        {
            InitializeComponent();
            showData();
            showcbb();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNV.Enabled = false;
        }
        string role = Login.getRole();
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

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = openFileDialog.Filter = "JPG files (*.jpg)| *.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = openFileDialog.FileName;
                txtImg.Text = openFileDialog.FileName;

            }
        }
        private void showData()
        {
            try
            {
                String sql = "Select MaNV,TenNV,TenChucVu,GioiTinh,NgSinh,CMND,SDT,Luong,SoNgayLam,TaiKhoan,MatKhau From NhanVien";
                DataSet ds = conn.getData(sql, "NhanVien", null);
                dataGridView1.DataSource = ds.Tables["NhanVien"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearData()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            cbbCV.SelectedItem = null;
            txtCMND.Text = "";
            txtSDT.Text = "";
            txtLuong.Text = "";
            txtTK.Text = "";
            txtMK.Text = "";
            txtImg.Text = "";
            txtNgayLam.Text = "";
            dtNgSinh.Text = "";
            rdBtnNam.Checked = false;
            rdBtnNu.Checked = false;
            pictureBox1.Image = null;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
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
                string luong = txtLuong.Text;
                string tk = txtTK.Text;
                string mk = txtMK.Text;
                string text_file = txtImg.Text;
                int ngayLam = 0;
                string sql = "Insert into NhanVien values(@tennv,@avatar,@fileanh,@chucvu,@gioitinh,@ngsinh,@cmnd,@sdt,@luong,@ngaylam,@tk,@mk)";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tennv", tennv));
                data.Add(new SqlParameter("@fileanh", text_file));
                data.Add(new SqlParameter("@chucvu", cv));
                data.Add(new SqlParameter("@gioitinh", gt));
                data.Add(new SqlParameter("@ngsinh", NgaySinh));
                data.Add(new SqlParameter("@cmnd", cmnd));
                data.Add(new SqlParameter("@sdt", sdt));
                data.Add(new SqlParameter("@luong", luong));
                data.Add(new SqlParameter("@ngaylam", ngayLam));
                data.Add(new SqlParameter("@tk", tk));
                data.Add(new SqlParameter("@mk", mk));
                data.Add(new SqlParameter("@avatar", convertImageToBytes()));
                if(tennv.Length==0 || cmnd.Length==0 || sdt.Length==0||luong.Length==0||tk.Length==0||mk.Length==0||text_file.Length==0)
                {
                    MessageBox.Show("Hãy điền đủ thông tin");
                }
                else
                {
                    conn.Updatedata(sql, data);
                    MessageBox.Show("Thêm mới thành công");
                    showData();
                    txtImg.Text = "";
                    clearData();
                }                              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Enabled = false;
                string manv = txtMaNV.Text;
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
                string luong = txtLuong.Text;
                string ngay = txtNgayLam.Text;
                string tk = txtTK.Text;
                string mk = txtMK.Text;
                string text_file = txtImg.Text;
                string sql = "UPDATE NhanVien set  TenNV = @tennv, Avatar = @avatar,fileAnh = @fileanh,TenChucVu = @chucvu,GioiTinh = @gioitinh,NgSinh = @ngsinh,CMND = @cmnd,SDT = @sdt,Luong = @luong,SoNgayLam=@ngaylam ,TaiKhoan = @tk,MatKhau =@mk where MaNV = @manv";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@manv", manv));
                data.Add(new SqlParameter("@tennv", tennv));
                data.Add(new SqlParameter("@fileanh", text_file));
                data.Add(new SqlParameter("@chucvu", cv));
                data.Add(new SqlParameter("@gioitinh", gt));
                data.Add(new SqlParameter("@ngsinh", NgaySinh));
                data.Add(new SqlParameter("@cmnd", cmnd));
                data.Add(new SqlParameter("@sdt", sdt));
                data.Add(new SqlParameter("@luong", luong));
                data.Add(new SqlParameter("@ngaylam", ngay));
                data.Add(new SqlParameter("@tk", tk));
                data.Add(new SqlParameter("@mk", mk));
                data.Add(new SqlParameter("@avatar", convertImageToBytes()));
                conn.Updatedata(sql, data);
                MessageBox.Show("Sửa thành công!");
                showData();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0)
            {
                txtMaNV.Text = dataGridView1.Rows[index].Cells["MaNV"].Value.ToString();
                txtTenNV.Text = dataGridView1.Rows[index].Cells["TenNV"].Value.ToString();
                cbbCV.SelectedValue = dataGridView1.Rows[index].Cells["TenChucVu"].Value.ToString();
                txtCMND.Text = dataGridView1.Rows[index].Cells["CMND"].Value.ToString();
                txtSDT.Text = dataGridView1.Rows[index].Cells["SDT"].Value.ToString();
                txtLuong.Text = dataGridView1.Rows[index].Cells["Luong"].Value.ToString();
                txtNgayLam.Text = dataGridView1.Rows[index].Cells["SoNgayLam"].Value.ToString();
                txtTK.Text = dataGridView1.Rows[index].Cells["TaiKhoan"].Value.ToString();
                txtMK.Text = dataGridView1.Rows[index].Cells["MatKhau"].Value.ToString();
                dtNgSinh.Text = dataGridView1.Rows[index].Cells["NgSinh"].Value.ToString();
                if(dataGridView1.Rows[index].Cells["GioiTinh"].Value.ToString() == "Nam")
                {
                    rdBtnNam.Checked = true;
                    rdBtnNu.Checked = false;
                }
                else
                {
                    rdBtnNam.Checked = false;
                    rdBtnNu.Checked = true;
                }
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                string manv = dataGridView1.Rows[index].Cells["MaNV"].Value.ToString();
                string sql = "Select * from NhanVien Where MaNV = @manv";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@manv",manv));
                DataSet ds = conn.getData(sql, "NhanVien", data);
                byte[] b = (byte[])ds.Tables["NhanVien"].Rows[0]["Avatar"];
                pictureBox1.Image = ByteArrayToImage(b);
                txtImg.Text = ds.Tables["NhanVien"].Rows[0]["fileAnh"].ToString();
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String manv = txtMaNV.Text;
            try
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @manv";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@manv", manv));
                conn.Updatedata(query, data);
                MessageBox.Show("Xóa thành công");
                showData();
                clearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NV frm = new NV();
            frm.Show();
            this.Hide();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearData();

        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = textBox1.Text;
                string sql = "Select * From NhanVien Where TenNV like '%'+@tennv+'%'";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tennv", tk));
                DataSet ds = conn.getData(sql, "NhanVien", data);
                dataGridView1.DataSource = ds.Tables["NhanVien"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmNV_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtImg_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
