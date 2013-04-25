using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JRICO.CodeArea;

namespace JRICO.Content
{
    public partial class hospitalList1 : System.Web.UI.Page
    {
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbInsert_Click(object sender, EventArgs e)
        {
            SqlDataSource1.InsertParameters["HospitalName"].DefaultValue =
                ((TextBox)GridView1.FooterRow.FindControl("txtHospitalName")).Text;

            SqlDataSource1.InsertParameters["Address1"].DefaultValue =
               ((TextBox)GridView1.FooterRow.FindControl("txtAddress1")).Text;

            SqlDataSource1.InsertParameters["Postcode"].DefaultValue =
               ((TextBox)GridView1.FooterRow.FindControl("txtPostcode")).Text;

            SqlDataSource1.InsertParameters["AccountNumber"].DefaultValue =
               ((TextBox)GridView1.FooterRow.FindControl("txtAccountNumber")).Text;


            SqlDataSource1.InsertParameters["DateUploaded"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource1.InsertParameters["User"].DefaultValue = "cathal reddin";

            SqlDataSource1.Insert();

            string query = "";
            query = query + "HospitalName =" + SqlDataSource1.InsertParameters["HospitalName"].DefaultValue.ToString() + "; ";
            query = query + "Address1 =" + SqlDataSource1.InsertParameters["Address1"].DefaultValue.ToString() + "; ";
            query = query + "Postcode =" + SqlDataSource1.InsertParameters["Postcode"].DefaultValue.ToString() + "; ";
            query = query + "AccountNumber =" + SqlDataSource1.InsertParameters["AccountNumber"].DefaultValue.ToString() + "; ";
            query = query + "DateUploaded =" + SqlDataSource1.InsertParameters["DateUploaded"].DefaultValue.ToString() + "; ";

            writeToLog.WriteLog("Hospital Row Inserted : " + query, SqlDataSource1.InsertParameters["User"].DefaultValue.ToString());
        }
        protected void RowUpdate(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string HospitalID = GridView1.DataKeys[e.RowIndex].Value.ToString();
                string HospitalName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox1")).Text;
                string Address1 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox2")).Text;
                string Postcode = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox3")).Text;
                string AccountNumber = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox4")).Text;


                SqlDataSource1.UpdateParameters["HospitalName"].DefaultValue = HospitalName;

                SqlDataSource1.UpdateParameters["Address1"].DefaultValue = Address1;

                SqlDataSource1.UpdateParameters["Postcode"].DefaultValue = Postcode;

                SqlDataSource1.UpdateParameters["AccountNumber"].DefaultValue = AccountNumber;

                SqlDataSource1.UpdateParameters["HospitalID"].DefaultValue = HospitalID;

                SqlDataSource1.UpdateParameters["DateUploaded"].DefaultValue = DateTime.Now.ToString();
                SqlDataSource1.UpdateParameters["User"].DefaultValue = "cathal reddin";

                SqlDataSource1.Update();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            //string query = "";
            //int index = GridView1.EditIndex;
            //GridViewRow row = GridView1.Rows[index];
            //TextBox HospitalName = row.FindControl("TextBox1") as TextBox;
            //TextBox Address1 = row.FindControl("TextBox2") as TextBox;
            //TextBox Postcode = row.FindControl("TextBox3") as TextBox;
            //TextBox AccountNumber = row.FindControl("TextBox4") as TextBox;

            //query = query + "HospitalName = " + HospitalName.Text;
            //query = query + "; Address1 = " + Address1.Text;
            //query = query + "; Postcode = " + Postcode.Text;
            //query = query + "; AccountNumber = " + AccountNumber.Text + "; DateUploaded=" + DateTime.Now.ToString();
            //writeToLog.WriteLog("Hospital Row Updated: " + query, "cathal");
        }

    }
}