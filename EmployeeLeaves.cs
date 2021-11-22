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
    public partial class EmployeeLeaves : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ConnectionString cs = new ConnectionString();
        public EmployeeLeaves()
        {
            InitializeComponent();
        }
        private void Reset()
        {
          
            txtreason.Text = "";
           
            txtnote.Text = "";
            StaffID.Text = "";
             
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = true;
         

        }
     
       
       
      private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (StaffID.Text == "")
            {
                MessageBox.Show("Please enter Staff ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StaffID.Focus();
                return;
            }

            if (txtreason.Text == "")
            {
                MessageBox.Show("Please enter Reason", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtreason.Focus();
                return;
            }

            if (txtnote.Text == "")
            {
                MessageBox.Show("Please enter Note", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtnote.Focus();
                return;
            }
           
            try
            {
             
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into Leaves(StaffID,fromDate,toDate,reason,note) VALUES (@d1,@d2,@d3,@d4,@d5)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", StaffID.Text);
                cmd.Parameters.AddWithValue("@d2", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@d3", dateTimePicker2.Text);
                cmd.Parameters.AddWithValue("@d4", txtreason.Text);
                cmd.Parameters.AddWithValue("@d5", txtnote.Text);
                cmd.ExecuteReader();
                MessageBox.Show("Leave Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
                //clear inputs After Save 

                StaffID.Text = "";
                txtreason.Text = "";
                txtnote.Text = "";
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
              string cq = "delete from Leaves where ID='" + ID.Text + "'";
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
            try
            {
          //// functions for Update
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update Leaves set StaffID=@d1,fromDate=@d2,toDate=@d3,reason=@d4,note=@d5 where Id=@did";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", StaffID.Text);
                cmd.Parameters.AddWithValue("@d2", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@d3", dateTimePicker2.Text);
                cmd.Parameters.AddWithValue("@d4", txtreason.Text);
                cmd.Parameters.AddWithValue("@d5", txtnote.Text);
                cmd.Parameters.AddWithValue("@did", ID.Text);
                cmd.ExecuteReader();
                MessageBox.Show("Leave Successfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                GetData();

                // clear
               
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
                cmd = new SqlCommand("SELECT RTRIM(Leaves.ID),RTRIM(Employees.StaffID),RTRIM(empName),RTRIM(fromDate),RTRIM(toDate),RTRIM(reason),RTRIM(note)from Leaves, Employees where Leaves.StaffID = Employees.StaffID ", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void FillCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(StaffID) from Employees order by StaffID";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    StaffID.Items.Add(rdr[0]);
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
            GetData(); FillCombo();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        //    get into text back
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            ID.Text = dr.Cells[0].Value.ToString();
            StaffID.Text = dr.Cells[1].Value.ToString();
            dateTimePicker1.Text = dr.Cells[3].Value.ToString();
            dateTimePicker2.Text = dr.Cells[4].Value.ToString();
            txtreason.Text = dr.Cells[5].Value.ToString();
            txtnote.Text = dr.Cells[6].Value.ToString();
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
                cmd = new SqlCommand("SELECT RTRIM(Leaves.ID),RTRIM(Employees.StaffID),RTRIM(empName),RTRIM(fromDate),RTRIM(toDate),RTRIM(reason),RTRIM(note)from Leaves, Employees where Leaves.StaffID = Employees.StaffID and  StaffID like '" + textBox1.Text + "%' order by StaffID", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6]);
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
