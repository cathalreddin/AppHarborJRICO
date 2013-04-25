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
    public partial class details : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                if (Request.QueryString["id"] != null || Request.QueryString["id"] != "")
                {
                    getSingleContract(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }
        protected void getSingleContract(int ContractID)
        {
            string sql = "sp_getSingleContract";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ContractID", SqlDbType.Int);
                cmd.Parameters["@ContractID"].Value = ContractID;
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        lblContractReference.Text = dr["Contract Reference"].ToString();
                        lblAssociatedReference.Text = dr["Associated Ref"].ToString();
                        lblRecordType.Text = dr["Record Type"].ToString();
                        lblSystemPriceList.Text = dr["System Price List"].ToString();
                        lblContractTitle.Text = dr["Contract Title"].ToString();
                        lblContractStatus.Text = dr["Contract Status"].ToString();
                        lblName.Text = dr["Name"].ToString();
                        lblEmail.Text = dr["Email"].ToString();
                        lblPhone.Text = dr["Phone"].ToString();
                        lblStartDate.Text = dr["Start Date"].ToString();
                        lblEndDate.Text = dr["End Date"].ToString();
                        lblUploadedBy.Text = dr["Uploaded By"].ToString();
                        lblDateUploaded.Text = dr["Date Uploaded"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }           
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
            //if (Request.QueryString["ContractID"] != null && Request.QueryString["ContractID"] != "")
            //{
            //    getSingleContract(Convert.ToInt32(Request.QueryString["ContractID"]));
            //}
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 2;
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Clicked";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 3;
        }

        protected void Tab5_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Clicked";
            MainView.ActiveViewIndex = 4;
        }
    }
}