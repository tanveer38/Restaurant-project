using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQA.Model
{
    public partial class fManage : Form
    {
        public fManage()
        {
            InitializeComponent();
        }

        public int MainID = 0;
        public string OrderType;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fManage_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory() 
        {
            string query = "Select * from Category";
            SqlCommand cmd = new SqlCommand(query, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);    
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(197, 61);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["catName"].ToString();

                    // Sự kiện click
                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }

            }

        }

        private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            if(b.Text == "All Categories")
            {
                txtSearch.Text = "1";
                txtSearch.Text = "";
                return;
            }

            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void AddItems(string id, String proID, string name, string cat, string price, Image pimage)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pimage,
                id = Convert.ToInt32(proID),
            };
            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach(DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    // Kiểm tra xem sản phẩm đã có chưa, sau đó kiểm tra số lượng và cập nhật giá
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());

                        GetTotal();
                        return;
                    }

                }
                // Dòng để thêm sản phẩm
                guna2DataGridView1.Rows.Add(new object[] {0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                GetTotal();
            };
        }

        // Lấy sản phẩm từ database
         private void LoadProducts()
         {
            string query = "Select * from products inner join category on catID = CategoryID";
            SqlCommand cmd = new SqlCommand(query, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["pImage"];
                byte[] imagebytearray = imagearray;

                AddItems("0" ,item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(),
                        item["pPrice"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
         }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach ( var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
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

        private void GetTotal()
        {
            double tot = 0;
            lblTotal.Text = "";

            foreach(DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }

            lblTotal.Text = tot.ToString("N2");

        }


        //private string GetTotal()
        //{
        //    double grandTotal = 0; 
        //    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
        //    {
        //        double amount = Convert.ToDouble(row.Cells["dgvAmount"].Value);
        //        grandTotal += amount;
        //    }
        //    string grandTotalString = grandTotal.ToString("C"); // Format the grand total as currency
        //    lblTotal.Text = grandTotalString; // Update the label with the grand total value
        //    return grandTotalString; // Return the grand total as a string
        //}

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            lblTotal.Text = "00";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Vận chuyển";
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Mua mang đi";
        }

        private void btnDin_Click(object sender, EventArgs e)
        {
            OrderType = "Ăn tại quán";

            fTableSelect f = new fTableSelect();
            MainClass.BlurBackground(f);
            if (f.TableName != "")
            {
                lblTable.Text = f.TableName;
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;

            }

            fWaiterSelect f2 = new fWaiterSelect();
            MainClass.BlurBackground(f2);
            if (f2.waiterName != "")
            {
                lblWaiter.Text = f2.waiterName;
                lblWaiter.Visible = true;
            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;

            }
        }

        private void btnKot_Click(object sender, EventArgs e)
        {
            string query1 = ""; // Main table
            string query2 = ""; // Detail table

            int detailID = 0;

            if (MainID == 0) // Insert
            {
                query1 = @"Insert into tblMain Values(@aDate, @aTime, @TableName, @WaiterName, 
                            @status, @orderType, @total, @received, @change);
                                Select SCOPE_IDENTITY()";
                // Dòng nhận giá trị id gân đây

            }
            else // Update
            {
                query1 = @"Update tblMain Set status = @status, total = @total, 
                        received = @received, change = @change where MainID = @ID";
            }

    

            SqlCommand cmd = new SqlCommand(query1, MainClass.con);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@WaiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "Pending");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text)); // Chỉ lưu dât cho giá trị nhà bếp sẽ cập nhật khi thanh toán hóa đơn
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));

            if(MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
            if(MainID == 0) { MainID = Convert.ToInt32(cmd.ExecuteScalar()); } else { cmd.ExecuteNonQuery(); }
            if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }


            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detailID == 0) // Insert
                {
                    query2 = @"Insert into tblDetails Values(@MainID, @proID, @qty, @price, @amount)";
                }
                else // Update
                {
                    query2 = @"Update tblDetails Set proID = @proID, qty = @qty, price = @price, amount = @amount
                                where DetailID = @ID";

                }

                SqlCommand cmd2 = new SqlCommand(query2, MainClass.con);
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }

                guna2MessageDialog1.Show("Lưu thành công");
                MainID = 0;
                detailID = 0;
                guna2DataGridView1.Rows.Clear();
                lblTable.Text = "";
                lblWaiter.Text = "";
                lblTable.Visible = false;
                lblWaiter.Visible = false;
                lblTotal.Text = "00";
            }

        }
    }
}
