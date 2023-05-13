using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QLQA
{
    public partial class fHome : Form
    {
        public fHome()
        {
            InitializeComponent();
        }

        void FillChart()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-795K8U1\\TRUNGNGHIA;Initial Catalog=QLQA;Integrated Security=True");
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select aDate ,total from tblMain where status = N'Đã thanh toán'", con);
            da.Fill(dt);
            chart1.DataSource= dt;
            con.Close();

            chart1.Series["total"].XValueMember = "aDate";
            chart1.Series["total"].YValueMembers = "total";
            chart1.Titles.Add("Monthly Profit");
        }

        private void fHome_Load(object sender, EventArgs e)
        {
            FillChart();

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-795K8U1\\TRUNGNGHIA;Initial Catalog=QLQA;Integrated Security=True");
            con.Open();

            SqlCommand cmd1 = new SqlCommand("select Count(*) from category", con);
            var countCategory = cmd1.ExecuteScalar();
            lblCategory.Text = countCategory.ToString();

            SqlCommand cmd2 = new SqlCommand("select Count(*) from products", con);
            var countProduct = cmd2.ExecuteScalar();
            lblProduct.Text = countProduct.ToString();

            SqlCommand cmd3 = new SqlCommand("select Count(*) from tables", con);
            var countTable = cmd3.ExecuteScalar();
            lblTable.Text = countTable.ToString();

            SqlCommand cmd4 = new SqlCommand("select Count(*) from staff", con);
            var countStaff = cmd3.ExecuteScalar();
            lblStaff.Text = countStaff.ToString();

            con.Close();
        }
    }
}
