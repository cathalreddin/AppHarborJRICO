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
    public partial class hospitalList : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData("none", " ");
                writeToLog.WriteLog("Hospital List populated on first page", "cathal");
            }
        }
        private void BindData(string column, string textSearch)
        {
            GridView1.DataSource = this.GetData(column, textSearch);
            GridView1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            writeToLog.WriteLog("Hospital Row returned results for dropdwn: " + DropDownList1.SelectedValue + " and for TextSearch: " + TextSearch.Text, "cathal");
        }

        private DataTable GetData(string column, string textSearch)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string sqlSP = "sp_getHospitalList";

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
            writeToLog.WriteLog("Hospital Row with Index:" + e.NewEditIndex.ToString() + " edit link clicked", "cathal");
        }
        protected void RowEditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // reseting grid view
            BindData("none", " ");
            writeToLog.WriteLog("Hospital Row cancelled for edit", "cathal");
        }
        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {

            try
            { // Retrieve the row being edited. 
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

                    using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@HospitalName", SqlDbType.NVarChar).Value = HospitalName.Text;
                        cmd.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = Address1.Text;
                        cmd.Parameters.Add("@Postcode", SqlDbType.NVarChar).Value = Postcode.Text;
                        cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar).Value = AccountNumber.Text;
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = "cathal Rehhupdate";
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = Convert.ToInt32(HospitalID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string query = "sp_updateHospitalList:";
                        foreach (SqlParameter p in cmd.Parameters)
                        {
                            query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                        }
                        GridView1.EditIndex = -1;
                        BindData("none", " ");
                        conn.Close();
                        writeToLog.WriteLog("Hospital Row updated with SP : " + query, "cathal");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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

            writeToLog.WriteLog("User Sorts on " + sortExpression + " " + direction, "cathal");
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

                using (SqlCommand cmd = new SqlCommand(sqlSP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@HospitalName", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtHospitalNameInsert")).Text;
                    cmd.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtAddress1Insert")).Text;
                    cmd.Parameters.Add("@Postcode", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtPostcodeInsert")).Text;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar).Value = ((TextBox)GridView1.FooterRow.FindControl("txtAccountNumberInsert")).Text;
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = "cathal Rehhinsert";
                    conn.Open();
                    cmd.ExecuteNonQuery();                    
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        query = query + p.ParameterName + "=" + p.Value.ToString() + "; ";
                    }
                    GridView1.EditIndex = -1;
                    BindData("none", " ");
                    conn.Close();
                    writeToLog.WriteLog("Hospital Row inserted with SP : " + query, "cathal");
                }
            }
            writeToLog.WriteLog("Hospital Row Inserted : " + query, "cathal insert");
        }
     
    }
}