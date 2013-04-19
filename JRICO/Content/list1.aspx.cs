using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JRICO.Content
{
    public partial class list1 : System.Web.UI.Page
    {
        string _connStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData("none", " ");
                Writelog("User_accessed_List_Page", "cathal");
            }
        }

        public void Writelog(string LogData, string LogUser) 
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                String MyString = @"INSERT INTO Log(LogData, LogUser) VALUES('"+LogData+"','"+LogUser+"')";
                //String MyString = "INSERT INTO Log(LogData, LogUser, LogDate) VALUES( " + LogData + " ,1,1/1/1)";
                SqlCommand MyCmd = new SqlCommand(MyString, conn);
                MyCmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            GridViewRow row = e.Row;
            List<TableCell> cells = new List<TableCell>();
            foreach (DataControlField column in GridView1.Columns)
            {
                //Getting first Column of the Gridview
                TableCell cell = row.Cells[0];

                //Remove that cell from the gridview
                row.Cells.Remove(cell);

                //Adding that cell as the last cell in the gridview
                cells.Add(cell);
            }
            // Add cells
            row.Cells.AddRange(cells.ToArray()); 
            
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
                    cmd.Parameters.Add(new SqlParameter("@column", column));
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

            Writelog("User_Sorts_on_" + sortExpression + "_" + direction, "cathal");
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData(DropDownList1.SelectedValue, TextSearch.Text);
            Writelog("User_Searched_on_dropdown_" + DropDownList1.SelectedValue + "_for_Text_" + TextSearch.Text, "cathal");
  
        }


    }
}