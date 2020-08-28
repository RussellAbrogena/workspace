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

public partial class UI_ADGroups : System.Web.UI.Page
{
    private DateTime Start;
    private DateTime End;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        Start = DateTime.Now;

        ActiveDirectory AD = new ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString());


        

        Label1.Text = "GROUPS";

        foreach (string group in AD.GetGroupsOfUser(txtUser.Text))
            Label1.Text = Label1.Text + "---" + group;

        Label1.Text += " ---- Processing Time :" + DateTime.Now.Subtract(Start);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Start = DateTime.Now;

        ActiveDirectory AD = new ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString());

        Label2.Text = AD.IsUserMemberOf(TextBox1.Text, TextBox2.Text).ToString();

        Label2.Text += " ---- Processing Time :" + DateTime.Now.Subtract(Start);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Start = DateTime.Now;

        ActiveDirectory AD = new ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString());


        Label3.Text = "MANAGERS";

        foreach (string manager in AD.GetManagerListUntilGroupIsReached(TextBox3.Text, TextBox4.Text))
            Label3.Text = Label3.Text + "---" + manager;

        Label3.Text += " ---- Processing Time :" + DateTime.Now.Subtract(Start);

    }
}
