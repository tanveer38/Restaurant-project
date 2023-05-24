using QLQA.Reports;
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

namespace QLQA
{
    public partial class fPrintCategory : Form
    {
        public fPrintCategory()
        {
            InitializeComponent();
        }

        private void btnPrintCategory_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-795K8U1\\TRUNGNGHIA;Initial Catalog=QLQA;Integrated Security=True");
            SqlCommand command = new SqlCommand("select catName ,SUM(p.pPrice) from category as c, products as p\r\nwhere c.catID = p.CategoryID\r\ngroup by catName", con);

            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataSet s = new DataSet();
            sd.Fill(s);

            rptListCategory sr = new rptListCategory();
            sr.SetDatabaseLogon("sa", "123456");
            sr.SetDataSource(s.Tables["table"]);
            crystalReportViewer1.ReportSource = sr;
        }
    }
}
