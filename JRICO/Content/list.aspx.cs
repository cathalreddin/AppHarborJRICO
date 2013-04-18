using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace JRICO.Content
{
    public partial class list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("SQLConnectionString");
           DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_getContractList");
           GridView1.DataSource = ds.Tables[0];
           GridView1.DataBind();            
        }
    }
}