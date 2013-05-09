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
using AjaxControlToolkit;
using System.Text;

namespace JRICO.Content
{
    public partial class hospitalList : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                writeToLog.WriteLog("Redirected to login from hospitalList page: No authentication ", Page.User.Identity.Name, 0);
                Response.Redirect("../Account/Login.aspx");
            }
            else
            {
                writeToLog.WriteLog("Accessed the hospitalList Page ", Page.User.Identity.Name, 0);      
                if (!IsPostBack)
                {
                    BindData("none", " ");
                    writeToLog.WriteLog("Hospital List populated on first page", Page.User.Identity.Name, 1);
                }
            }
        }
        //popup
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

                string behaviorID = "pce_" + e.Row.RowIndex;
                pce.BehaviorID = behaviorID;

                Label lblPostCode = (Label)e.Row.FindControl("Label5");

                string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                lblPostCode.Attributes.Add("onmouseover", OnMouseOverScript);
                lblPostCode.Attributes.Add("onmouseout", OnMouseOutScript);
            }
        }
        [System.Web.Services.WebMethodAttribute(),
   System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetDynamicContent(string contextKey)
        {
            StringBuilder b = new StringBuilder();

            b.Append("<table style='width:350px; font-size:10pt; font-family:Verdana;'>");

            b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>");
            b.Append("<b>MAP</b>"); 
            b.Append("</td></tr>");
            b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>");
            b.Append("<b>In construction - show map here</b>");
            b.Append("</td></tr>");
            b.Append("</table>");

            return b.ToString();
        }
        private void BindData(string column, string textSearch)
        {
            //Sorting Issue #1
            if (ViewState["sortExpression"] != null)
            {
                DataTable table = this.GetData(column, textSearch);
                table.DefaultView.Sort = ViewState["sortExpression"].ToString() + ViewState["direction"].ToString();
                GridView1.DataSource = table;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = this.GetData(column, textSearch);
                GridView1.DataBind();
            }            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Hospital Row returned results for dropdwn: " + DropDownList1.SelectedValue + " and for TextSearch: " + TextSearch.Text, Page.User.Identity.Name, 0);
        }

        private DataTable GetData(string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getHospitalList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Column", column));
                        cmd.Parameters.Add(new SqlParameter("@TextSearch", textSearch));
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(table);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 18: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }

            return table;
        }
        protected void RowEdit(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Hospital Row with Index:" + e.NewEditIndex.ToString() + " edit link clicked", Page.User.Identity.Name, 0);
        }
        protected void RowEditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // reseting grid view
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Hospital Row cancelled for edit", Page.User.Identity.Name, 0);
        }
        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {
            // Retrieve the row being edited. 
            int index = GridView1.EditIndex;
            GridViewRow row = GridView1.Rows[index];
            TextBox HospitalName = row.FindControl("txtHospitalName") as TextBox;
            TextBox Address1 = row.FindControl("txtAddress1") as TextBox;
            TextBox Postcode = row.FindControl("txtPostcode") as TextBox;
            TextBox AccountNumber = row.FindControl("txtAccountNumber") as TextBox;
            string HospitalID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_updateHospitalList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@HospitalName", SqlDbType.NVarChar).Value = HospitalName.Text;
                        cmd.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = Address1.Text;
                        cmd.Parameters.Add("@Postcode", SqlDbType.NVarChar).Value = Postcode.Text;
                        cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar).Value = AccountNumber.Text;
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = Convert.ToInt32(HospitalID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string query = "sp_updateHospitalList:";
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView1.EditIndex = -1;
                        BindData(DropDownList1.SelectedValue, TextSearch.Text);
                        conn.Close();
                        writeToLog.WriteLog("Hospital Row updated with SP : " + query, Page.User.Identity.Name, 1);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 19: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }



        protected void SortRecords(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string direction = string.Empty;
            if (sortDirection == SortDirection.Ascending)
            {
                sortDirection = SortDirection.Descending;
                direction = " DESC ";
            }
            else
            {
                sortDirection = SortDirection.Ascending;
                direction = " ASC ";
            }
            DataTable table = this.GetData(DropDownList1.SelectedValue, TextSearch.Text);
            table.DefaultView.Sort = sortExpression + direction;

            GridView1.DataSource = table;
            GridView1.DataBind();
            //Sorting Issue #1
            ViewState["sortExpression"] = sortExpression.ToString();
            ViewState["direction"] = direction.ToString();

            writeToLog.WriteLog("User Sorts on " + sortExpression + " " + direction, Page.User.Identity.Name, 0);
        }

        public SortDirection sortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            string query = "sp_insertHospitalList: "; 
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_insertHospitalList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@HospitalName", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtHospitalNameInsert")).Text;
                        cmd.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtAddress1Insert")).Text;
                        cmd.Parameters.Add("@Postcode", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtPostcodeInsert")).Text;
                        cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtAccountNumberInsert")).Text;
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView1.EditIndex = -1;
                        BindData("none", " ");
                        conn.Close();
                        writeToLog.WriteLog("Hospital Row inserted with SP : " + query, Page.User.Identity.Name, 1);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 20: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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