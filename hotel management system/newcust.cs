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
using System.Xml.Linq;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace hotel_management_system
{
    public partial class newcust : Form
    {
        SqlConnection conn=new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB;Integrated Security=True");
        public newcust()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 cp=new Form1();
            cp.Show();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name=cname.Text;
            long phone=long.Parse(cnum.Text);
            string idtyp=id.Text.ToString();
            string cardnum=idnum.Text;
            string pcity=city.Text;
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName ="@cusname";
                p1.Direction = ParameterDirection.Input;
                p1.SqlDbType = SqlDbType.VarChar;
                p1.Size = 30;
                p1.Value = name;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@cusphone";
                p2.Direction = ParameterDirection.Input;
                p2.SqlDbType = SqlDbType.BigInt;
                p2.Value = phone;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@cusid";
                p3.Direction = ParameterDirection.Input;
                p3.SqlDbType = SqlDbType.VarChar;
                p3.Size = 20;
                p3.Value = idtyp;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@cusidnum";
                p4.Direction = ParameterDirection.Input;
                p4.SqlDbType = SqlDbType.VarChar;
                p4.Size = 40;
                p4.Value = cardnum;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@cuspcity";
                p5.Direction = ParameterDirection.Input;
                p5.SqlDbType = SqlDbType.VarChar;
                p5.Size = 30;
                p5.Value = pcity;



                string sqlcmd = "insert into HCUTOMER values(@cusname,@cusphone,@cusid,@cusidnum,@cuspcity)";
                SqlCommand cmd = new SqlCommand(sqlcmd,conn);
                conn.Open();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                int res= cmd.ExecuteNonQuery();
                MessageBox.Show((res != 0) ? "DATA SAVED" : "DATA NOT SAVED");

            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x.Message);
            }
            finally
            {
                conn.Close();
                cname.Clear();
                cnum.Clear();
                city.Clear();
                idnum.Clear();
                id.Text = "-SELECT ANY OPTION-";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cname.Clear();
            city.Clear();
            idnum.Clear();
            id.Text = "-SELECT ANY OPTION-";
            long phone = long.Parse(cnum.Text);
            try
            {
                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@cusphone";
                p2.Direction = ParameterDirection.Input;
                p2.SqlDbType = SqlDbType.BigInt;
                p2.Value = phone;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@cusname";
                p1.Direction = ParameterDirection.Output;
                p1.SqlDbType = SqlDbType.VarChar;
                p1.Size = 30;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@cusid";
                p3.Direction = ParameterDirection.Output;
                p3.SqlDbType = SqlDbType.VarChar;
                p3.Size = 20;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@cusidnum";
                p4.Direction = ParameterDirection.Output;
                p4.SqlDbType = SqlDbType.VarChar;
                p4.Size = 40;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@cuspcity";
                p5.Direction = ParameterDirection.Output;
                p5.SqlDbType = SqlDbType.VarChar;
                p5.Size = 30;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@cusidno";
                p6.Direction = ParameterDirection.Output;
                p6.SqlDbType = SqlDbType.Int;


                string sqlcmd = "select @cusname=cname,@cusid=idtype,@cusidnum=idnum,@cuspcity=city,@cusidno=cid from HCUTOMER where cphone=@cusphone";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                conn.Open();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.ExecuteNonQuery();
                cname.Text=p1.Value.ToString();
                city.Text=p5.Value.ToString();
                cidno.Text = "C_id:" + p6.Value;
                idnum.Text=p4.Value.ToString();
                id.Text=p3.Value.ToString();
                


            }
            catch(Exception x)
            {
                MessageBox.Show("Error:"+x.Message);
            }
            finally
            { 
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cname.Clear();
            cnum.Clear();
            city.Clear();
            idnum.Clear();
            id.Text="-SELECT ANY OPTION-";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            long phone = long.Parse(cnum.Text);
            try
            {
                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@cusphone";
                p2.Direction = ParameterDirection.Input;
                p2.SqlDbType = SqlDbType.BigInt;
                p2.Value = phone;

                string sqlcmd = "DELETE FROM HCUTOMER WHERE CPHONE=@cusphone";
                SqlCommand cmd = new SqlCommand(sqlcmd,conn);
                conn.Open();
                cmd.Parameters.Add(p2);
                int res=cmd.ExecuteNonQuery();
                MessageBox.Show((res!=0)?"DELETED SUCESS":"NOT DELETED");
            }
            catch (Exception x)
            {
                MessageBox.Show("ERROR:" + x.Message);
            }
            finally
            {

                conn.Close();
                cnum.Clear();
                cname.Clear();
                cnum.Clear();
                city.Clear();
                idnum.Clear();
                id.Text = "-SELECT ANY OPTION-";
            }
        }
    }
}
