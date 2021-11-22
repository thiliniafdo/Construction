using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WFM_Construction
{
    public partial class Employee : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ConnectionString cs = new ConnectionString();
        public Employee()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            EmpName.Text = "";
            txtContactNo.Text = "";
            StaffID.Text = "";
            Qualification.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = true;
            EmpName.Focus();

        }
     
       
       
      private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmpName.Text == "")
            {
                MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmpName.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }

            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter City", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtContactNo.Text == "") 
            {
                MessageBox.Show("Please enter Mobile Number", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtEmail.Text == "") 
            {
                MessageBox.Show("Please enter Email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (Qualification.Text == "")
            {
                MessageBox.Show("Please enter Qualification", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            try
            {
             
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into Employees(StaffID,empName,Address,City,ContactNo,Email,JoiningDate,Qualification) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", StaffID.Text);
                cmd.Parameters.AddWithValue("@d2", EmpName.Text);
                cmd.Parameters.AddWithValue("@d3", txtAddress.Text);
                cmd.Parameters.AddWithValue("@d4", txtCity.Text);
                cmd.Parameters.AddWithValue("@d5", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@d6", txtEmail.Text);
                cmd.Parameters.AddWithValue("@d7", JointDate.Text);
                cmd.Parameters.AddWithValue("@d8", Qualification.Text);
                cmd.ExecuteReader();
                MessageBox.Show("Profile Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
      


                GetData();
                //clear inputs After Save 
                
                    EmpName.Text = "";
                    txtAddress.Text = "";
                    txtCity.Text = "";
                    txtContactNo.Text = "";
                    txtEmail.Text = "";
                    StaffID.Text = "";
                    Qualification.Text = "";
                    StaffID.Focus();
                    btnSave.Enabled = true;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Close();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void delete_records()
        {
            
            try
            {

              int RowsAffected = 0;
              con = new SqlConnection(cs.DBConn);
              con.Open();
              string cq = "delete from Employees where ID='" + ID.Text + "'";
              cmd = new SqlCommand(cq);
              cmd.Connection = con;
            
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Profile Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    GetData();
                }
                else
                {
                    MessageBox.Show("Sorry No record found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                    con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /////delete  


            if (MessageBox.Show("Do you really want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }


           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (EmpName.Text == "")
            {
                MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmpName.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }

            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter City", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtContactNo.Text == "")
            {
                MessageBox.Show("Please enter Mobile Number", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter Email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (Qualification.Text == "")
            {
                MessageBox.Show("Please enter Qualification", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            try
            {
          //// functions for Update
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update Employees set StaffID=@d1,empName=@d2,Address=@d3,City=@d4,ContactNo=@d5,Email=@d6,JoiningDate=@d7,Qualification=@d8 where Id=@did";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", StaffID.Text);
                cmd.Parameters.AddWithValue("@d2", EmpName.Text);
                cmd.Parameters.AddWithValue("@d3", txtAddress.Text);
                cmd.Parameters.AddWithValue("@d4", txtCity.Text);
                cmd.Parameters.AddWithValue("@d5", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@d6", txtEmail.Text);
                cmd.Parameters.AddWithValue("@d7", JointDate.Text);
                cmd.Parameters.AddWithValue("@d8", Qualification.Text);
                cmd.Parameters.AddWithValue("@did", ID.Text);
                cmd.ExecuteReader();
                MessageBox.Show("Profile Successfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                GetData();

                // clear
                EmpName.Text = "";
                txtAddress.Text = "";
                txtCity.Text = "";
                txtContactNo.Text = "";
                txtEmail.Text = "";
                StaffID.Text = "";
                Qualification.Text = "";
                StaffID.Focus();
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
             
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 47 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(ID),RTRIM(StaffID),RTRIM(empName),RTRIM(Address),RTRIM(City),RTRIM(ContactNo),RTRIM(Email),RTRIM(Qualification),RTRIM(Joiningdate) from Employees", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Employee_Add_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get into text back
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            ID.Text = dr.Cells[0].Value.ToString();
            StaffID.Text = dr.Cells[1].Value.ToString();
            EmpName.Text = dr.Cells[2].Value.ToString();
            txtAddress.Text = dr.Cells[3].Value.ToString();
            txtCity.Text = dr.Cells[4].Value.ToString();
            txtEmail.Text = dr.Cells[5].Value.ToString();
            txtContactNo.Text = dr.Cells[6].Value.ToString();
            Qualification.Text = dr.Cells[7].Value.ToString();
            JointDate.Text = dr.Cells[8].Value.ToString();
            // reset Buttons & force StaffID text
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            StaffID.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //code for search staff id
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(ID),RTRIM(StaffID),RTRIM(empName),RTRIM(Address),RTRIM(City),RTRIM(ContactNo),RTRIM(Email),RTRIM(Qualification),RTRIM(Joiningdate) from Employees where  StaffID like '" + textBox1.Text + "%' order by StaffID", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //code for search Employee Name
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(ID),RTRIM(StaffID),RTRIM(empName),RTRIM(Address),RTRIM(City),RTRIM(ContactNo),RTRIM(Email),RTRIM(Qualification),RTRIM(Joiningdate) from Employees where  empName like '" + textBox2.Text + "%' order by empName", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
       

    }
}
