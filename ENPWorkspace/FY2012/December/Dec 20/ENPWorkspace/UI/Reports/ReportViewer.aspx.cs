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
using System.Collections.Generic;

using Emerson.WF;
using Emerson.WF.ADUtilities;
using Emerson.WF.K2DataAccess;
using Emerson.WF.MasterConfig;

public partial class UI_Reports_ReportViewer : System.Web.UI.Page
{

#region ENPCountryList

    public List<ENPCountry> ENPCountryList
    {
        get
        {
            if (ViewState["ENPCountryList"] == null)
                ViewState["ENPCountryList"] = new ENPCountry().GetList<ENPCountry>(string.Empty, ENPCountry.GetListBy.All, false);

            return (List<ENPCountry>)ViewState["ENPCountryList"];
        }
        set
        {
            ViewState["ENPCountryList"] = value;
        }
    }

    public ListItemCollection CountryListItems
    {
        get
        {
            ListItemCollection _CountryItemColl = new ListItemCollection();

            int index = 0;

            while (index < chklCountry.Items.Count)
            {
                ListItem _CountryItem = new ListItem();

                _CountryItem.Value = chklCountry.Items[index].Value;
                _CountryItem.Text = chklCountry.Items[index].Text;
                _CountryItemColl.Add(_CountryItem);

                _CountryItem = null;
                index++;
            }

            return _CountryItemColl;
        }
        set
        {
            chklCountry.Items.Clear();
            int index = 0;

            while (index < value.Count)
            {
                chklCountry.Items.Add(value[index]);
                index++;
            }

            ENPCountry newEC = new ENPCountry();
            newEC.Find(ENPCountryList, ENPCountry.GetBy.MUCode, chklCountry.SelectedValue);

        }
    }
    public string CountryCode
    {
        get
        {
            return chklCountry.SelectedItem.Value;
        }
        set
        {
            chklCountry.SelectedItem.Value = value;
        }
    }
    public string CountryName
    {
        get
        {
            return chklCountry.SelectedItem.Text;
        }
        set
        {
            chklCountry.SelectedItem.Text = value;
        }
    }

    #endregion   
    
    private K2SQLDataAccess K2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
    private string ReportName = string.Empty;
        
    protected void Page_Load(object sender, EventArgs e)
    {

        if (ADConnector.LDAPDomain == null || ADConnector.LDAPPassword == null || ADConnector.LDAPUsername == null)
        {
            ADConnector.LDAPDomain = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"].ToString();
            ADConnector.LDAPUsername = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"].ToString();
            ADConnector.LDAPPassword = ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"].ToString();
        }

        ReportName = Request.QueryString["rname"];
        Session["ReportViewer.ReportName"] = ReportName;

        switch (ReportName)
        {
            case Constants.Report.Name.InitiatedRequests:
                {
                    lblReportName.Text = Constants.Report.DisplayName.InitiatedRequests;
                    break;
                }
            case Constants.Report.Name.ApprovedDeniedRequests:
                {
                    lblReportName.Text = Constants.Report.DisplayName.ApprovedDeniedRequests;
                    break;
                }
            case Constants.Report.Name.DocSubSummaryRequests:
                {
                    lblReportName.Text = Constants.Report.DisplayName.DocSubSummaryRequests;
                    break;
                }

            case Constants.Report.Name.TravelSummaryRequests:
                {
                    lblReportName.Text = Constants.Report.DisplayName.TravelSummaryRequests;
                    break;
                }

            case Constants.Report.Name.CAPEXSummaryRequests:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CAPEXSummaryRequests;
                    break;
                }

            case Constants.Report.Name.CAPEXRequests:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CAPEXRequests;
                    break;
                }

