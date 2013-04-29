﻿using System;
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
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    hlMessage.Visible = false;
                }
                else
                {
                    hlMessage.Text = "OOOOPS... NO DATA SELECTED PLEASE CLICK HERE AND SELECT DATA AGAIN";
                    hlMessage.Visible = true;
                    hlMessage.Font.Italic = true;
                    hlMessage.Font.Bold = true;
                    hlMessage.ForeColor = System.Drawing.Color.Red;
                    hlMessage.NavigateUrl = "contractList1.aspx";
                    lblContractReferenceHeading.Visible = false;
                    lblContractTitleHeading.Visible = false;
                    lblContractReferenceLabel.Visible = false;
                    lblContractTitleLabel.Visible = false;
                }
            }
        }
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
            GridView5.DataSource = this.GetDataAccountNumber(contractID ,column, textSearch);
            GridView5.DataBind();
        }
        private DataTable GetDataAccountNumber(int contractID, string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getContractHospitalList";

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

            writeToLog.WriteLog("User Sorts on " + sortExpressionAccountNumber + " " + directionAccountNumber, "cathal");
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
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = "cathal insert CH";
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView5.EditIndex = -1;
                        BindDataAccountNumber(id, "none", " ");
                        conn.Close();
                        writeToLog.WriteLog("Hospital / AccountNumber Row inserted with SP : " + query, "cathal");
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

        protected void Button_AccountNumber(object sender, EventArgs e)
        {
            BindDataAccountNumber(id, DropDownListAccountNumber.SelectedValue, txtAccountNumberSearch.Text);
            //writeToLog.WriteLog("Hospital Row returned results for dropdwn: " + DropDownList1.SelectedValue + " and for TextSearch: " + TextSearch.Text, "cathal");
        }
        //*** END OF TAB 5 ***

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
                        lblStartDate.Text = Convert.ToDateTime(dr["Start Date"].ToString()).ToShortDateString();
                        lblEndDate.Text = Convert.ToDateTime(dr["End Date"].ToString()).ToShortDateString();
                        lblUploadedBy.Text = dr["Uploaded By"].ToString();
                        lblDateUploaded.Text = Convert.ToDateTime(dr["Date Uploaded"].ToString()).ToShortDateString();
                        lblContractReferenceHeading.Text = dr["Contract Reference"].ToString();
                        lblContractTitleHeading.Text = dr["Contract Title"].ToString();
                        lblContractReferenceHeading.Font.Bold = true;
                        lblContractTitleHeading.Font.Bold = true;
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
        // *** END OF TAB 1 ***
        //******************************************************************************************************************
        // *** START OF TAB 4 ***
        private void BindDataPrice(int contractID, string column, string textSearch)
        {
            GridView4.DataSource = this.GetData(contractID, column, textSearch);
            GridView4.DataBind();
        }

        private DataTable GetData(int contractID ,string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getPriceList";

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

            return table;
        }
        protected void RowEditPrice(object sender, GridViewEditEventArgs e)
        {
            GridView4.EditIndex = e.NewEditIndex;
            BindDataPrice(id,"none", " ");
            //writeToLog.WriteLog("Hospital Row with Index:" + e.NewEditIndex.ToString() + " edit link clicked", "cathal");
        }
        protected void RowEditCancelPrice(object sender, GridViewCancelEditEventArgs e)
        {
            GridView4.EditIndex = -1; // reseting grid view
            BindDataPrice(id,"none", " ");
            //writeToLog.WriteLog("Hospital Row cancelled for edit", "cathal");
        }

        protected void RowUpdatePrice(object sender, GridViewUpdateEventArgs e)
        {

            try
            { // Retrieve the row being edited. 
                int index = GridView4.EditIndex;
                GridViewRow row = GridView4.Rows[index];
                TextBox Subject = row.FindControl("txtSubject") as TextBox;
                TextBox Description = row.FindControl("txtDescription") as TextBox;
                TextBox Price = row.FindControl("txtPrice") as TextBox;
                string PriceID = GridView4.DataKeys[e.RowIndex].Value.ToString();

                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string sqlSP = "sp_updatePriceList";

                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = Subject.Text;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description.Text;
                        cmd.Parameters.Add("@Price", SqlDbType.NVarChar).Value = Price.Text;
                        cmd.Parameters.Add("@PriceID", SqlDbType.Decimal).Value = Convert.ToDecimal(PriceID);
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = "cathalPriceUpdate";
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string query = "sp_updatePriceList:";
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView4.EditIndex = -1;
                        BindDataPrice(id, "none", " ");
                        conn.Close();
                        //writeToLog.WriteLog("Hospital Row updated with SP : " + query, "cathal");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Button_Price(object sender, EventArgs e)
        {
            BindDataPrice(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
            //writeToLog.WriteLog("Hospital Row returned results for dropdwn: " + DropDownList1.SelectedValue + " and for TextSearch: " + TextSearch.Text, "cathal");
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
            DataTable table = this.GetData(id, DropDownListPrice.SelectedValue, txtPriceSearch.Text);
            table.DefaultView.Sort = sortExpressionPrice + directionPrice;

            GridView4.DataSource = table;
            GridView4.DataBind();

            //writeToLog.WriteLog("User Sorts on " + sortExpression + " " + direction, "cathal");
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

                using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = ((TextBox)GridView4.FooterRow.FindControl("txtSubjectInsert")).Text;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ((TextBox)GridView4.FooterRow.FindControl("txtDescriptionInsert")).Text;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar).Value = ((TextBox)GridView4.FooterRow.FindControl("txtPriceInsert")).Text;
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = "cathalPriceinsert";
                    cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = id;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                    }
                    GridView4.EditIndex = -1;
                    BindDataPrice(1,"none", " ");
                    conn.Close();
                    //writeToLog.WriteLog("Hospital Row inserted with SP : " + query, "cathal");
                }
            }
            //writeToLog.WriteLog("Hospital Row Inserted : " + query, "cathal insert");
        }
        // *** END OF TAB 4 ***
       

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