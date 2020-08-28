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

using Emerson.WF.K2DataAccess;

public partial class Worklist : System.Web.UI.Page
{
  private string Username;
  public string ApprovalURL;

  protected void Page_Load(object sender, EventArgs e)
  {
    #region Detect JavaScript
    if (Session["JSChecked"] == null)
    {
      Session["JSChecked"] = "Checked";

      if (Request.Url.Query.Trim() == string.Empty)
        Response.Write(@"<script language='javascript' type='text/jscript'>   window.location  = '" + Request.Url + "?js=1'; </script>");
      else
        Response.Write(@"<script language='javascript' type='text/jscript'>   window.location  = '" + Request.Url + "&js=1'; </script>");

    }
    //If the javascript Above executed JScript will have a value 
    if (Request.QueryString["js"] != null)
    {
      Session["JsWorks"] = "True";
    }

    if (Session["JsWorks"] == null) // Work under initial assumption that Javascript Doesnt Work // On first load
    {
      Session["JsWorks"] = "False"; //Does Javascript Work
    }

    if (Session["JsWorks"].ToString().Equals("False")) // Work if JS Is Set to False
    {
      Response.Write("Browser scripting is disabled.");
      Response.End();
    }
    #endregion

    if (Request.QueryString["sn"] != null) // automatically shows approval page in the main pane
    {
      string strWorklistSN = Request.QueryString["sn"].ToString();

      K2SQLDataAccess k2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);

      try
      {
        DataRow dr = k2SQLDA.GetWorklistItemInfo(strWorklistSN);

        ApprovalURL = dr["ApprovalURL"].ToString();
      }

      catch (Exception ex)
      {
        if (!ex.Message.Contains(Emerson.WF.Konstants.ERR.WF_002))
          throw ex;
      }
    }
    else
    {
      ApprovalURL = string.Empty;
    }

    K2ROMDataAccess k2ROMDA = new K2ROMDataAccess(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"]);

    Username = User.Identity.Name;
    ucTopPanel.User = Username;
  }
}
