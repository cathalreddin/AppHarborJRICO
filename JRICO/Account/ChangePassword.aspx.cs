using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JRICO.CodeArea;

namespace JRICO.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                writeToLog.WriteLog("Redirected to login from change password page: No authentication ", Page.User.Identity.Name, 0);
                Response.Redirect("../Account/Login.aspx");
            }
            writeToLog.WriteLog("Accessed the Change Password Page ", Page.User.Identity.Name, 0);                
        }
    }
}
