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

using Emerson.WF.ADUtilities;

public partial class UI_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
        ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
        ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        string[] ADGroups = ADConnector.GetGroupsOfUser("rommel.lapade");
        //Trace.Write(
        foreach (string strGroup in ADGroups)
        {
            sb.Append(String.Format(@",{0}\{1}",  "ENP-AP", strGroup.Trim()));
        }

        Label1.Text = sb.ToString().Substring(1);
    }
}
