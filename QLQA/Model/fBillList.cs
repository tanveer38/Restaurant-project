using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQA.Model
{
    public partial class fBillList : SampleAdd
    {
        public fBillList()
        {
            InitializeComponent();
        }

        public int MainID = 0;

        private void fBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string query = @"select MainID, TableName, WaiterName, orderType, status, total from 
                            tblMain where status <> 'Chờ' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvtable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);


            MainClass.LoadData(query, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // for searil no
            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Edit 
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {

                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();               

            }

            // Print
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvPrint")
            {

               string status = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvStatus"].Value);

                if(status == "Đã thanh toán")
                {
                    string MainID = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    MessageBox.Show(MainID);

                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-795K8U1\\TRUNGNGHIA;Initial Catalog=QLQA;Integrated Security=True");
                    SqlCommand command = new SqlCommand("select * from tblMain , tblDetails , products " +
                        "where tblMain.MainID = tblDetails.MainID and tblDetails.proID = products.pID " +
                        "and tblMain.MainID = '" + MainID + "'", con);

                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataSet s = new DataSet();
                    sd.Fill(s);

                    rptBill sr = new rptBill();
                    sr.SetDataSource(s.Tables["table"]);


                    var fPrintBill = new fPrintBill();
                    var report = new rptBill();

                    fPrintBill.SetReportSource(report);
                    fPrintBill.Show();

                    //fPrintBill f = new fPrintBill();
                    //f.crystalReportViewerPrintBill.ReportSource = sr;

                    //fPrintBill f = new fPrintBill();
                    //f.ShowDialog();

                }
                else
                {
                    guna2MessageDialog1.Show("Bạn chưa thanh toán");
                }


            }

        }
    }
}
