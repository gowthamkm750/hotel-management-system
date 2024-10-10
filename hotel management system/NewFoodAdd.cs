using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data.SqlTypes;

namespace hotel_management_system
{
    public partial class NewFoodAdd : Form
    {
        SqlConnection con= new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB;Integrated Security=True");
        public NewFoodAdd()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1   f1=new Form1();
            f1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = fname.Text.ToUpper();
                string type = veg.Checked ? "VEG" : "NON-VEG";
                int price = int.Parse(fprice.Text);
                string ava = favail.SelectedItem.ToString();
                SqlCommand cmd = new SqlCommand("insert into tblfood values('" + name + "','" + type + "','" + price + "','" + ava + "');", con);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show((res>0) ? "NEW FOOD ADDED" : "FOOD WAS NOT ADDED");
                fname.Clear();veg.Checked = false;fprice.Clear();favail.SelectedItem=null;
            }
            catch (Exception x) 
            {
                MessageBox.Show("An error occurred: " + x.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from tblFood;",con);
            con.Open();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataSet ds=new DataSet();
            da.Fill(ds);
            output.DataSource = ds.Tables[0];
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string name = fname.Text.ToUpper();
                SqlCommand cmd = new SqlCommand("delete from tblfood where fname='"+name+"';", con);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show((res > 0) ? "FOOD DELETED" : "FOOD WAS NOT DELETED");
                fname.Clear();
            }
            catch (Exception x)
            {
                MessageBox.Show("An error occurred: " + x.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string name = fname.Text.ToUpper();
                string type = veg.Checked ? "VEG" : "NON-VEG";
                int price = int.Parse(fprice.Text);
                string ava = favail.SelectedItem?.ToString();
                string q = "update tblfood set ftype='"+type+"',fprice="+price+",favailable='"+ava+"' where fname='"+name+"'";
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show((res > 0) ? $"{name} WAS UPDATED" : $"{name} WAS NOT UPDATED");
                fname.Clear(); veg.Checked = false; fprice.Clear(); favail.SelectedItem = null;
            }
            catch (Exception x)
            {
                MessageBox.Show("An error occurred: " + x.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try 
            {
                string name = fname.Text.ToUpper();
                SqlCommand cmd = new SqlCommand("select * from tblFood where fname='"+name+"';", con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                output.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
