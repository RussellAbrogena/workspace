using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using Emerson.WF.K2DataAccess;

public partial class UI_Menu_MenuItems : System.Web.UI.Page
{
    public string WorklistCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        //K2ROMDataAccess k2ROMDA = new K2ROMDataAccess(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"]);

        //int intWorklistCount = k2ROMDA.GetWorklistCount();

        string dbConn = ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"].ToString();
        SqlConnection con = new SqlConnection(dbConn);
        SqlCommand cmd = new SqlCommand("sp_GetUserWorklist", con);
        //SqlCommand cmd = new SqlCommand("sp_GetUserWorklistSummary", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@sUserFQN", SqlDbType.NVarChar, 128).Value = this.User.Identity.Name;
        //cmd.Parameters.Add("@UserFQN", SqlDbType.VarChar, 255).Value = this.User.Identity.Name;
        DataTable dt = new DataTable();
        con.Open();
        SqlDataAdapter adptr = new SqlDataAdapter(cmd);
        adptr.Fill(dt);
        con.Close();

        int intWorklistCount = dt.Rows.Count;

        if (intWorklistCount > 0)
            WorklistCount = " (" + intWorklistCount + ")";
        else
            WorklistCount = "";
    }
}
