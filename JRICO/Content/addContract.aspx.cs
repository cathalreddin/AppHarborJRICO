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
    public partial class addContract : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            writeToLog.WriteLog("Add New Contract Selected ", Page.User.Identity.Name);            
        }
        protected void Cancel(object sender, EventArgs e)
        {
            writeToLog.WriteLog("Add New Contract Cancelled: ", Page.User.Identity.Name);
            Response.Redirect("/content/contractList.aspx");
        }
        protected void AddContract(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string sqlSP = "sp_insertContractList";

                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ContractReference", SqlDbType.NVarChar).Value = txtContractReference.Text;
                        cmd.Parameters.Add("@AssociatedRef", SqlDbType.Int).Value = Convert.ToInt32(ddlAssociatedReference.SelectedValue);
                        cmd.Parameters.Add("@RecordType", SqlDbType.Int).Value = Convert.ToInt32(ddlRecordType.SelectedValue);
                        cmd.Parameters.Add("@SystemPriceList", SqlDbType.NVarChar).Value = txtSystemPriceList.Text;
                        cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar).Value = txtContractTitle.Text;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value =txtPhone.Text;
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtStartDate.Text);
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEndDate.Text);
                        cmd.Parameters.Add("@ContractStatus", SqlDbType.Int).Value = Convert.ToInt32(ddlContractStatus.SelectedValue);
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        string query = "sp_insertContractList: ";
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        conn.Close();
                        writeToLog.WriteLog("Contract Row inserted with SP : " + query, Page.User.Identity.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("ERROR 1: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
            }
            Response.Redirect("/content/contractList.aspx");
        }
    }
}