            case Constants.Report.Name.ReportBuilder:
                {
                    lblReportName.Text = Constants.Report.DisplayName.ReportBuilder;
                    break;
                }
            case Constants.Report.Name.IPRSummaryRequest:
                {
                    lblReportName.Text = Constants.Report.DisplayName.IPRSummaryRequest;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CRPSummaryReport;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportDetailed:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CRPSummaryReportDetailed;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractType:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CRPSummaryReportByContractType;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractTypeWithMU:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CRPSummaryReportByContractTypeWithMU;
                    break;
                }
            case Constants.Report.Name.SCRSummaryReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.SCRSummaryReport;
                    break;
                }

            case Constants.Report.Name.SCRSummaryReportDetailed:
                {
                    lblReportName.Text = Constants.Report.DisplayName.SCRSummaryReportDetailed;
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.ePRMasterTATReport;
                    break;
                }
            case Constants.Report.Name.PRMasterReport2:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PRMasterReport2;
                    break;
                }
            case Constants.Report.Name.PRMasterReport3:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PRMasterReport3;
                    break;
                }
            case Constants.Report.Name.PRMasterReportByMU:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PRMasterReportByMU;
                    break;
                }
            case Constants.Report.Name.PRMasterReportCurrentApprover:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PRMasterReportCurrentApprover;
                    break;
                }
            case Constants.Report.Name.PROverallSummaryReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PROverallSummaryReport;
                    break;
                }
            case Constants.Report.Name.PRDetailReportByMU:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PRDetailReportByMU;
                    break;
                }
            case Constants.Report.Name.PROverallSummaryReportByMU:
                {
                    lblReportName.Text = Constants.Report.DisplayName.PROverallSummaryReportByMU;
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.ePRMasterDetailReport;
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReportByMU:
                {
                    ReportName = Constants.Report.Name.ePRMasterDetailReport;
                    lblReportName.Text = Constants.Report.DisplayName.ePRMasterDetailReportByMU;
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReportByMU:
                {
                    ReportName = Constants.Report.Name.ePRMasterTATReport;
                    lblReportName.Text = Constants.Report.DisplayName.ePRMasterTATReportByMU;
                    break;
                }
            case Constants.Report.Name.ePRSavingsReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.ePRSavingsReport;
                    break;
                }
            case Constants.Report.Name.ePRSavingsReportByMU:
                {
                    ReportName = Constants.Report.Name.ePRSavingsReport;
                    lblReportName.Text = Constants.Report.DisplayName.ePRSavingsReportByMU;
                    break;
                }
            case Constants.Report.Name.CSRSummaryReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CSRSummaryReport;
                    break;
                }
            case Constants.Report.Name.CSRSummaryReportDetailed:
                {
                    lblReportName.Text = Constants.Report.DisplayName.CSRSummaryReportDetailed;
                    break;
                }
            case Constants.Report.Name.SMRSummaryReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.SMRSummaryReport;
                    break;
                }
            case Constants.Report.Name.SMRSummaryReportDetailed:
                {
                    lblReportName.Text = Constants.Report.DisplayName.SMRSummaryReportDetailed;
                    break;
                }
            case Constants.Report.Name.OARSummaryReport:
                {
                    lblReportName.Text = Constants.Report.DisplayName.OARSummaryReport;
                    break;
                }
            case Constants.Report.Name.OARSummaryReportDetailed:
                {
                    lblReportName.Text = Constants.Report.DisplayName.OARSummaryReportDetailed;
                    break;
                }

        }
    
        imgDateFrom.Attributes.Add("onclick", "javascript:popUpCalendar(this, " + txtDateFrom.ClientID + ", 'm/d/yyyy'); return false;");
        txtDateFrom.Attributes.Add("readonly", "true");
        imgDateTo.Attributes.Add("onclick", "javascript:popUpCalendar(this, " + txtDateTo.ClientID + ", 'm/d/yyyy'); return false;");
        txtDateTo.Attributes.Add("readonly", "true");

        imgTRPFlightStartDate.Attributes.Add("onclick", "javascript:popUpCalendar(this, " + txtTRPFlightStartDate.ClientID + ", 'm/d/yyyy'); return false;");
        txtTRPFlightStartDate.Attributes.Add("readonly", "true");
        imgTRPFlightEndDate.Attributes.Add("onclick", "javascript:popUpCalendar(this, " + txtTRPFlightEndDate.ClientID + ", 'm/d/yyyy'); return false;");
        txtTRPFlightEndDate.Attributes.Add("readonly", "true");

        imgApprovedDateFrom.Attributes.Add("onclick", "javascript:popUpCalendar(this, " + txtApprovedDateFrom.ClientID + ", 'm/d/yyyy'); return false;");
        txtApprovedDateFrom.Attributes.Add("readonly", "true");
        imgApprovedDateTo.Attributes.Add("onclick", "javascript:popUpCalendar(this, " + txtApprovedDateTo.ClientID + ", 'm/d/yyyy'); return false;");
        txtApprovedDateTo.Attributes.Add("readonly", "true");

        if (!Page.IsPostBack)
        {
            PopulateProcessFilter();
            PopulateCountryFilter();
            PopulateTraveller();
            SetVisibleParameters();

        }
    }

    private void PopulateProcessFilter()
    {
        string strADGroups = ((UserPassport)Session["UserPassport"]).ADGroupsCommaDel;

        DataTable pf = new DataTable();
        pf.Columns.Add("ProcSetName");
        pf.Columns.Add("ProcSetID");

        foreach (DataRow dr1 in K2SQLDA.GetProcSetWithPermission(User.Identity.Name, ref strADGroups).Rows)
        {
            bool bExists = false;

            foreach (DataRow dr2 in pf.Rows)
            {
                if (dr2["ProcSetName"].ToString() == dr1["ProcSetName"].ToString())
                {
                    dr2["ProcSetID"] += "," + dr1["ProcSetID"].ToString();
                    bExists = true;
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

        chklProcess.DataSource = pf;
        // 6/8/07 added abrs BEGIN

        chklProcess.DataTextField = "ProcSetName";
        chklProcess.DataValueField = "ProcSetID";
        chklProcess.DataBind();

        ddl_ProcessFilter.DataSource = pf;
        ddl_ProcessFilter.DataTextField = "ProcSetName";
        ddl_ProcessFilter.DataValueField = "ProcSetID";
        ddl_ProcessFilter.DataBind();

        ddl_ProcessFilter.Items.Insert(0, new ListItem(" -- Select Process -- ", ""));
        ddl_ProcessFilter.SelectedValue = "Select";


        // select all by default	
        foreach (ListItem li in chklProcess.Items)
        {
            li.Selected = true;
        }
    }
    private void PopulateCountryFilter()
    {
        ENPCountryList = new ENPCountry().GetList<ENPCountry>(string.Empty, ENPCountry.GetListBy.All, false);

        ENPCountry ecExclude = new ENPCountry();

        foreach (string ExcludedMU in ConfigurationManager.AppSettings["ExcludedMUs"].Split(','))
        {
            if (ExcludedMU.Trim() != string.Empty)
                ENPCountryList.Remove(ecExclude.FindObject(ENPCountryList, ENPCountry.GetBy.MUCode, ExcludedMU.Trim()));
        }
   
        chklCountry.DataSource = ENPCountryList;
        chklCountry.DataTextField = ENPCountry.FieldNames.MUName;
        chklCountry.DataValueField = ENPCountry.FieldNames.MUCode;
        chklCountry.DataBind();

        ddlCountry.DataSource = ENPCountryList;
        ddlCountry.DataTextField = ENPCountry.FieldNames.MUName;
        ddlCountry.DataValueField = ENPCountry.FieldNames.MUCode;
        ddlCountry.DataBind();

        ddlCountry.Items.Insert(0, new ListItem(" -- All -- ", ""));
        ddlCountry.SelectedValue = "";

        ReportName = Request.QueryString["rname"];
        Session["ReportViewer.ReportName"] = ReportName;

        switch (ReportName)
        {            
            case Constants.Report.Name.PRMasterReportByMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.PRMasterReportCurrentApprover:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractTypeWithMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.PRDetailReportByMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.PROverallSummaryReportByMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReportByMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReportByMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
            case Constants.Report.Name.ePRSavingsReportByMU:
                {
                    Emerson.WF.ADUtilities.ActiveDirectory AD = new Emerson.WF.ADUtilities.ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"],
                    ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);

                    //hdnCountry.Text = AD.GetCountryNameByLoginId(((UserPassport)Session["UserPassport"]).Username);
                    this.hdnCountry.Text = AD.GetCountryCodeByLoginId(Page.User.Identity.Name.Substring(Page.User.Identity.Name.LastIndexOf(@"\") + 1));
                    ddlCountry.SelectedValue = hdnCountry.Text.ToString();
                    ddlCountry.Enabled = false;
                    break;
                }
        }
    

    }

    private void PopulateDatafields()
    {
        cb_Datafields.Items.Clear();
        cb_Datafields.ClearSelection();

        DataTable df = new DataTable();
        df.Columns.Add("sDataFieldsName");
        df.Columns.Add("sDataFieldsID");

        foreach (DataRow dr1 in K2SQLDA.GetDataFields(this.ddl_ProcessFilter.SelectedValue).Rows)
        {
            bool bExists = false;

            foreach (DataRow dr2 in df.Rows)
            {
                if (dr2["sDataFieldsName"].ToString() == dr1["sDataFieldsName"].ToString())
                {
                    dr2["sDataFieldsID"] += "," + dr1["sDataFieldsID"].ToString();
                    bExists = true;
                }
            }

            if (!bExists)
            {
                DataRow newdr = df.NewRow();
                newdr["sDataFieldsID"] = dr1["sDataFieldsID"];
                newdr["sDataFieldsName"] = dr1["sDataFieldsName"];
                df.Rows.Add(newdr);
            }
        }

        cb_Datafields.DataSource = df;
        cb_Datafields.DataTextField = "sDataFieldsName";
        cb_Datafields.DataValueField = "sDataFieldsID";
        cb_Datafields.DataBind();

    }

    private void PopulateDepartment(string MUCode)
    {
        ddlDepartment.Items.Clear();
        ddlDepartment.ClearSelection();

        ENPDepartment enpDept = new ENPDepartment(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
        ddlDepartment.DataSource = enpDept.GetList<ENPDepartment>(ENPDepartment.GetListBy.MUCode, false, MUCode);
        ddlDepartment.DataTextField = ENPDepartment.FieldNames.DepartmentName;
        ddlDepartment.DataValueField = ENPDepartment.FieldNames.PKey;

        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem(" -- Select All -- ", ""));
       

    }
    private void PopulateTraveller()
    {
        ddlTraveller.Items.Clear();
        ddlTraveller.ClearSelection();

        ENPUser tr = new ENPUser(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
        ddlTraveller.DataSource = tr.GetList<ENPUser>(ENPUser.GetListBy.AllActive);
        ddlTraveller.DataTextField = ENPUser.FieldNames.DisplayName;
        ddlTraveller.DataValueField = ENPUser.FieldNames.LoginName;

        ddlTraveller.DataBind();
        ddlTraveller.Items.Insert(0, new ListItem("< Select All >", ""));
        
    }

    #region Get concatenated parameter values
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
    private string GetProcessSelectedFilter()
    {
        string strProcess = string.Empty;

        strProcess += (this.ddl_ProcessFilter.SelectedValue.ToString());

        return strProcess;
    }
    private string GetCountry()
    {
        string strCountry = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklCountry.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strCountry += ((strCountry != string.Empty) ? "," : "") + String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString());

        }

        if (bolAllSelected)

            strCountry = "ALL";

        return strCountry;
    }
    private string GetCountryTRF()
    {
        string strCountry = string.Empty;
        ddlDepartment.Items.Clear();
        bool bolAllSelected = true;
        int intMUCounter = 0;

        foreach (ListItem li in chklCountry.Items)
        {

            if (!li.Selected)
            {
                bolAllSelected = false;
            }
            else
            {
                strCountry += ((strCountry != string.Empty) ? "," : "") + String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString());
                intMUCounter++;
            }
        }

        if (intMUCounter <= 1)
        {
            ddlDepartment.Enabled = true;
            PopulateDepartment(chklCountry.SelectedValue.ToString());           
        }
        else
        {
            ddlDepartment.Enabled = false;
            ddlDepartment.Items.Clear();
        }



        if (bolAllSelected)
        {
            strCountry = "";
        }

        return strCountry;
    }
    private string GetBestCost()
    {
        string strBestCost = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklBestCost.Items)
        {
            if (!li.Selected)
            {
                bolAllSelected = false;
            }
            else
            {
                strBestCost += ((strBestCost != string.Empty) ? "," : "") + String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString());
            }

            if (bolAllSelected)
            {
                strBestCost = "";
            }

         }

        return strBestCost;
    }

    private string GetEmployee()
    {
        string strEmployee = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chkEmployee.Items)
        {
            if (!li.Selected)
            {
                bolAllSelected = false;
            }
            else
            {
                strEmployee += ((strEmployee != string.Empty) ? "," : "") + String.Format((li.Value.ToString().Length == 1) ? "0{0}" : "{0}", li.Value.ToString());
            }

            if (bolAllSelected)
            {
                strEmployee = "";
            }

        }

        return strEmployee;
    }
    private string GetDepartment()
    {
        string strDepartment = string.Empty;
        strDepartment += (ddlDepartment.SelectedValue.ToString());

        if (ddlDepartment.SelectedValue == string.Empty)
        {
            strDepartment = "";
        }
        
        return strDepartment;
    }
    private string GetTraveller()
    {
        string strTraveller = string.Empty;
        strTraveller += (ddlTraveller.SelectedItem.ToString());

        if (ddlTraveller.SelectedValue == string.Empty)
        {
            strTraveller = "";
        }

        return strTraveller;
    }
    private string GetStatusTRF()
    {
        string strStatus = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklStatus.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strStatus += ((strStatus != string.Empty) ? "," : "") + li.Value.ToString();

        }

        if (bolAllSelected)
            strStatus = "";
        return strStatus;
    
    }
    private string GetStatusFilter()
    {
        string strStatus = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklStatus.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strStatus += ((strStatus != string.Empty) ? "," : "") + li.Value.ToString();

        }

        if (bolAllSelected)
            strStatus = "ALL";

        return strStatus;
    }
    private string GetStatusFilterAD()
    {
        string strStatus = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklStatusAD.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strStatus += ((strStatus != string.Empty) ? "," : "") + li.Value.ToString();

        }

        if (bolAllSelected)
            strStatus = "ALL";

        return strStatus;
    }
    private string GetARTypeFilter()
    {
        string strARType = string.Empty;
        bool bolAllSelected = true;

        foreach (ListItem li in chklARType.Items)
        {

            if (!li.Selected)
                bolAllSelected = false;
            else
                strARType += ((strARType != string.Empty) ? "," : "") + li.Value.ToString();

        }

        if (bolAllSelected)
            strARType = "ALL";

        return strARType;
    }
  
    private string GetCountryFilter()
    {
        string strCountryFilter = string.Empty;
        foreach (FinanceGroup fg in ((UserPassport)Session["UserPassport"]).FinanceGroups)
        {
            strCountryFilter += ((strCountryFilter != string.Empty) ? "," : "") + fg.Country;
        }

        return strCountryFilter;
    }
    private string GetDataFieldsFilter()
    {
        string strDataFieldsFilter = string.Empty;
        
        foreach (ListItem li in cb_Datafields.Items)
        {
            if(li.Selected == true)
            {
                strDataFieldsFilter += ((strDataFieldsFilter != string.Empty) ? "," : "") + li.Value.ToString();
            }

        }

        return strDataFieldsFilter;
    }   
    #endregion

    private void SetVisibleParameters()
    {
        switch (ReportName)
        {
            case Constants.Report.Name.InitiatedRequests:
                {                    
                    trNoteAD.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;                   
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trCountry.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }

            case Constants.Report.Name.ApprovedDeniedRequests:
                {
                    trInitiatedRequests.Visible = false;
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trCountry.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.DocSubSummaryRequests:
                {
                    trNoteAD.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trNoteInitiated.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trARTypeFilter.Visible = false;
                    trProcessFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }

            case Constants.Report.Name.TravelSummaryRequests:
                {
                    trNoteInitiated.Visible = false;
                    trNoteAD.Visible = false;
                    trNoteInitiated.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trARTypeFilter.Visible = false;
                    trProcessFilter.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    break;
                }

            case Constants.Report.Name.CAPEXSummaryRequests:
                {
                    trNoteInitiated.Visible = false;
                    trNoteAD.Visible = false;
                    trNoteInitiated.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trProcessFilter.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }


            case Constants.Report.Name.CAPEXRequests:
                {
                    trInitiatedRequests.Visible = false;                   
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trCountry.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }

            case Constants.Report.Name.ReportBuilder:
                {
                    trInitiatedRequests.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trNoteAD.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trARTypeFilter.Visible = false;
                    trProcessFilter.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.IPRSummaryRequest:
                {
                    trNoteAD.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trCountry.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReport:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;                    
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PRMasterReport2:
                {
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PRMasterReport3:
                {
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PRMasterReportByMU:
                {                  
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PRMasterReportCurrentApprover:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PROverallSummaryReport:
                {
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trPRType.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReport:
                {                    
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportDetailed:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractType:
                {
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractTypeWithMU:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.SCRSummaryReport:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.SCRSummaryReportDetailed:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PRDetailReportByMU:
                {
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.PROverallSummaryReportByMU:
                {
                    trPRNumber.Visible = false;
                    trDateFilter.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReport:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReportByMU:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReportByMU:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.ePRSavingsReport:
                {
                    trPRType.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.ePRSavingsReportByMU:
                {
                    trPRType.Visible = false;
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRNumber.Visible = false;
                    trCountry.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.CSRSummaryReport:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.CSRSummaryReportDetailed:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.SMRSummaryReport:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.SMRSummaryReportDetailed:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.OARSummaryReport:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
            case Constants.Report.Name.OARSummaryReportDetailed:
                {
                    trNoteAD.Visible = false;
                    trProcessFilter.Visible = false;
                    trCountry.Visible = false;
                    trInitiatedRequests.Visible = false;
                    trApprovalDateFilter.Visible = false;
                    trApprovedDeniedRequests.Visible = false;
                    trNoteInitiated.Visible = false;
                    trARTypeFilter.Visible = false;
                    trDepartment.Visible = false;
                    trBestCost.Visible = false;
                    trTraveller.Visible = false;
                    trCostCenter.Visible = false;
                    trAccomodation.Visible = false;
                    trFlight.Visible = false;
                    trProcessFilterDDL.Visible = false;
                    trDataFields.Visible = false;
                    trPRType.Visible = false;
                    trPRNumber.Visible = false;
                    trCountryFilterDDL.Visible = false;
                    trContractType.Visible = false;
                    trTRPEmployee.Visible = false;
                    trTRPFlightDate.Visible = false;
                    break;
                }
        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string SubmittedDateFrom = "1/1/2007";
        string SubmittedDateTo = "12/30/2020";

        string ApprovedDateFrom = "1/1/2007";
        string ApprovedDateTo = "12/30/2020";

        string CostCenter = txtCostCenter.Text.Trim();
        string Accomodation = txtAccomodation.Text.Trim();
        string FlightDetails = txtFlight.Text.Trim();
        string PRNumber = txtPRNumber.Text.Trim();
        string PRType = ddlPRType.SelectedValue.ToString();
        string Country = ddlCountry.SelectedValue.ToString();
        string ContractType = ddlContractType.SelectedValue.ToString();
        
        
        string ParamValues = string.Empty;

        string ReportViewerURL = System.Configuration.ConfigurationManager.AppSettings["ReportViewerURL"].ToString();

        if (txtDateFrom.Text.Trim() != string.Empty && txtDateTo.Text.Trim() != string.Empty)
        {
            SubmittedDateFrom = txtDateFrom.Text.Trim();
            SubmittedDateTo = txtDateTo.Text.Trim();
        }

        if (txtApprovedDateFrom.Text.Trim() != string.Empty && txtApprovedDateTo.Text.Trim() != string.Empty)
        {
            ApprovedDateFrom = txtApprovedDateFrom.Text.Trim();
            ApprovedDateTo = txtApprovedDateTo.Text.Trim();
        }               
       
        switch (ReportName)
        {
            case Constants.Report.Name.InitiatedRequests:
                {                    
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&uid=" + User.Identity.Name.ToString() + "&pf=" + GetProcessFilter() + "&sf=" + GetStatusFilter();
                    break;
                }
            case Constants.Report.Name.ApprovedDeniedRequests:
                {                    
                    ParamValues = @"dtfromAD=" + ApprovedDateFrom + "&dttoAD=" + (Convert.ToDateTime(ApprovedDateTo).AddDays(1)).ToShortDateString() + "&uidAD=" + User.Identity.Name.ToString() + "&sfAD=" + GetStatusFilterAD() + "&pfAD=" + GetProcessFilter();
                    break;
                }
            case Constants.Report.Name.DocSubSummaryRequests:
                {                    
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&mu=" + GetCountry() + "&sf=" + GetStatusFilter();
                    break;
                }

            case Constants.Report.Name.TravelSummaryRequests:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&sf=" + GetStatusTRF() + "&bf=" + GetBestCost() + "&mf=" + GetCountryTRF() + "&df=" + GetDepartment() + "&tf=" + GetTraveller() + "&cf=" + CostCenter + "&af=" + Accomodation + "&ff=" + FlightDetails + "&ef=" + GetEmployee() + "&startDate=" + txtTRPFlightStartDate.Text.Trim() + "&endDate=" + txtTRPFlightEndDate.Text.Trim();
                    break;
                }

            case Constants.Report.Name.CAPEXSummaryRequests:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&sf=" + GetStatusFilter() + "&af=" + GetARTypeFilter() + "&mf=" + GetCountry();
                    break;
                }

            case Constants.Report.Name.CAPEXRequests:
                {                                                     
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&adtfrom=" + ApprovedDateFrom + "&adtto=" + (Convert.ToDateTime(ApprovedDateTo).AddDays(1)).ToShortDateString() + "&uid=" + User.Identity.Name.ToString() + "&atf=" + GetARTypeFilter() + "&sf=" + GetStatusFilter() + "&cf=" + GetCountryFilter();
                    break;
                }

            case Constants.Report.Name.ReportBuilder:
                {
                    ParamValues = @"dfrom=" + SubmittedDateFrom + "&dto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&dfromA=" + ApprovedDateFrom + "&dtoA=" + (Convert.ToDateTime(ApprovedDateTo).AddDays(1)).ToShortDateString() + "&pf=" + GetProcessSelectedFilter() + "&mf=" + GetCountry() + "&df=" + GetDataFieldsFilter();
                    break;
                }
            case Constants.Report.Name.IPRSummaryRequest:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.CRPSummaryReport:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportDetailed:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractType:
                {
                    ParamValues = @"sContractType=" + ContractType;
                    break;
                }
            case Constants.Report.Name.CRPSummaryReportByContractTypeWithMU:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString() + "&sContractType=" + ContractType + "&sMUCode=" + Country;
                    break;
                }
            case Constants.Report.Name.SCRSummaryReport:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.SCRSummaryReportDetailed:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReport:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.ePRMasterTATReportByMU:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.PRMasterReport2:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country;
                    break;
                }
            case Constants.Report.Name.PRMasterReport3:
                {
                    ParamValues = @"prNumber_=" + PRNumber;
                    break;
                }
            case Constants.Report.Name.PRMasterReportByMU:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.PRMasterReportCurrentApprover:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.PRDetailReportByMU:
                {
                    ParamValues = @"prNumber_=" + PRNumber + "&marketUnit_=" + Country;
                    break;
                }
            case Constants.Report.Name.PROverallSummaryReportByMU:
                {
                    ParamValues = @"marketUnit_=" + Country;
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReport:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.ePRMasterDetailReportByMU:
                {
                    ParamValues = @"prtype_=" + PRType + "&marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.ePRSavingsReport:
                {
                    ParamValues = @"marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.ePRSavingsReportByMU:
                {
                    ParamValues = @"marketUnit_=" + Country + "&dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.CSRSummaryReport:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.CSRSummaryReportDetailed:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.SMRSummaryReport:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.SMRSummaryReportDetailed:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.OARSummaryReport:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }
            case Constants.Report.Name.OARSummaryReportDetailed:
                {
                    ParamValues = @"dtfrom=" + SubmittedDateFrom + "&dtto=" + (Convert.ToDateTime(SubmittedDateTo).AddDays(1)).ToShortDateString();
                    break;
                }

        }

        ReportViewerURL = ReportViewerURL.Replace("{ReportName}", ReportName).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");

        if (ParamValues.Trim() == "")
            ReportViewerURL = ReportViewerURL.Replace("&rc:Parameters=false&", "");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"<script language=javascript>window.open('" + ReportViewerURL + "', '', 'top=0, left=0,height=600, width=900, toolbar=no, scrollbars=yes, resizable=yes');</script>");
    }
    protected void chklCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chklCountry.SelectedValue == string.Empty)
        {
            ddlDepartment.Enabled = false;
            ddlDepartment.Items.Clear();
        }
        else
        {
            GetCountryTRF();            
        }
       
    }
    protected void ddl_ProcessFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_ProcessFilter.SelectedValue == string.Empty || ddl_ProcessFilter.SelectedValue == "Select")
        {
            cb_Datafields.Enabled = false;
            cb_Datafields.Items.Clear();
        }
        else
        {
            cb_Datafields.Enabled = true;
            PopulateDatafields();
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
