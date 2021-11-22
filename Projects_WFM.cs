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
    public partial class Projects_WFM : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ConnectionString cs = new ConnectionString();
        public Projects_WFM()
        {
            InitializeComponent();
        }
        private void Reset()
        {

            ProjectCode.Text = "";
            ClientName.Text = "";
            txtnote.Text = "";
            ClientMobile.Text = "";
            Cost.Text = "";
            Profilt.Text = "";
            Duration.Text = "";
            txtnote.Text = "";
            ProjectCode.Focus();
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
            if (ProjectCode.Text == "")
            {
                MessageBox.Show("Please enter Project Code", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProjectCode.Focus();
                return;
            }

            
            if (ClientName.Text == "")
            {
                MessageBox.Show("Please enter Client Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientName.Focus();
                return;
            }

            if (ClientMobile.Text == "")
            {
                MessageBox.Show("Please enter Client Mobile", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientMobile.Focus();
                return;
            }

            if (Cost.Text == "")
            {
                MessageBox.Show("Please enter Cost", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cost.Focus();
                return;
            }
            if (Profilt.Text == "")
            {
                MessageBox.Show("Please enter Profilt", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Profilt.Focus();
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
                string cb = "insert into ProjectsWFM(projectCode,ClientName,ClientContact,startDate,Cost,profit,note,Duration,Status) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", ProjectCode.Text);
                cmd.Parameters.AddWithValue("@d2", ClientName.Text);
                cmd.Parameters.AddWithValue("@d3", ClientMobile.Text);
                cmd.Parameters.AddWithValue("@d4", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@d5", Cost.Text);
                cmd.Parameters.AddWithValue("@d6", Profilt.Text);
                cmd.Parameters.AddWithValue("@d7", txtnote.Text);
                cmd.Parameters.AddWithValue("@d8", Duration.Text);
                cmd.Parameters.AddWithValue("@d9", Status.Text);
                
                cmd.ExecuteReader();
                MessageBox.Show("Project Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
                //clear inputs After Save 

                ProjectCode.Text = "";
                ClientName.Text = "";
                txtnote.Text = "";
                ClientMobile.Text = "";
                Cost.Text = "";
                Profilt.Text = "";
                txtnote.Text = "";
                ProjectCode.Focus();
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
              string cq = "delete from ProjectsWFM where ID='" + ID.Text + "'";
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
                string cb = "update ProjectsWFM set projectCode=@d1,ClientName=@d2,startDate=@d3,Cost=@d4,profit=@d5,note=@d6,ClientContact=@d7,Duration=@d8,Status=@d9          where Id=@did";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", ProjectCode.Text);
                cmd.Parameters.AddWithValue("@d2", ClientName.Text);
                cmd.Parameters.AddWithValue("@d7", ClientMobile.Text);
                cmd.Parameters.AddWithValue("@d3", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@d4", Cost.Text);
                cmd.Parameters.AddWithValue("@d5", Profilt.Text);
                cmd.Parameters.AddWithValue("@d6", txtnote.Text);
                cmd.Parameters.AddWithValue("@d8", Duration.Text);
                cmd.Parameters.AddWithValue("@d9", Status.Text); 
                
                cmd.Parameters.AddWithValue("@did", ID.Text);
                cmd.ExecuteReader();
                MessageBox.Show("Project Successfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                cmd = new SqlCommand("SELECT RTRIM(ProjectsWFM.ID),RTRIM(projectCode),RTRIM(ClientName),RTRIM(ClientContact),RTRIM(startDate),RTRIM(Duration), RTRIM(Cost),RTRIM(profit),RTRIM(note),RTRIM(Status)     from ProjectsWFM", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8], rdr[9]);
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
        //    get into text back
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            ID.Text = dr.Cells[0].Value.ToString();
            ProjectCode.Text = dr.Cells[1].Value.ToString();
            ClientName.Text = dr.Cells[2].Value.ToString();
            ClientMobile.Text = dr.Cells[3].Value.ToString();
            dateTimePicker1.Text = dr.Cells[4].Value.ToString();
            Duration.Text = dr.Cells[5].Value.ToString(); 
            Cost.Text = dr.Cells[6].Value.ToString();
             Profilt.Text = dr.Cells[7].Value.ToString();
             txtnote.Text = dr.Cells[8].Value.ToString();

             Status.Text = dr.Cells[9].Value.ToString();


            
            // reset Buttons & force StaffID text
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            ProjectCode.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //code for search staff id
            //try
            //{
            //    con = new SqlConnection(cs.DBConn);
            //    con.Open();
            //    cmd = new SqlCommand("SELECT RTRIM(Leaves.ID),RTRIM(Employees.StaffID),RTRIM(empName),RTRIM(fromDate),RTRIM(toDate),RTRIM(reason),RTRIM(note)from Leaves, Employees where Leaves.StaffID = Employees.StaffID and  StaffID like '" + textBox1.Text + "%' order by StaffID", con);
            //    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    dataGridView1.Rows.Clear();
            //    while (rdr.Read() == true)
            //    {
            //        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6]);
            //    }
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }


    }
}
