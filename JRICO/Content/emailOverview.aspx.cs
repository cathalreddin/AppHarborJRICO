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
using System.Web.Security;

namespace JRICO.Content
{
    public partial class emailOverview : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!(Roles.IsUserInRole(Page.User.Identity.Name, "SuperUser"))) && (!(Roles.IsUserInRole(Page.User.Identity.Name, "Admin"))))
            {
                writeToLog.WriteLog("Redirected to Login from Email Overview Page: No authentication ", Page.User.Identity.Name, 0);
                Response.Redirect("Login.aspx");
            }
            else
            {
                writeToLog.WriteLog("Accessed the Email Overview Page ", Page.User.Identity.Name, 0);
                if (!IsPostBack)
                {
                    BindData("none", " ");
                    writeToLog.WriteLog("Email Overview List populated on first page", Page.User.Identity.Name, 0);
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Email Overview Row returned results for dropdwn: " + DropDownList1.SelectedValue + " and for TextSearch: " + TextSearch.Text, Page.User.Identity.Name, 0);
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

        private DataTable GetData(string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getEmailOverviewList";
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
                    Response.Write("ERROR 1001: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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
    }
}