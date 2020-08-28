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

public partial class UI_Worklist_WorklistOptions : System.Web.UI.Page
{
    private K2SQLDataAccess K2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        hlOutOffice.Attributes.Add("onclick", "return OutOfOffice();");
        hlEmailSettings.Attributes.Add("onclick", "return EmailSettings();");

        if (!Page.IsPostBack)
        {
            Session["WorklistProcessFilter"] = "ALL"; // set to all processes by default
            PopulateProcessFilter();
        }
        
    }

    private void PopulateProcessFilter()
    {
        string strADGroups = string.Empty;

        GetADGroups(ref strADGroups);

        // 6/8/07 added abrs BEGIN -- to address the issue with 2 process versions deployed simultaneously

        //chklProcess.DataSource = K2SQLDA.GetProcSetWithPermission(User.Identity.Name, ref strADGroups);

        DataTable pf = new DataTable();
        pf.Columns.Add("ProcSetName");
        pf.Columns.Add("ProcSetID");


        string oUsername = "EMRSN\\dbonoan";
        int oCounter = 0;
        //foreach (DataRow dr1 in K2SQLDA.GetProcSetWithPermission(User.Identity.Name, ref strADGroups).Rows)

        foreach (DataRow dr1 in K2SQLDA.GetProcSetWithPermission(oUsername, ref strADGroups).Rows)
        {
            bool bExists = false;
            oCounter = oCounter + 1;

            foreach (DataRow dr2 in pf.Rows)
            {
                if (dr2["ProcSetName"].ToString() == dr1["ProcSetName"].ToString())
                {
                    dr2["ProcSetID"] += "," + dr1["ProcSetID"].ToString();
                    bExists = true;
                }
                else
                {
                    string oName = dr2["ProcSetName"].ToString();
                    string oName2 = dr1["ProcSetName"].ToString();
                }
            }

            if (!bExists)
            {
                DataRow newdr = pf.NewRow();
                newdr["ProcSetID"] = dr1["ProcSetID"];
                newdr["ProcSetName"] = dr1["ProcSetName"];
                pf.Rows.Add(newdr);
            }

        }

        int xCounter = oCounter;
        chklProcess.DataSource = pf;
        // 6/8/07 added abrs BEGIN

        chklProcess.DataTextField = "ProcSetName";
        chklProcess.DataValueField = "ProcSetID";
        chklProcess.DataBind();

        // select all by default	
        //foreach (ListItem li in chklProcess.Items)
        //    li.Selected = true;

        SaveADGroups(strADGroups);
    }

    private void GetADGroups(ref string ADGroups)
    {
        //if (Request.Cookies[User.Identity.Name + "ADGroups"] != null)
        //{
        //    ADGroups = Request.Cookies[User.Identity.Name + "ADGroups"].Value.ToString();
        //}

        string oUsername = "EMRSN\\dbonoan";

        if (Request.Cookies[oUsername + "ADGroups"] != null)
        {
            ADGroups = Request.Cookies[oUsername + "ADGroups"].Value.ToString();
        }
        else  // if cookie is not found, set up ADConnection config settings
        {
            //ActiveDirectory AD = new ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString(), ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString());
            
            /*
            ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
            ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
            ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
             * */
            /*
            StringBuilder sb = new StringBuilder();
            string Username = Page.User.Identity.Name.ToString();

            foreach (string strGroup in ((UserPassport)Session["UserPassport"]).ADGroups)
            {
                sb.Append(String.Format(@",{0}{1}", Username.Substring(0, Username.IndexOf(@"\") + 1).Trim(), strGroup.Trim()));

                ADGroups = sb.ToString().Substring(1);
            }
              */
            ADGroups = ((UserPassport)Session["UserPassport"]).ADGroupsCommaDel;
        }
    }

    private void SaveADGroups(string ADGroups)
    {
        // 4/11 saving and retrieval from cookie removed 
        //if (Request.Cookies[User.Identity.Name + "ADGroups"] == null)
        //{
        //    Response.Cookies[User.Identity.Name + "ADGroups"].Value = ADGroups;
        //    Response.Cookies[User.Identity.Name + "ADGroups"].Expires = DateTime.Today.AddDays(1);
        //}
    }

    private string GetProcessFilter()
    {
        string strProcess = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklProcess.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strProcess += ((strProcess != string.Empty) ? "," : "") + String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString());

        }

        if (bolAllSelected)
            strProcess = "ALL";

        return strProcess;
    }

    protected void btnViewItems_Click(object sender, EventArgs e)
    {
        hdnIsPostBack.Value = "true";
        hdnProcessFilter.Value = GetProcessFilter();

        Session["WorklistProcessFilter"] = hdnProcessFilter.Value;
    }
}
