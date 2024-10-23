using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace hotel_management_system
{

    public partial class SaleBill : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB;Integrated Security=True");
        public SaleBill()
        {
            InitializeComponent();
        }

        private void SaleBill_Load(object sender, EventArgs e)
        {
            foodlist();
        }
        public void foodlist()
        {
            SqlCommand cmd = new SqlCommand("select fname from tblfood;", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                fname.Items.Add(row["fname"].ToString());
            }
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                int bill = int.Parse(billno.Text);
                SqlCommand cmd = new SqlCommand("select *from tblbilling where billno=" + bill + ";", con);
                SqlCommand cmd1 = new SqlCommand("Select sum(amount) from tblbilling where billno=" + bill + ";", con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                saleoutput.DataSource = ds.Tables[0];
                object result = cmd1.ExecuteScalar();
                total.Text = result != null ? "TOTAL AMOUNT: " + result.ToString() : "TOTAL AMOUNT: 0";
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void amt_TextChanged(object sender, EventArgs e)
        {

        }

        private void price_TextChanged(object sender, EventArgs e)
        {

        }


        private void Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(Qty.Text);
                int b = int.Parse(price.Text);
                amt.Text = (a * b).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int bill = int.Parse(billno.Text);
            int pr = int.Parse(price.Text);
            int qun = int.Parse(Qty.Text);
            int amtt = int.Parse(amt.Text);
            string fn = fname.Text;
            SqlCommand c = new SqlCommand("select fid from tblfood where fname='" + fn + "';", con);
            con.Open();
            object fidd = c.ExecuteScalar();
            int fd = int.Parse(fidd.ToString());
            SqlCommand cmd = new SqlCommand("insert into tblbilling(fid,BillNo,Price,Quantity,Amount) values(" + fd + "," + bill + "," + pr + "," + qun + "," + amtt + ");", con);
            int res = cmd.ExecuteNonQuery();
            MessageBox.Show((res > 0) ? "Added" : "not added");
            con.Close();

        }

        private void billno_TextChanged(object sender, EventArgs e)
        {

        }

        private void fname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = fname.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand("select fprice from tblfood where fname='" + name + "';", con);
            con.Open();
            object re = cmd.ExecuteScalar();
            if (re != null)
            {
                price.Text = Convert.ToInt32(re).ToString();
            }
            con.Close();
        }
    }
}