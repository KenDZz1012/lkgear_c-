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
    public partial class buildCase : Form
    {
        List<buildCase> product = new List<buildCase>();
        Connect conn = new Connect();
        public buildCase()
        {
            InitializeComponent();
        }
        private string id;
        private int qty;
        string mahd = frmBanHang.getidHD();
        public buildCase(string MaHD,string id,int qty)
        {
            this.MaHD = MaHD;
            this.id = id;
            this.qty = qty;
        }
        public string MaHD { get => mahd; set => mahd = value; }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public int QTY
        {
            get { return qty; }
            set { qty = value; }
        }


        private void buildCase_Load(object sender, EventArgs e)
        {
        
        }
        private void showTT()
        {
            try
            {
                double giaVGA = 0;
                double giaRAM = 0;
                double giaPSU = 0;
                double giaCASE = 0;
                double giaCPU = 0;
                double giaSCREEN = 0;
                double giaFAN = 0;
                double giaSSD = 0;
                double giaHDD = 0;
                double giaMB = 0;
                if (double.TryParse(lbGiaVGA.Text, out giaVGA))
                {
                    giaVGA = double.Parse(lbGiaVGA.Text);
                }
                if (double.TryParse(lbGiaMB.Text, out giaMB))
                {
                    giaMB = double.Parse(lbGiaMB.Text);
                }
                if (double.TryParse(lbGiaCPU.Text, out giaCPU))
                {
                    giaCPU = double.Parse(lbGiaCPU.Text);
                }
                if (double.TryParse(lbGiaPSU.Text, out giaPSU))
                {
                    giaPSU = double.Parse(lbGiaPSU.Text);
                }
                if (double.TryParse(lbGiaSSD.Text, out giaSSD))
                {
                    giaSSD = double.Parse(lbGiaSSD.Text);
                }
                if (double.TryParse(lbGiaHDD.Text, out giaHDD))
                {
                    giaHDD = double.Parse(lbGiaHDD.Text);
                }
                if (double.TryParse(lbGiaSCREEN.Text, out giaSCREEN))
                {
                    giaSCREEN = double.Parse(lbGiaSCREEN.Text);
                }
                if (double.TryParse(lbGiaCASE.Text, out giaCASE))
                {
                    giaCASE = double.Parse(lbGiaCASE.Text);
                }
                if (double.TryParse(lbGiaFAN.Text, out giaFAN))
                {
                    giaFAN = double.Parse(lbGiaFAN.Text);
                }
                if (double.TryParse(lbGiaRAM.Text, out giaRAM))
                {
                    giaRAM = double.Parse(lbGiaRAM.Text);
                }
                double tong = giaCASE + giaCPU + giaFAN + giaHDD + giaMB + giaPSU + giaRAM + giaSCREEN + giaSSD + giaVGA;
                lbTong.Text = tong.ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if(index >= 0)
                {
                    string tenlsp = dataGridView1.Rows[index].Cells["TenLSP"].Value.ToString();
                    string g = "";
                    double gia = 0;
                    switch (tenlsp)
                    {
                        case "VGA":
                            byte[] a = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptVGA.Image = ByteArrayToImage(a);
                            ptVGA.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenVGA.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(numVGA.Value.ToString());
                            lbGiaVGA.Text = gia.ToString();
                            showTT();
                            break;
                        case "CPU":
                            byte[] b = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptCPU.Image = ByteArrayToImage(b);
                            ptCPU.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenCPU.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(numCPU.Value.ToString());                
                            lbGiaCPU.Text = gia.ToString();
                            showTT();
                            break;
                        case "MAINBOARD":
                            byte[]c  = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptMB.Image = ByteArrayToImage(c);
                            ptMB.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenMB.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbMB.Value.ToString());
                            lbGiaMB.Text = gia.ToString();

                            showTT();

                            break;
                        case "SSD":
                            byte[] d = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptSSD.Image = ByteArrayToImage(d);
                            ptSSD.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenSSD.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbSSD.Value.ToString());
                            lbGiaSSD.Text = gia.ToString();
                            showTT();
                            break;
                        case "HDD":
                            byte[] k = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptHDD.Image = ByteArrayToImage(k);
                            ptHDD.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenHDD.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbHDD.Value.ToString());
                            lbGiaHDD.Text = gia.ToString();
                            showTT();

                            break;
                        case "RAM":
                            byte[] h = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptRAM.Image = ByteArrayToImage(h);
                            ptRAM.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenRAM.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbRAM.Value.ToString());
                            lbGiaRAM.Text = gia.ToString();
                            showTT();
                            break;
                        case "SCREEN":
                            byte[] l = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptSCREEN.Image = ByteArrayToImage(l);
                            ptSCREEN.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenSCREEN.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbSCREEN.Value.ToString());
                            lbGiaSCREEN.Text = gia.ToString();
                            showTT();
                            break;
                        case "PSU":
                            byte[] m = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptPSU.Image = ByteArrayToImage(m);
                            ptPSU.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenPSU.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbPSU.Value.ToString());
                            lbGiaPSU.Text = gia.ToString();
                            showTT();

                            break;
                        case "CASE":
                            byte[] n = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptCASE.Image = ByteArrayToImage(n);
                            ptCASE.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenCASE.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbCASE.Value.ToString());
                            lbGiaCASE.Text = gia.ToString();
                            showTT();

                            break;
                        case "FAN":
                            byte[] o = (byte[])dataGridView1.Rows[index].Cells["AnhSP"].Value;
                            ptFAN.Image = ByteArrayToImage(o);
                            ptFAN.SizeMode = PictureBoxSizeMode.StretchImage;
                            lbTenFAN.Text = dataGridView1.Rows[index].Cells["MaSP"].Value.ToString();
                            g = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
                            gia = double.Parse(g) * double.Parse(nbFAN.Value.ToString());
                            lbGiaFAN.Text = gia.ToString();
                            showTT();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnThemVGA_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'VGA'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaVGA_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if(item.ID == lbTenVGA.Text)
                    {
                        product.Remove(item);
                    }    
                }
                ptVGA.Image = null;
                lbTenVGA.Text = "";
                lbGiaVGA.Text = "";
                numVGA.Value = 1;
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'CPU'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnXoaCPU_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenCPU.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptCPU.Image = null;
                lbTenCPU.Text = "";
                lbGiaCPU.Text = "";
                numCPU.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemMB_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'MAINBOARD'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaMB_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenMB.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptMB.Image = null;
                lbTenMB.Text = "";
                lbGiaMB.Text = "";
                nbMB.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemRAM_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'RAM'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemSSD_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'SSD'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemHDD_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'HDD'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemPSU_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'PSU'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemCASE_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'CASE'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemSCREEN_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'SCREEN'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemFAN_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select MaSP,TenSP,AnhSP,TenLSP,TenNhaCC,DonGia,BaoHanh from SanPham Where SoLuong > SoLuongHong and TenLSP = 'FAN'";
                DataSet rs = conn.getData(sql, "SanPham", null);
                dataGridView1.DataSource = rs.Tables["SanPham"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaRAM_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenRAM.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptRAM.Image = null;
                lbTenRAM.Text = "";
                lbGiaRAM.Text = "";
                nbRAM.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaSSD_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenSSD.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptSSD.Image = null;
                lbTenSSD.Text = "";
                lbGiaSSD.Text = "";
                nbSSD.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaHDD_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenHDD.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptHDD.Image = null;
                lbTenHDD.Text = "";
                lbGiaHDD.Text = "";
                nbHDD.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaPSU_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenPSU.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptPSU.Image = null;
                lbTenPSU.Text = "";
                lbGiaPSU.Text = "";
                nbPSU.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaCASE_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenCASE.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptCASE.Image = null;
                lbTenCASE.Text = "";
                lbGiaCASE.Text = "";
                nbCASE.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaSCREEN_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenSCREEN.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptSCREEN.Image = null;
                lbTenSCREEN.Text = "";
                lbGiaSCREEN.Text = "";
                nbSCREEN.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaFAN_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in product.ToList())
                {
                    if (item.ID == lbTenFAN.Text)
                    {
                        product.Remove(item);
                    }
                }
                ptFAN.Image = null;
                lbTenFAN.Text = "";
                lbGiaFAN.Text = "";
                nbFAN.Value = 1;
                showTT();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                if(lbTenVGA.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenVGA.Text, int.Parse(numVGA.Value.ToString())));
                }
                if (lbTenCPU.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenCPU.Text, int.Parse(numCPU.Value.ToString())));
                }
                if (lbTenRAM.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenRAM.Text, int.Parse(nbRAM.Value.ToString())));
                }
                if (lbTenPSU.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenPSU.Text, int.Parse(nbPSU.Value.ToString())));
                }
                if (lbTenMB.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenMB.Text, int.Parse(nbMB.Value.ToString())));
                }
                if (lbTenSCREEN.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenSCREEN.Text, int.Parse(nbSCREEN.Value.ToString())));
                }
                if (lbTenCASE.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenCASE.Text, int.Parse(nbCASE.Value.ToString())));
                }
                if (lbTenFAN.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenFAN.Text, int.Parse(nbFAN.Value.ToString())));
                }
                if (lbTenSSD.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenSSD.Text, int.Parse(nbSSD.Value.ToString())));
                }
                if (lbTenHDD.Text.Length > 0)
                {
                    product.Add(new buildCase(mahd, lbTenHDD.Text, int.Parse(nbHDD.Value.ToString())));
                }
                foreach (var item in product)
                {
                    string idhd = item.MaHD;
                    string idsp = item.ID;
                    int qtySP = item.QTY;
                    string sql = "Insert into CTHoaDon values(@mahd,@masp,@qty)";
                    List<SqlParameter> data = new List<SqlParameter>();
                    data.Add(new SqlParameter("@mahd", idhd));
                    data.Add(new SqlParameter("@masp", idsp));
                    data.Add(new SqlParameter("@qty", qtySP));
                    conn.Updatedata(sql, data);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numCPU_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenCPU.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql,"SanPham",data);
                if(rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(numCPU.Value.ToString());
                lbGiaCPU.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbMB_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenMB.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbMB.Value.ToString());
                lbGiaMB.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbRAM_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenRAM.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbRAM.Value.ToString());
                lbGiaRAM.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbSSD_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenSSD.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbSSD.Value.ToString());
                lbGiaSSD.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbHDD_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenHDD.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbHDD.Value.ToString());
                lbGiaHDD.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numVGA_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenVGA.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(numVGA.Value.ToString());
                lbGiaVGA.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbPSU_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenPSU.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbPSU.Value.ToString());
                lbGiaPSU.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbCASE_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenCASE.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbCASE.Value.ToString());
                lbGiaCASE.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbSCREEN_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenSCREEN.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbSCREEN.Value.ToString());
                lbGiaSCREEN.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nbFAN_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = lbTenFAN.Text;
                string sql = "Select * from SanPham Where MaSP = @id";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@id", id));
                DataSet rs = conn.getData(sql, "SanPham", data);
                if (rs.Tables["SanPham"].Rows.Count <= 0)
                {
                    return;
                }
                string g = rs.Tables["SanPham"].Rows[0]["DonGia"].ToString();
                double gia = double.Parse(g.ToString()) * double.Parse(nbFAN.Value.ToString());
                lbGiaFAN.Text = gia.ToString();
                showTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
