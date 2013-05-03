using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JRICO.CodeArea;
using SendGridMail;
using SendGridMail.Transport;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.IO;
using Amazon.S3.Model;
using Amazon.S3;

namespace JRICO.Content
{
    public partial class details : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        private const string accessKey = "AKIAI6URINAZNMO2P3CQ";
        private const string secretKey = "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";
        int id = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                writeToLog.WriteLog("Redirected to login from details page: No authentication ", Page.User.Identity.Name);
                Response.Redirect("../Account/Login.aspx");
            }
            else
            {
                writeToLog.WriteLog("Accessed the details Page ", Page.User.Identity.Name);
                id = Convert.ToInt32(Request.QueryString["id"]);
                if (!IsPostBack)
                {
                    Tab1.CssClass = "Clicked";
                    MainView.ActiveViewIndex = 0;
                    if (id != 0)
                    {
                        getSingleContract(id);
                        BindDataPrice(id, "none", " ");
                        BindDataAccountNumber(id, "none", " ");
                        BindDataAttachment(id, "none", " ");
                        BindDataNote(id, "none", " ");
                        EmailViewData();
                        getEmailDetails(id);
                        hlMessage.Visible = false;
                        writeToLog.WriteLog("Contract ID: " + id.ToString() + " selected", Page.User.Identity.Name);
                    }
                    else
                    {
                        hlMessage.Text = "OOOOPS... NO DATA SELECTED PLEASE CLICK HERE AND SELECT DATA AGAIN";
                        hlMessage.Visible = true;
                        hlMessage.Font.Italic = true;
                        hlMessage.Font.Bold = true;
                        hlMessage.ForeColor = System.Drawing.Color.Red;
                        hlMessage.NavigateUrl = "contractList.aspx";
                        lblContractReferenceHeading.Visible = false;
                        lblContractTitleHeading.Visible = false;
                        lblContractReferenceLabel.Visible = false;
                        lblContractTitleLabel.Visible = false;
                    }
                }
            }
        }
        //*** START OF TAB 1 ***
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
                        lblStartDate.Text = dr["Start Date"].ToString() == "" ? "" : Convert.ToDateTime(dr["Start Date"].ToString()).ToShortDateString();
                        lblEndDate.Text = dr["End Date"].ToString() == "" ? "" : Convert.ToDateTime(dr["End Date"].ToString()).ToShortDateString();
                        lblUploadedBy.Text = dr["Uploaded By"].ToString();
                        lblDateUploaded.Text = dr["Date Uploaded"].ToString() == "" ? "" : Convert.ToDateTime(dr["Date Uploaded"].ToString()).ToShortDateString();
                        lblContractReferenceHeading.Text = dr["Contract Reference"].ToString();
                        lblContractTitleHeading.Text = dr["Contract Title"].ToString();
                        lblContractReferenceHeading.Font.Bold = true;
                        lblContractTitleHeading.Font.Bold = true;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 6: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        // *** END OF TAB 1 ***
        //******************************************************************************************************************
        // *** START OF TAB 2 ***
        private void BindDataAttachment(int contractID, string column, string textSearch)
        {
            //Sorting Issue #1
            if (ViewState["sortExpressionAttachment"] != null)
            {
                DataTable table = this.GetDataAttachment(contractID, column, textSearch);
                table.DefaultView.Sort = ViewState["sortExpressionAttachment"].ToString() + ViewState["directionAttachment"].ToString();
                GridView2.DataSource = table;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = this.GetDataAttachment(contractID, column, textSearch);
                GridView2.DataBind();
            }
        }

        private DataTable GetDataAttachment(int contractID, string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getAttachmentList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ContractID", contractID));
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
                    Response.Write("ERROR 7: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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

        protected void Button_Attachment(object sender, EventArgs e)
        {
            BindDataAttachment(id, DropDownListAttachment.SelectedValue, txtAttachmentSearch.Text);
            writeToLog.WriteLog("Attachment Row returned results for dropdwn: " + DropDownListAttachment.SelectedValue + " and for TextSearch: " + txtAttachmentSearch.Text, Page.User.Identity.Name);
        }
        protected void SortRecordsAttachment(object sender, GridViewSortEventArgs e)
        {
            string sortExpressionAttachment = e.SortExpression;
            string directionAttachment = string.Empty;
            if (sortDirectionAttachment == SortDirection.Ascending)
            {
                sortDirectionAttachment = SortDirection.Descending;
                directionAttachment = " DESC ";
            }
            else
            {
                sortDirectionAttachment = SortDirection.Ascending;
                directionAttachment = " ASC ";
            }
            DataTable table = this.GetDataAttachment(id, DropDownListAttachment.SelectedValue, txtAttachmentSearch.Text);
            table.DefaultView.Sort = sortExpressionAttachment + directionAttachment;

            GridView2.DataSource = table;
            GridView2.DataBind();

            //Sorting Issue #1
            ViewState["sortExpressionAttachment"] = sortExpressionAttachment.ToString();
            ViewState["directionAttachment"] = directionAttachment.ToString();


            writeToLog.WriteLog("User Sorts on " + sortExpressionAttachment + " " + directionAttachment, Page.User.Identity.Name);
        }
        public SortDirection sortDirectionAttachment
        {
            get
            {
                if (ViewState["SortDirectionAttachment"] == null)
                {
                    ViewState["SortDirectionAttachment"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirectionAttachment"];
            }
            set
            {
                ViewState["SortDirectionAttachment"] = value;
            }
        }

        protected void DownloadApplicationFile(object sender, CommandEventArgs e)
        {
            try
            {
                string keyn = "333-" + id.ToString() + "/";
                string attachment = e.CommandArgument.ToString();
                string dest = Path.Combine(HttpRuntime.CodegenDir, attachment);

                AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(
                        accessKey, secretKey);
                keyn = keyn + attachment;
                GetObjectRequest request = new GetObjectRequest()
                            .WithBucketName("JRIFiles").WithKey(keyn);

                using (GetObjectResponse response = client.GetObject(request))
                {
                    response.WriteResponseStreamToFile(dest, false);
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + attachment);
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.TransmitFile(dest);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();

                // Clean up temporary file.
                System.IO.File.Delete(dest);
                writeToLog.WriteLog("file " + keyn + attachment + " downloaded", Page.User.Identity.Name);        
            }
            catch (Exception ex)
            {
                Response.Write("ERROR 21: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
            }
        }
        protected void lbInsert_Attachment(object sender, EventArgs e)
        {
            string query = "sp_insertAttachmentList: ";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                try
                {
                    string sqlSP = "sp_insertAttachmentList";
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AttachmentTitle", SqlDbType.NVarChar).Value = ((TextBox)GridView2.FooterRow.FindControl("txtAttachmentTitleInsert")).Text;
                        cmd.Parameters.Add("@AttachmentName", SqlDbType.NVarChar).Value = ((FileUpload)GridView2.FooterRow.FindControl("fAttachmentNameInsert")).FileName;
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = id;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }

                        if (((FileUpload)GridView2.FooterRow.FindControl("fAttachmentNameInsert")).FileContent.Length > 0) // accept the file
                        {
                            AmazonS3 client;
                            using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey))
                            {
                                //int c = ((FileUpload)GridView2.FooterRow.FindControl("fAttachmentNameInsert")).FileName.LastIndexOf('\\');
                                //string AName = ((FileUpload)GridView2.FooterRow.FindControl("fAttachmentNameInsert")).FileName.Substring(c + 1);
                                string AName = ((FileUpload)GridView2.FooterRow.FindControl("fAttachmentNameInsert")).FileName;
                                MemoryStream ms = new MemoryStream();
                                PutObjectRequest request = new PutObjectRequest();
                                request.WithBucketName("JRIFiles")
                               .WithCannedACL(S3CannedACL.PublicRead)
                               .WithKey("333-" + id.ToString() + "/" + AName).InputStream = ((FileUpload)GridView2.FooterRow.FindControl("fAttachmentNameInsert")).PostedFile.InputStream;
                                S3Response response = client.PutObject(request);
                                writeToLog.WriteLog("file 333-" + id.ToString() + "/" + AName  +" uploaded", Page.User.Identity.Name);  
                            }
                        }
                        GridView2.EditIndex = -1;
                        BindDataAttachment(id, DropDownListAttachment.SelectedValue, txtAttachmentSearch.Text);
                        conn.Close();
                        writeToLog.WriteLog("Attachment Row inserted with SP : " + query, Page.User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 8: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }
       

        // *** END OF TAB 2 ***
        //******************************************************************************************************************
        // *** START OF TAB 3 ***


        protected void lbEdit_Email(object sender, EventArgs e)
        {
            lblEmailTo.Visible = false;
            lblEmailDate.Visible = false;
            lblEmailSubject.Visible = false;
            lblEmailContent.Visible = false;
            txtEmailTo.Visible = true;
            txtEmailDate.Visible = true;
            txtEmailSubject.Visible = true;
            txtEmailContent.Visible = true;
            lbUpdateEmail.Visible = true;
            lbCancelEmail.Visible = true;
            lbEditEmail.Visible = false;
            lblEmailDateRFC.Text = "";
            lblEmailToRFC.Text = "";
            lblEmailSubjectRFC.Text = "";
            lblEmailContentRFC.Text = "";
        }
        protected void EmailViewData()
        {
            lblEmailTo.Visible = true;
            lblEmailDate.Visible = true;
            lblEmailSubject.Visible = true;
            lblEmailContent.Visible = true;
            lblEmailStatus.Visible = true;
            lblEmailStatusHeading.Visible = true;
            txtEmailTo.Visible = false;
            txtEmailDate.Visible = false;
            txtEmailSubject.Visible = false;
            txtEmailContent.Visible = false;
            lbUpdateEmail.Visible = false;
            lbCancelEmail.Visible = false;
            lbEditEmail.Visible = true; 
            lblEmailDateRFC.Text = "";
            lblEmailToRFC.Text = "";
            lblEmailSubjectRFC.Text = "";
            lblEmailContentRFC.Text = "";
        }

        protected void lbUpdate_Email(object sender, EventArgs e)
        {
            if ((txtEmailDate.Text != "*") && (txtEmailDate.Text != "") && (txtEmailTo.Text != "") && (txtEmailSubject.Text != "") && (txtEmailContent.Text != ""))
            {
                // Retrieve the row being edited.                
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    try
                    {
                        Email newEmail = new Email();
                        string result = newEmail.SendEmailToCathal(txtEmailTo.Text, "[New Email Trigger Set for Contract Title: " +lblContractTitle.Text+ "] ", "EmailID: [" + lblEmailID.Text + " ]: " + txtEmailContent.Text, Convert.ToDateTime(txtEmailDate.Text), id);
                        string sqlSP = "sp_updateEmail"; 

                        if (lblEmailID.Text != "")
                        {
                            using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@EmailTo", SqlDbType.NVarChar).Value = txtEmailTo.Text;
                                cmd.Parameters.Add("@EmailDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEmailDate.Text);
                                cmd.Parameters.Add("@EmailSubject", SqlDbType.NVarChar).Value = txtEmailSubject.Text;
                                cmd.Parameters.Add("@EmailContent", SqlDbType.NVarChar).Value = txtEmailContent.Text;
                                cmd.Parameters.Add("@AdminSentMessage", SqlDbType.NVarChar).Value = result;
                                cmd.Parameters.Add("@EmailID", SqlDbType.Int).Value = Convert.ToInt32(lblEmailID.Text);
                                cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                string query = "sp_updateEmail:";
                                foreach (SqlParameter p in cmd.Parameters)
                                {
                                    query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                                }
                                conn.Close();
                                getEmailDetails(id);
                                EmailViewData();
                                writeToLog.WriteLog("Update Email Row with SP : " + query, Page.User.Identity.Name);
                                writeToLog.WriteLog("Admin Updated Email [" + lblEmailID.Text + "] attempted - result : " + result, Page.User.Identity.Name);
                            }
                        }
                        else
                        {
                            sqlSP = "sp_insertEmail";
                            using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@EmailTo", SqlDbType.NVarChar).Value = txtEmailTo.Text;
                                cmd.Parameters.Add("@EmailDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEmailDate.Text);
                                cmd.Parameters.Add("@EmailSubject", SqlDbType.NVarChar).Value = txtEmailSubject.Text;
                                cmd.Parameters.Add("@EmailContent", SqlDbType.NVarChar).Value = txtEmailContent.Text;
                                cmd.Parameters.Add("@AdminSentMessage", SqlDbType.NVarChar).Value = result;
                                cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = id;
                                cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                string query = "sp_insertEmail:";
                                foreach (SqlParameter p in cmd.Parameters)
                                {
                                    query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                                }
                                conn.Close();
                                getEmailDetails(id);
                                EmailViewData();
                                writeToLog.WriteLog("Insert Email Row with SP : " + query, Page.User.Identity.Name);
                                writeToLog.WriteLog("Admin New Email attempted - result : " + result, Page.User.Identity.Name);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("ERROR 9: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                    }
                    finally
                    {
                        // Close data reader object and database connection
                        if (conn != null)
                            conn.Close();
                    }
                }
            }
            else
            {
                lblEmailDateRFC.Text = "";
                lblEmailToRFC.Text = "";
                lblEmailSubjectRFC.Text = "";
                lblEmailContentRFC.Text = "";

                if ((txtEmailDate.Text == "*") || (txtEmailDate.Text == ""))
                {
                    lblEmailDateRFC.Text = "*";
                    lblEmailDateRFC.Font.Bold = true;
                    lblEmailDateRFC.ForeColor = System.Drawing.Color.Red;
                }
                if (txtEmailTo.Text == "")
                {
                    lblEmailToRFC.Text = "*";
                    lblEmailToRFC.Font.Bold = true;
                    lblEmailToRFC.ForeColor = System.Drawing.Color.Red;
                }
                if (txtEmailSubject.Text == "")
                {
                    lblEmailSubjectRFC.Text = "*";
                    lblEmailSubjectRFC.Font.Bold = true;
                    lblEmailSubjectRFC.ForeColor = System.Drawing.Color.Red;
                }
                if (txtEmailContent.Text == "")
                {
                    lblEmailContentRFC.Text = "*";
                    lblEmailContentRFC.Font.Bold = true;
                    lblEmailContentRFC.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
       

        protected void lbCancel_Email(object sender, EventArgs e)
        {
            getEmailDetails(id);
            EmailViewData();
        }
        protected void getEmailDetails(int ContractID)
        {
            string sql = "sp_getEmailDetails";
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
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lblEmailID.Text = dr["EmailID"].ToString();
                            lblEmailTo.Text = dr["EmailTo"].ToString();
                            lblEmailDate.Text = Convert.ToDateTime(dr["EmailDate"].ToString()).ToShortDateString();
                            lblEmailSubject.Text = dr["EmailSubject"].ToString();
                            lblEmailContent.Text = dr["EmailContent"].ToString();
                            txtEmailTo.Text = dr["EmailTo"].ToString();
                            txtEmailDate.Text = Convert.ToDateTime(dr["EmailDate"].ToString()).ToShortDateString();
                            txtEmailSubject.Text = dr["EmailSubject"].ToString();
                            txtEmailContent.Text = dr["EmailContent"].ToString();
                            string emailStatus = dr["SentMessage"].ToString();
                            if (emailStatus == "Email Sent")
                            {
                               lblEmailStatus.Text = "Email Sent"; 
                            }
                            else if (emailStatus == "")
                            {
                                lblEmailStatus.Text = "Awaiting Trigger Date";
                            }
                            else 
                            {
                                lblEmailStatus.Text = "Error Sending Email - Contact Administrator";                             
                            }
                        }
                    }
                    else
                    {
                        lblEmailTo.Visible = false;
                        lblEmailDate.Visible = false;
                        lblEmailSubject.Visible = false;
                        lblEmailContent.Visible = false;
                        txtEmailTo.Visible = true;
                        txtEmailDate.Visible = true;
                        txtEmailSubject.Visible = true;
                        txtEmailContent.Visible = true;
                        lblEmailStatusHeading.Visible = false;
                        lbUpdateEmail.Visible = true;
                        lbCancelEmail.Visible = true;
                        lbEditEmail.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 10: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        protected void Button_Note(object sender, EventArgs e)
        {
            BindDataNote(id, DropDownListNote.SelectedValue, txtNoteSearch.Text);
            writeToLog.WriteLog("Note Row returned results for dropdwn: " + DropDownListNote.SelectedValue + " and for TextSearch: " + txtNoteSearch.Text, Page.User.Identity.Name);
        }
        private void BindDataNote(int contractID, string column, string textSearch)
        {
             //Sorting Issue #1
            if (ViewState["sortExpressionNote"] != null)
            {
                DataTable table = this.GetDataNote(contractID, column, textSearch);
                table.DefaultView.Sort = ViewState["sortExpressionNote"].ToString() + ViewState["directionNote"].ToString();
                GridView3.DataSource = table;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = this.GetDataNote(contractID, column, textSearch);
                GridView3.DataBind();
            }
        }
        protected IEnumerable GridView3_MustAddARow(IEnumerable data) 
        {
		    List<int> dds = (List<int>)data;
		    dds.Add(new int());
		    return dds;
	    }
        private DataTable GetDataNote(int contractID, string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getNoteList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ContractID", contractID));
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
                    Response.Write("ERROR 11: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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

        protected void SortRecordsNote(object sender, GridViewSortEventArgs e)
        {
            string sortExpressionNote = e.SortExpression;
            string directionNote = string.Empty;
            if (sortDirectionNote == SortDirection.Ascending)
            {
                sortDirectionNote = SortDirection.Descending;
                directionNote = " DESC ";
            }
            else
            {
                sortDirectionNote = SortDirection.Ascending;
                directionNote = " ASC ";
            }
            DataTable table = this.GetDataNote(id, DropDownListNote.SelectedValue, txtNoteSearch.Text);
            table.DefaultView.Sort = sortExpressionNote + directionNote;

            GridView3.DataSource = table;
            GridView3.DataBind();
            //Sorting Issue #1
            ViewState["sortExpressionNote"] = sortExpressionNote.ToString();
            ViewState["directionNote"] = directionNote.ToString();

            writeToLog.WriteLog("User Sorts on " + sortExpressionNote + " " + directionNote, Page.User.Identity.Name);
        }
        public SortDirection sortDirectionNote
        {
            get
            {
                if (ViewState["SortDirectionNote"] == null)
                {
                    ViewState["SortDirectionNote"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirectionNote"];
            }
            set
            {
                ViewState["SortDirectionNote"] = value;
            }
        }
        protected void lbInsert_Note(object sender, EventArgs e)
        {
            string query = "sp_insertNoteList: ";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_insertNoteList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = ((TextBox)GridView3.FooterRow.FindControl("txtNoteTitleInsert")).Text;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ((TextBox)GridView3.FooterRow.FindControl("txtNoteDescriptionInsert")).Text;
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = id;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView3.EditIndex = -1;
                        BindDataNote(id, "none", " ");
                        conn.Close();
                        writeToLog.WriteLog("Note Row inserted with SP : " + query, Page.User.Identity.Name);
                    }
                }

                catch (Exception ex)
                {
                    Response.Write("ERROR 12: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        // *** END OF TAB 3 ***             
        //******************************************************************************************************************
        // *** START OF TAB 4 ***
        private void BindDataPrice(int contractID, string column, string textSearch)
        {
            //Sorting Issue #1
            if (ViewState["sortExpressionPrice"] != null)
            {
                DataTable table = this.GetDataPrice(contractID, column, textSearch);
                table.DefaultView.Sort = ViewState["sortExpressionPrice"].ToString() + ViewState["directionPrice"].ToString();
                GridView4.DataSource = table;
                GridView4.DataBind();
            }
            else
            {
                GridView4.DataSource = this.GetDataPrice(contractID, column, textSearch);
                GridView4.DataBind();
            }
        }

        private DataTable GetDataPrice(int contractID, string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getPriceList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ContractID", contractID));
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
                    Response.Write("ERROR 13: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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
        protected void RowEditPrice(object sender, GridViewEditEventArgs e)
        {
            GridView4.EditIndex = e.NewEditIndex;
            BindDataPrice(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
            writeToLog.WriteLog("Price Row with Index:" + e.NewEditIndex.ToString() + " edit link clicked", Page.User.Identity.Name);
        }
        protected void RowEditCancelPrice(object sender, GridViewCancelEditEventArgs e)
        {
            GridView4.EditIndex = -1; // reseting grid view
            BindDataPrice(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
            writeToLog.WriteLog("Price Row cancelled for edit", Page.User.Identity.Name);
        }

        protected void RowUpdatePrice(object sender, GridViewUpdateEventArgs e)
        {

            // Retrieve the row being edited. 
            int index = GridView4.EditIndex;
            GridViewRow row = GridView4.Rows[index];
            TextBox Subject = row.FindControl("txtSubject") as TextBox;
            TextBox Description = row.FindControl("txtDescription") as TextBox;
            TextBox Price = row.FindControl("txtPrice") as TextBox;
            string PriceID = GridView4.DataKeys[e.RowIndex].Value.ToString();

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                try
                {
                    string sqlSP = "sp_updatePriceList";

                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = Subject.Text;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description.Text;
                        cmd.Parameters.Add("@Price", SqlDbType.NVarChar).Value = Price.Text;
                        cmd.Parameters.Add("@PriceID", SqlDbType.Decimal).Value = Convert.ToDecimal(PriceID);
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string query = "sp_updatePriceList:";
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView4.EditIndex = -1;
                        BindDataPrice(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
                        conn.Close();
                        writeToLog.WriteLog("Price Row updated with SP : " + query, Page.User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 14: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }

        protected void Button_Price(object sender, EventArgs e)
        {
            BindDataPrice(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
            writeToLog.WriteLog("Price Row returned results for dropdwn: " + DropDownListPrice.SelectedValue + " and for TextSearch: " + txtPriceSearch.Text, Page.User.Identity.Name);
        }
        protected void SortRecordsPrice(object sender, GridViewSortEventArgs e)
        {
            string sortExpressionPrice = e.SortExpression;
            string directionPrice = string.Empty;
            if (sortDirectionPrice == SortDirection.Ascending)
            {
                sortDirectionPrice = SortDirection.Descending;
                directionPrice = " DESC ";
            }
            else
            {
                sortDirectionPrice = SortDirection.Ascending;
                directionPrice = " ASC ";
            }
            DataTable table = this.GetDataPrice(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
            table.DefaultView.Sort = sortExpressionPrice + directionPrice;
            GridView4.DataSource = table;
            GridView4.DataBind();
            //Sorting Issue #1
            ViewState["sortExpressionPrice"] = sortExpressionPrice.ToString();
            ViewState["directionPrice"] = directionPrice.ToString();

            writeToLog.WriteLog("User Sorts on " + sortExpressionPrice + " " + directionPrice, Page.User.Identity.Name);
        }
        public SortDirection sortDirectionPrice
        {
            get
            {
                if (ViewState["SortDirectionPrice"] == null)
                {
                    ViewState["SortDirectionPrice"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirectionPrice"];
            }
            set
            {
                ViewState["SortDirectionPrice"] = value;
            }
        }
        protected void lbInsert_Price(object sender, EventArgs e)
        {
            string query = "sp_insertPriceList: ";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_insertPriceList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = ((TextBox)GridView4.FooterRow.FindControl("txtSubjectInsert")).Text;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ((TextBox)GridView4.FooterRow.FindControl("txtDescriptionInsert")).Text;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = Convert.ToDecimal(((TextBox)GridView4.FooterRow.FindControl("txtPriceInsert")).Text);
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = id;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView4.EditIndex = -1;
                        BindDataPrice(id, "none", " ");
                        conn.Close();
                        writeToLog.WriteLog("Price Row inserted with SP : " + query, Page.User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 15: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        // *** END OF TAB 4 ***
        //******************************************************************************************************************
        //*** START OF TAB 5 ***
        protected void ddlHospitalName_Selected(Object sender, EventArgs e)
        {
            int tx = ((DropDownList)GridView5.FooterRow.FindControl("ddlHospitalName")).SelectedIndex;
            ((DropDownList)GridView5.FooterRow.FindControl("ddlAccountNumber")).SelectedIndex = Convert.ToInt32(tx);
        }
        protected void ddlAccountNumber_Selected(Object sender, EventArgs e)
        {
            int tx = ((DropDownList)GridView5.FooterRow.FindControl("ddlAccountNumber")).SelectedIndex;
            ((DropDownList)GridView5.FooterRow.FindControl("ddlHospitalName")).SelectedIndex = Convert.ToInt32(tx);
        }
        private void BindDataAccountNumber(int contractID, string column, string textSearch)
        {
            //Sorting Issue #1
            if (ViewState["sortExpressionAccountNumber"] != null)
            {
                DataTable table = this.GetDataAccountNumber(contractID, column, textSearch);
                table.DefaultView.Sort = ViewState["sortExpressionAccountNumber"].ToString() + ViewState["directionAccountNumber"].ToString();
                GridView5.DataSource = table;
                GridView5.DataBind();
            }
            else
            {
                GridView5.DataSource = this.GetDataAccountNumber(contractID, column, textSearch);
                GridView5.DataBind();
            }
        }
        private DataTable GetDataAccountNumber(int contractID, string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getContractHospitalList";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Column", column));
                        cmd.Parameters.Add(new SqlParameter("@TextSearch", textSearch));
                        cmd.Parameters.Add(new SqlParameter("@ContractID", contractID));
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(table);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 16: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
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
        protected void SortRecordsAccountNumber(object sender, GridViewSortEventArgs e)
        {
            string sortExpressionAccountNumber = e.SortExpression;
            string directionAccountNumber = string.Empty;
            if (sortDirectionAccountNumber == SortDirection.Ascending)
            {
                sortDirectionAccountNumber = SortDirection.Descending;
                directionAccountNumber = " DESC ";
            }
            else
            {
                sortDirectionAccountNumber = SortDirection.Ascending;
                directionAccountNumber = " ASC ";
            }
            DataTable table = this.GetDataAccountNumber(id, DropDownListAccountNumber.SelectedValue, txtAccountNumberSearch.Text);
            table.DefaultView.Sort = sortExpressionAccountNumber + directionAccountNumber;

            GridView5.DataSource = table;
            GridView5.DataBind();

            //Sorting Issue #1
            ViewState["sortExpressionAccountNumber"] = sortExpressionAccountNumber.ToString();
            ViewState["directionAccountNumber"] = directionAccountNumber.ToString();


            writeToLog.WriteLog("User Sorts on " + sortExpressionAccountNumber + " " + directionAccountNumber, Page.User.Identity.Name);
        }

        public SortDirection sortDirectionAccountNumber
        {
            get
            {
                if (ViewState["SortDirectionAccountNumber"] == null)
                {
                    ViewState["SortDirectionAccountNumber"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirectionAccountNumber"];
            }
            set
            {
                ViewState["SortDirectionAccountNumber"] = value;
            }
        }
        protected void lbInsert_AccountNumber(object sender, EventArgs e)
        {
            string query = "sp_insertContractHospitalList: ";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                try
                {
                    string sqlSP = "sp_insertContractHospitalList";

                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = Convert.ToInt32(((DropDownList)GridView5.FooterRow.FindControl("ddlHospitalName")).SelectedItem.Value);
                        cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Page.User.Identity.Name;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView5.EditIndex = -1;
                        BindDataAccountNumber(id, "none", " ");
                        conn.Close();
                        writeToLog.WriteLog("Hospital / AccountNumber Row inserted with SP : " + query, Page.User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("ERROR 17: " + ex.Message + " : Please email this error to cathal@techsupportit.co.uk");
                }
                finally
                {
                    // Close data reader object and database connection
                    if (conn != null)
                        conn.Close();
                }
            }
        }

        protected void Button_AccountNumber(object sender, EventArgs e)
        {
            BindDataAccountNumber(id, DropDownListAccountNumber.SelectedValue, txtAccountNumberSearch.Text);
            writeToLog.WriteLog("Hospital Row returned results for dropdwn: " + DropDownListAccountNumber.SelectedValue + " and for TextSearch: " + txtAccountNumberSearch.Text, Page.User.Identity.Name);
        }
        //*** END OF TAB 5 ***

        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
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