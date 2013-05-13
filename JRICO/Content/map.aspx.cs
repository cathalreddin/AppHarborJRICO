using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JRICO.CodeArea;

namespace JRICO.Content
{
    public partial class map : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "sp_getHospitalDetails";
            if (Request.QueryString["id"] != null)
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@HospitalID", SqlDbType.Int);
                    cmd.Parameters["@HospitalID"].Value = Request.QueryString["id"];
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read())
                        {
                            lblHospitalName.Text = dr["HospitalName"].ToString();
                            lblAddress1.Text = dr["Address1"].ToString();
                            lblPostcode.Text = dr["Postcode"].ToString();
                            lblAccountNumber.Text = dr["AccountNumber"].ToString();
                            txtlat.Text = dr["Latitude"].ToString();
                            txtlon.Text = dr["Longitude"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("ERROR 606: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                    }
                    finally
                    {
                        // Close data reader object and database connection
                        if (conn != null)
                            conn.Close();
                    }
                }
            }
        }
    }
}