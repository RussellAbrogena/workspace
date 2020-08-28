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

public partial class UI_Menu_MenuItems : System.Web.UI.Page
{
    public string WorklistCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        K2ROMDataAccess k2ROMDA = new K2ROMDataAccess(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"]);

        int intWorklistCount = k2ROMDA.GetWorklistCount();

        if (intWorklistCount > 0)
            WorklistCount = " (" + intWorklistCount + ")";
        else
            WorklistCount = "";

    }
}
