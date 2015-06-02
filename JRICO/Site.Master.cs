using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace JRICO
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "/Account/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (Roles.IsUserInRole(Page.User.Identity.Name, "SuperUser"))
            {
                hlReports.Visible = true;
                RegisterHyperLink.Visible = true;
                hlLogs.Visible = true;
                hlEmailOverview.Visible = true;
            }
            else if (Roles.IsUserInRole(Page.User.Identity.Name, "Admin"))
            {
                hlReports.Visible = true; 
                RegisterHyperLink.Visible = true;
                hlLogs.Visible = true;
                hlEmailOverview.Visible = false;
            }
            else
            {
                hlReports.Visible = false;
                RegisterHyperLink.Visible = false;
                hlLogs.Visible = false;
                hlEmailOverview.Visible = false;
            }
        }
    }
}
