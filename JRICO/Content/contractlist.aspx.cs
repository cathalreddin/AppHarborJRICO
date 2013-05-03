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
    public partial class contractList : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.Identity.IsAuthenticated))
            {
               writeToLog.WriteLog("Redirected to login from contractList page: No authentication ", Page.User.Identity.Name);
               Response.Redirect("/Account/Login.aspx");
            }
            else
            {
                writeToLog.WriteLog("Accessed the contractList Page ", Page.User.Identity.Name);
                if (!IsPostBack)
                {
                    BindData("none", " ");
                    writeToLog.WriteLog("List populated on first page", Page.User.Identity.Name);
                    CheckForEmailTriggers();
                    if ((Request.QueryString["m"] != null) && (Request.QueryString["m"] == "new"))
                    {
                        lblNewUser.Visible = true;
                        lblNewUser.Text = "Success - New User Added";
                        lblNewUser.Font.Italic = true;
                        lblNewUser.Font.Bold = true;
                        lblNewUser.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        protected void CheckForEmailTriggers()
        {       
            Email newEmail = new Email();
            //loop through sending emails
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getListEmailsToSendNow";
                using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read())
                        {
                            //Send Email                            
                            int EmailID = Convert.ToInt32(dr["EmailID"]);
                            string emailTo = dr["EmailTo"].ToString();
                            string EmailFrom = dr["EmailFrom"].ToString();
                            string EmailSubject = dr["EmailSubject"].ToString();
                            string EmailContent = dr["EmailContent"].ToString();
                            int ContractID = Convert.ToInt32(dr["ContractID"]);
                            string result = newEmail.SendEmail(emailTo, "[Contract Title: " + dr["ContractTitle"].ToString() + "] " +EmailSubject, EmailContent, ContractID);

                            using (SqlConnection connEmail = new SqlConnection(_connStr))
                            {
                                using (SqlCommand cmdEmail = new SqlCommand("sp_updateTriggerEmailSent", connEmail))
                                {
                                    try
                                    {
                                        //Update Sent Status
                                        cmdEmail.CommandType = CommandType.StoredProcedure;
                                        cmdEmail.Parameters.Add("@EmailID", SqlDbType.Int).Value = Convert.ToInt32(EmailID);
                                        cmdEmail.Parameters.Add("@SentMessage", SqlDbType.NVarChar).Value = result;                                        
                                        connEmail.Open();
                                        cmdEmail.ExecuteNonQuery();
                                        string query = "sp_updateTriggerEmailSent: ";
                                        foreach (SqlParameter p in cmd.Parameters)
                                        {
                                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                                        }
                                        connEmail.Close();
                                        writeToLog.WriteLog("Trigger Email attempted to Send : " + query, Page.User.Identity.Name);
                                        connEmail.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        Response.Write("ERROR 2: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                                    }
                                    finally
                                    {
                                        // Close data reader object and database connection
                                        if (connEmail != null)
                                            connEmail.Close();
                                    }
                                }
                            }                            
                       }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("ERROR 3: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Row returned results for dropdwn: " + DropDownList1.SelectedValue + " and for TextSearch: " + TextSearch.Text, Page.User.Identity.Name);
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
                string sqlSP = "sp_getContractList";
                using (SqlCommand cmd = new SqlCommand(sqlSP, conn))                
                {
                    try
                    {
                        cmd.Parameters.Add(new SqlParameter("@Column", column));
                        cmd.Parameters.Add(new SqlParameter("@TextSearch", textSearch));
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(table);
                        }
                    }                
                    catch (Exception ex)
                    {
                        Response.Write("ERROR 4: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                    }
                    finally
                    {
                        // Close data reader object and database connection
                        if (conn != null)
                            conn.Close();
                    }
                }
            }
            return table;
        }
        protected void RowEdit(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Row with Index:" + e.NewEditIndex.ToString() + " edit link clicked", Page.User.Identity.Name);
        }
        protected void RowEditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // reseting grid view
            BindData(DropDownList1.SelectedValue, TextSearch.Text); 
            writeToLog.WriteLog("Row cancelled for edit", Page.User.Identity.Name);      
        }
        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {
                // Retrieve the row being edited. 
                int index = GridView1.EditIndex;
                GridViewRow row = GridView1.Rows[index];
                TextBox ContractReference = row.FindControl("TextBox1") as TextBox;
                DropDownList AssociatedRef = row.FindControl("ddlAssociatedRef") as DropDownList;
                DropDownList RecordType = row.FindControl("ddlRecordType") as DropDownList;
                TextBox SystemPriceList = row.FindControl("TextBox4") as TextBox;
                TextBox ContractTitle = row.FindControl("TextBox5") as TextBox;
                TextBox Name = row.FindControl("TextBox6") as TextBox;
                TextBox Email = row.FindControl("TextBox7") as TextBox;
                TextBox Phone = row.FindControl("TextBox8") as TextBox;
                TextBox StartDate = row.FindControl("TextBox9") as TextBox;
                TextBox EndDate = row.FindControl("TextBox10") as TextBox;
                TextBox SubmissionDate = row.FindControl("TextBox11") as TextBox;
                DropDownList ContractStatus = row.FindControl("ddlContractStatus") as DropDownList;
                string ContractID = GridView1.DataKeys[e.RowIndex].Value.ToString();

                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    try
                    {
                        string sqlSP = "sp_updateContractList";
                        using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ContractReference", SqlDbType.NVarChar).Value = ContractReference.Text;
                            cmd.Parameters.Add("@AssociatedRef", SqlDbType.Int).Value = Convert.ToInt32(AssociatedRef.Text);
                            cmd.Parameters.Add("@RecordType", SqlDbType.Int).Value = Convert.ToInt32(RecordType.Text);
                            cmd.Parameters.Add("@SystemPriceList", SqlDbType.NVarChar).Value = SystemPriceList.Text;
                            cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar).Value = ContractTitle.Text;
                            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name.Text;
                            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email.Text;
                            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone.Text;
                            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(StartDate.Text);
                            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(EndDate.Text);
                            cmd.Parameters.Add("@ContractStatus", SqlDbType.Int).Value = Convert.ToInt32(ContractStatus.Text);
                            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                            cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = Convert.ToInt32(ContractID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            string query = "sp_updateContractList:";
                            foreach (SqlParameter p in cmd.Parameters)
                            {
                                query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                            }
                            GridView1.EditIndex = -1;
                            BindData(DropDownList1.SelectedValue, TextSearch.Text);
                            conn.Close();
                            writeToLog.WriteLog("Row updated with SP : " + query, Page.User.Identity.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("ERROR 5: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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

            writeToLog.WriteLog("User Sorts on " + sortExpression + " " + direction, Page.User.Identity.Name);
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