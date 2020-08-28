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

using Emerson.WF.MasterConfig;
using Emerson.WF;

public partial class UI_Worklist_EmailNotificationSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnClose.Attributes.Add("onclick", "window.close(); return false;");
        rbtnOff.Attributes.Add("onclick", "document.getElementById('" + ddlDays.ClientID + "').disabled = this.checked; ValidatorEnable(document.getElementById('" + rfvDay.ClientID + "'), !this.checked);");
        rbtnOn.Attributes.Add("onclick", "document.getElementById('" + ddlDays.ClientID + "').disabled = !this.checked; ValidatorEnable(document.getElementById('" + rfvDay.ClientID + "'), this.checked); document.getElementById('" + ddlDays.ClientID + "').options[7].selected = true;");

        if (!Page.IsPostBack)
        {
            LoadSettings();
        }
        else
        {
            
        }

        if (rbtnOff.Checked)
        {
            ddlDays.Enabled = false;
            rfvDay.Enabled = false;
        }

        if (rbtnOn.Checked)
        {
            ddlDays.Enabled = true;
            rfvDay.Enabled = true;
        }
    }

    private void LoadSettings()
    {
        string username = Page.User.Identity.Name;

        ENPUser eu = new ENPUser(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
        eu.Get(username, ENPUser.GetBy.LoginName);

        rbtnOff.Checked = (eu.EmailFlag == "0");
        rbtnOn.Checked = (eu.EmailFlag != "0");

        if (rbtnOff.Checked)
        {
            ddlDays.Enabled = false;    
        }

        if (rbtnOn.Checked)
        {
            ddlDays.Enabled = true;    
            ddlDays.SelectedIndex = Convert.ToInt16(eu.EmailFlag);
        }

        XMLDataAccess.MasterDataLocation = ConfigurationManager.AppSettings["XMLMasterDataLocation"].ToString();
        DataTable dtMessages = XMLDataAccess.GetMasterData(XMLDataAccess.XMLData.Messages);

        lblDesc1.Text = dtMessages.Select("Code = 'TurnOffWSS'")[0][1].ToString();
        lblDesc2.Text = dtMessages.Select("Code = 'TurnOnWSS'")[0][1].ToString();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string username = Page.User.Identity.Name;
        
        ENPUser eu = new ENPUser(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
        eu.Get(username, ENPUser.GetBy.LoginName);
        eu.EmailFlag = ddlDays.SelectedIndex.ToString();
        eu.Update();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Konstants.Message.SaveSuccessful + "');</script>");
    }
}
