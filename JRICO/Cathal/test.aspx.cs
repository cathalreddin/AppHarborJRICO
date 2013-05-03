using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using JRICO.CodeArea;

namespace JRICO.Cathal
{
    public partial class test : System.Web.UI.Page
    {
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Roles.IsUserInRole(Page.User.Identity.Name, "SuperUser")))
            {
                Response.Redirect("../Account/Login.aspx");
                writeToLog.WriteLog("Redirected to login from SuperUser page: No authentication ", Page.User.Identity.Name);
            }
            writeToLog.WriteLog("Accessed the SuperUser Page ", Page.User.Identity.Name);            
        }
    }
}