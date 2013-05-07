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
                RegisterHyperLink.Visible = true;
                hlLogs.Visible = true;
            }
            else if (Roles.IsUserInRole(Page.User.Identity.Name, "Admin"))
            {
                RegisterHyperLink.Visible = true;
                hlLogs.Visible = true;
            }
            else
            {
                RegisterHyperLink.Visible = false;
                hlLogs.Visible = false;
            }
        }
    }
}
