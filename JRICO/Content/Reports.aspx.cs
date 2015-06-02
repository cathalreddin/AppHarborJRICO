using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace JRICO.Content
{
    public partial class Reports : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string queryStr = "SELECT * from Contracts";
            //SqlDataAdapter sda = new SqlDataAdapter(queryStr, _connStr);
            //sda.Fill(dt);
            //ExportTableData(dt);
            GetData();
        }
        private void GetData()
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getReport1";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@Column", column));
                        //cmd.Parameters.Add(new SqlParameter("@TextSearch", textSearch));
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(table);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 99: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
            ExportTableData(table);
        }

        // this does all the work to export to excel
        public void ExportTableData(DataTable dtdata)
        {
            string attach = "attachment;filename=report1.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attach);
            Response.ContentType = "application/ms-excel";
            if (dtdata != null)
            {
                foreach (DataColumn dc in dtdata.Columns)
                {
                    Response.Write(dc.ColumnName + "\t");
                    //sep = ";";
                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dtdata.Rows)
                {
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
    }
}