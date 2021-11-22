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
    public partial class MainMenu : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ConnectionString cs = new ConnectionString();

        public MainMenu()
        {
            InitializeComponent();
        }

     
        private void timer1_Tick(object sender, EventArgs e)
        {
            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();
        }
 
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            WFM_CONSTRUCTION_Login WFM_Construction2021 = new WFM_CONSTRUCTION_Login();
            WFM_Construction2021.Show();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            employeesCount();
            ProjectCount();
            profit();
           
        }



        public void employeesCount()
        {
            con = new SqlConnection(cs.DBConn);
            SqlCommand cmd = new SqlCommand("SELECT count (ID) as employeesCount FROM Employees ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    label6.Text = dr["employeesCount"].ToString();

                }
            } 

        }
        public void ProjectCount()
        {
            con = new SqlConnection(cs.DBConn);
            SqlCommand cmd = new SqlCommand("SELECT count (ID) as ProjectsWFM FROM ProjectsWFM ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    label7.Text = dr["ProjectsWFM"].ToString();

                }
            }

        }

        public void profit()
        {
            con = new SqlConnection(cs.DBConn);
            SqlCommand cmd = new SqlCommand("SELECT SUM (profit) as profit FROM ProjectsWFM ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    label8.Text = dr["profit"].ToString();

                }
            }

        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            Employee WFM_Construction2021 = new Employee();
            WFM_Construction2021.Show();
        }

       

        private void customerProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee WFM_Construction2021 = new Employee();
            WFM_Construction2021.Show();
        }
 
        
        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WFM_CONSTRUCTION_Login WFM_Construction2021 = new WFM_CONSTRUCTION_Login();
            WFM_Construction2021.Show();
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            Employee WFM_Construction2021 = new Employee();
            WFM_Construction2021.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmployeeLeaves WFM_Construction2021 = new EmployeeLeaves();
            WFM_Construction2021.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Projects_WFM WFM_Construction2021 = new Projects_WFM();
            WFM_Construction2021.Show();
        }
 

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            WFM_CONSTRUCTION_Login WFM_Construction2021 = new WFM_CONSTRUCTION_Login();
            WFM_Construction2021.Show();
        }
 
        private void button12_Click(object sender, EventArgs e)
        {

             
            ProjectReport WFM_Construction2021 = new ProjectReport();
            WFM_Construction2021.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            LeaveReport1 WFM_Construction2021 = new LeaveReport1();
            WFM_Construction2021.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PReport WFM_Construction2021 = new PReport();
            WFM_Construction2021.Show();
        }
        

    }
}
