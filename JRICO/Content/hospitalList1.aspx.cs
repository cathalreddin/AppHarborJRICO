using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRICO.Content
{
    public partial class hospitalList1 : System.Web.UI.Page
    {
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
        }

    }
}