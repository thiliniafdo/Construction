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

    public partial class ProjectReport : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
    
        public ProjectReport()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try   
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                ProjectsReport rpt = new ProjectsReport();
                //The report you created.
                cmd = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                WFM_ConstructionDataSet myDS = new WFM_ConstructionDataSet();
                //The DataSet you created.
                con = new SqlConnection(cs.DBConn);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from  ProjectsWFM where startDate Between @d1 and @d2 order by startDate";
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "startDate").Value = datefrom.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "startDate").Value = dateto.Value.Date; 
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "ProjectsWFM");
                rpt.SetDataSource(myDS);
               crystalReportViewer1.ReportSource = rpt;
            Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
