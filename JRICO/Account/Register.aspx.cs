using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using JRICO.CodeArea;

namespace JRICO.Account
{
    public partial class Register : System.Web.UI.Page
    {
        WriteToLog writeToLog = new WriteToLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            if ((!(Roles.IsUserInRole(Page.User.Identity.Name, "SuperUser"))) && (!(Roles.IsUserInRole(Page.User.Identity.Name, "Admin"))))
            {
                writeToLog.WriteLog("Redirected to login from register page: No authentication ", Page.User.Identity.Name);
                Response.Redirect("Login.aspx");
            }
            writeToLog.WriteLog("Accessed the Register Page ", Page.User.Identity.Name);                   
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

    }
}
