﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace JRICO.Content
{
    public partial class contractList1 : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData("none", " ");
                //Writelog("User_accessed_List_Page", "cathal");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            //Writelog("User_Searched_on_dropdown_" + DropDownList1.SelectedValue + "_for_Text_" + TextSearch.Text, "cathal");
        }
        private void BindData(string column, string textSearch)
        {
            GridView1.DataSource = this.GetData(column, textSearch);
            GridView1.DataBind();
        }


        private DataTable GetData(string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getContractList";

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

            return table;
        }
        protected void RowEdit(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData("none", " ");
        }
        protected void RowEditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // reseting grid view
            BindData("none", " ");
        }
        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {

            try
            { // Retrieve the row being edited. 
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
                    string sqlSP = "sp_updateContractList";

                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ContractReference", SqlDbType.NVarChar).Value = ContractReference.Text;
                        cmd.Parameters.Add("@AssociatedRef", SqlDbType.NVarChar).Value = AssociatedRef.Text;
                        cmd.Parameters.Add("@RecordType", SqlDbType.Int).Value = Convert.ToInt32(RecordType.Text);
                        cmd.Parameters.Add("@SystemPriceList", SqlDbType.NVarChar).Value = SystemPriceList.Text;
                        cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar).Value = ContractTitle.Text;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name.Text;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email.Text;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone.Text;
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(StartDate.Text);
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(EndDate.Text);
                        cmd.Parameters.Add("@ContractStatus", SqlDbType.Int).Value = Convert.ToInt32(ContractStatus.Text);
                        cmd.Parameters.Add("@ContractID", SqlDbType.Int).Value = Convert.ToInt32(ContractID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        GridView1.EditIndex = -1;
                        BindData("none", " ");
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }



        //protected void RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    GridView1.EditIndex = -1;
        //    BindData("none", " "); 
        //} 

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

            //Writelog("User_Sorts_on_" + sortExpression + "_" + direction, "cathal");
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