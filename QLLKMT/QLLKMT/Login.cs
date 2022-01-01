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
    public partial class Login : Form
    {
        Connect conn = new Connect();
        public Login()
        {
            InitializeComponent();
        }
        private static string role;
        private static string name;
        private static string id;
        public static string getRole()
        {
            return role;
        }

        public static void setRole(string role)
        {
            Login.role = role;
        }
        public static string getName()
        {
            return name;
        }

        public static void setName(string name)
        {
            Login.name= name;
        }
        public static string getId()
        {
            return id;
        }

        public static void setId(string id)
        {
            Login.id = id;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = txtTaiKhoan.Text;
                string mk = txtMatkhau.Text;
                if (tk.Trim().Length == 0 || mk.Trim().Length == 0)
                {
                    MessageBox.Show("Tài Khoản hoặc Mật Khẩu không được bỏ trông!");
                    return;
                }
                string sql = "select count(*) from NhanVien where TaiKhoan = @tk and MatKhau = @mk";
                string sql1 = "select * from NhanVien where TaiKhoan = @tk and MatKhau = @mk";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tk", tk));
                data.Add(new SqlParameter("@mk", mk));
                List<SqlParameter> dta = new List<SqlParameter>();
                dta.Add(new SqlParameter("@tk", tk));
                dta.Add(new SqlParameter("@mk", mk));
                int rs = (int)conn.CountData(sql, data);
                DataSet ds = conn.getData(sql1, "NhanVien", dta);
                string b = ds.Tables["NhanVien"].Rows[0]["TenChucVu"].ToString();
                string name = ds.Tables["NhanVien"].Rows[0]["TenNV"].ToString();
                string ma = ds.Tables["NhanVien"].Rows[0]["MaNV"].ToString();
                setRole(b);
                setName(name);
                setId(ma);
                if (rs == 1)
                {
                    MessageBox.Show("Đăng Nhập thành công !" ) ;
                    this.DialogResult = DialogResult.OK;
                    Main frm = new Main();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng Nhập thất bại !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Kết Nối!"+ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Register frm = new Register();
            frm.ShowDialog();
        }

        
    }
}
