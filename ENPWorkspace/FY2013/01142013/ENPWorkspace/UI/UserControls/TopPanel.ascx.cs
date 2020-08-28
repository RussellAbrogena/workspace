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

using System.Text;

using Emerson.WF.K2DataAccess;
using Emerson.WF.ADUtilities;

public partial class UserControls_TopPanel : System.Web.UI.UserControl
{
  private string _User;
  public string User
  {
    get { return _User; }
    set { _User = value; }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
      ((UserPassport)Session["UserPassport"]).GetUserInfo();

    //hlSupport.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["SupportLinkURL"];
    //hlSupport.NavigateUrl = "#";
    hlSupport.Attributes.Add("onclick", "window.open('" + System.Configuration.ConfigurationManager.AppSettings["SupportLinkURL"] + "', '', 'top=0, left=0,height=600, width=800, toolbar=yes, scrollbars=yes, resizable=yes'); return false;");
    hlFAQ.NavigateUrl = "#";
    //hlFAQ.Attributes.Add("onclick", "window.showModalDialog('../FAQ/FAQ.htm', 'ENP WorkFlow FAQ', 'dialogHeight: 600px; dialogWidth: 800px; center:yes; resizable:yes;'); return false;");
    hlFAQ.Attributes.Add("onclick", "window.open('../FAQ/FAQ.htm', '', 'top=0, left=0,height=600, width=800, toolbar=no, scrollbars=yes, resizable=yes'); return false;");

    /*
    K2SQLDataAccess k2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
    ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
    ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
    ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
    */

    Page.ClientScript.RegisterStartupScript(this.GetType(), "PageJavaScript", GetPageJavaScript());

    //string DisplayName = k2SQLDA.GetUserDisplayName(_User);

    //if (DisplayName == string.Empty)
    //    lblUser.Text = "  " + Emerson.WF.ADUtilities.ADConnector.GetUserDisplayName(_User.Substring(_User.LastIndexOf(@"\") + 1)) + "  ";

    //else
    //    lblUser.Text = "  " + DisplayName + "  ";
    lblUser.Text = ((UserPassport)Session["UserPassport"]).DisplayName;

    Page.ClientScript.RegisterStartupScript(this.GetType(), "TriggerRefresh", "<script language='javascript' type='text/javascript'>window.setTimeout('refreshTime()', 0);</script>");
  }

  private string GetPageJavaScript()
  {
    StringBuilder strJS = new StringBuilder();

    strJS.Append("<script language='javascript' type='text/javascript'> \n");
    strJS.Append("<!-- \n\n");

    strJS.Append("serverDate = new Date('" + DateTime.Now.ToString() + "')\n");

    strJS.Append("function refreshTime()\n");
    strJS.Append("{\n");

    strJS.Append("  systemDate = new Date();\n");

    //strJS.Append("  systemDate.setSeconds(systemDate.getSeconds() + 1);\n");
    strJS.Append("  document.getElementById('" + this.lblSystemTime.ClientID + "').innerHTML = ((systemDate.getHours()<10)?'0':'') + systemDate.getHours() + ':' + ((systemDate.getMinutes()<10)?'0':'') + systemDate.getMinutes() + ':' + ((systemDate.getSeconds()<10)?'0':'') + systemDate.getSeconds();\n");
    strJS.Append("  document.getElementById('" + this.lblSystemDate.ClientID + "').innerHTML = formatDate(systemDate, 'NNN d, y') ;\n");

    strJS.Append("  serverDate.setSeconds(serverDate.getSeconds() + 1);\n");
    strJS.Append("  document.getElementById('" + this.lblServerTime.ClientID + "').innerHTML = ((serverDate.getHours()<10)?'0':'') + serverDate.getHours() + ':' + ((serverDate.getMinutes()<10)?'0':'') + serverDate.getMinutes() + ':' + ((serverDate.getSeconds()<10)?'0':'') + serverDate.getSeconds();\n");
    strJS.Append("  document.getElementById('" + this.lblServerDate.ClientID + "').innerHTML = formatDate(serverDate, 'NNN d, y') ;\n");

    strJS.Append("  window.setTimeout('refreshTime()', 1000);\n");
    strJS.Append("}\n");

    strJS.Append("\n//--> \n");
    strJS.Append("</script>\n\n");

    return strJS.ToString();
  }
}
