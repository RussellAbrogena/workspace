using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Emerson.WF.K2DataAccess;
using Emerson.WF.ADUtilities;
using Microsoft.ApplicationBlocks.Data;

public partial class UI_Worklist_WorklistItemsExt : System.Web.UI.Page
{

    private K2SQLDataAccess k2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
    private K2ROMDataAccess k2ROMDA = new K2ROMDataAccess(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"]);
    
    public string ProcessFilter
    {
        get { return ViewState["ProcessFilter"] as string ?? null; }
        set { ViewState["ProcessFilter"] = value; }
    }

    public DataTable ItemsDataSource
    {
        get { return ViewState["ProcInst"] as DataTable ?? null; }
        set { ViewState["ProcInst"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PopulateResult();        
        ibtnRefresh.Attributes.Add("onclick", "positionLoadingProgress();");
        lblVersion.Text = Config.ApplicationVersion.CurrentVersion;
    }
    
    private void PopulateResult()
    {
        SqlDataReader WListReader = GetWorkListSummary(this.User.Identity.Name);
        gvWorklistItemsExt.DataSource = WListReader;
        gvWorklistItemsExt.DataBind();

        if (gvWorklistItemsExt.Rows.Count > 0)
        {
            lblSearchMatchNo.Text = gvWorklistItemsExt.Rows.Count.ToString() + " record(s) found.";
            lblPendingItems.Text = "Pending Items";
        }
        else
        {
            lblPendingItems.Text = "No pending items.";
            lblSearchMatchNo.Text = "No records found.";
        }
    }

    private SqlDataReader GetWorkListSummary(string UserFQN)
    {
        string SQLConnString = ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"].ToString();
        SqlDataReader WrkLstReader = null;

        try
        {
            WrkLstReader = SqlHelper.ExecuteReader(SQLConnString, "sp_GetUserWorklistSummary", UserFQN);
        }
        catch (Exception ex)
        {
            if (WrkLstReader != null) WrkLstReader.Close();
            throw new Exception("PopulateResult: " + ex.Message);
        }        
        return WrkLstReader;
    }

    protected void gvWorklistItemsExt_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["WorklistProcessFilter"] = gvWorklistItemsExt.DataKeys[gvWorklistItemsExt.SelectedIndex].Value.ToString();
        Response.Redirect("WorklistItems.aspx");
    }
}
