using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace QLLKMT.src.Database
{
    class Connect
    {
        
        string con_str = "Data Source=LAPTOP-A1ALD9SE\\SQLEXPRESS; Initial catalog=QLLKMT_C#;User ID = sa; Password = 123456;";
        SqlConnection conn = null;
        public Connect()
        {
            conn = new SqlConnection(con_str);
        }
        public DataSet getData(String query, String table_name, List<SqlParameter> data)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                if (data != null)
                {
                    da.SelectCommand.Parameters.AddRange(data.ToArray());
                }
                da.Fill(ds, table_name);
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối data" + e);
            }
            return ds;
        }

        public void Updatedata(String query, List<SqlParameter> data)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if (data != null)
                {
                    cmd.Parameters.AddRange(data.ToArray());
                }
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lối update data" + e);
            }
            
        }
        public int CountData(String sql, List<SqlParameter> data)
        {
            int rs = 0;
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection(con_str);
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (data != null)
                {
                    cmd.Parameters.AddRange(data.ToArray());
                }
                rs = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rs;
        }
    }
}
