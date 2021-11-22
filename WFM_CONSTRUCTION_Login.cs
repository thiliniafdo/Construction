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
    public partial class WFM_CONSTRUCTION_Login : Form
    {
        ConnectionString cs = new ConnectionString();
        DataTable dt = new DataTable();
        public WFM_CONSTRUCTION_Login()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // check your input
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            // function to make sure username & password
                try
                {
                    SqlConnection myConnection = default(SqlConnection);
                    myConnection = new SqlConnection(cs.DBConn);
                    SqlCommand myCommand = default(SqlCommand);
                    myCommand = new SqlCommand("SELECT Username,password FROM SystemUsers WHERE Username = @username AND password = @UserPassword AND UserType = 'Admin' ", myConnection);
                    SqlParameter uName = new SqlParameter("@username", SqlDbType.VarChar);
                    SqlParameter uPassword = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                    uName.Value = txtUserName.Text;
                    uPassword.Value = txtPassword.Text;
                    myCommand.Parameters.Add(uName);
                    myCommand.Parameters.Add(uPassword);
                    myCommand.Connection.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                   
                    // if login info valid
                    if (myReader.Read() == true)
                    {
                        this.Hide();
                        MainMenu frm = new MainMenu();
                        frm.Show();
                        frm.lblUser.Text = txtUserName.Text;
                    }

                    else
                    {
                        // if login info Invalid
                        MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();

                    }
                    if (myConnection.State == ConnectionState.Open)
                    {
                        myConnection.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
        //point username textbox when application start
            txtUserName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //close the applications
            this.Dispose();
            
        }
    }
}
