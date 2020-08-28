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

using System.Text;

using Emerson.WF.K2DataAccess;
using Emerson.WF.ADUtilities;
using Emerson.WF.AuditTrail;
using Emerson.WF;

public partial class UI_Search_RequestDetails : System.Web.UI.Page
{
    private K2SQLDataAccess K2SQLDA = new K2SQLDataAccess(ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
    private K2ROMDataAccess K2ROMDA = new K2ROMDataAccess(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"], ConfigurationManager.AppSettings["SourceCode.K2.ConnectionString"]);
    private K2ServiceManager k2MngDA = new K2ServiceManager(ConfigurationManager.AppSettings["SourceCode.K2.PlanServer"], ConfigurationManager.AppSettings["SourceCode.K2.ConnectionString"]);
    
    private int ProcInstID;
    private string ProcInstGUID;
    private string ReferenceID;
    private string PrevActivity;
    private string ProcInstProcSetID;
    private string ProcInstProcName;
    private string ReportViewerURL;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        ProcInstID = Convert.ToInt32(Request.QueryString["id"]);
        ReferenceID = Request.QueryString["ref"];

        if (ReferenceID.Trim() == string.Empty)
            ReferenceID = "Draft";

        lblReferenceID.Text = ReferenceID.Substring(0, (ReferenceID.IndexOf(' ') >= 0) ? ReferenceID.IndexOf(' ') : ReferenceID.Length);

        K2SQLDA.K2ServiceManager = k2MngDA;


    
        //lbtnBackToResults.Attributes.Add("onclick", "window.history.back(1); alert() return false;");

        if (!IsOKToView())
        {
            trAccessDenied.Visible = true;
            trDetailsOld.Visible = false;
            trDetailsNew.Visible = false;

            tdResubmit.Visible = false;
            tdCancel.Visible = false;
            tdRetrieve.Visible = false;
            tdPrint.Visible = false;
            
            tdActionResult.Visible = false;
        }

        else
        {
            trAccessDenied.Visible = false;
            //trDetails.Visible = true;

            ProcInstGUID = K2SQLDA.GetProcInstDataFieldValue(ProcInstID, Constants.DataField.ProcInstGuid);

            lbtnCancel.Attributes.Add("onclick", "return (confirm('" + Constants.Message.ConfirmCancel + "'));");
            lbtnRetrieveBack.Attributes.Add("onclick", "return (confirm('" + Constants.Message.ConfirmRetrieve + "'));");
            lbtnResubmit.Attributes.Add("onclick", "return (confirm('" + Constants.Message.ConfirmResubmit + "'));");


            string URL = string.Empty;
            bool bolShowPrintButton = false;
            string ParamValues = string.Empty;

            ReportViewerURL = System.Configuration.ConfigurationManager.AppSettings["ReportViewerURL"].ToString();
            ParamValues = @"pid=" + ProcInstID;

            ProcInstProcName = K2SQLDA.GetProcInstProcName(ProcInstID);
            ProcInstProcSetID = K2SQLDA.GetProcInstProcSetID(ProcInstID);

            switch (ProcInstProcName)
            {
                case Constants.ProcessName.CAPEX:
                    {
                        bolShowPrintButton = true;
                        ReportViewerURL = ReportViewerURL.Replace("{ReportName}", Constants.Report.Name.CAPEXPrintForm).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");
                        break;
                    }
                case Constants.ProcessName.ITR:
                    {
                        bolShowPrintButton = true;
                        ReportViewerURL = ReportViewerURL.Replace("{ReportName}", Constants.Report.Name.ITRPrintForm).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");
                        break;
                    }
                case Constants.ProcessName.HCR:
                    {
                        bolShowPrintButton = true;
                        ReportViewerURL = ReportViewerURL.Replace("{ReportName}", Constants.Report.Name.HCRPrintForm).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");
                        break;
                    }
                case Constants.ProcessName.TRP:
                    {
                        bolShowPrintButton = true;
                        ReportViewerURL = ReportViewerURL.Replace("{ReportName}", Constants.Report.Name.TRPPrintForm).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");
                        break;
                    }
                case Constants.ProcessName.EEP:
                    {
                        bolShowPrintButton = true;
                        ReportViewerURL = ReportViewerURL.Replace("{ReportName}", Constants.Report.Name.EEPPrintForm).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");
                        break;
                    }

                default:
                    {
                        bolShowPrintButton = false;
                        break;
                    }

            }

            if (bolShowPrintButton)
            {
                tdPrint.Visible = true;
                lbtnPrint.Attributes.Add("onclick", "ShowPrintableForm(true);");
            }
            else
            {
                tdPrint.Visible = false;
                lbtnPrint.Attributes.Add("onclick", "ShowPrintableForm(false);");
            }

          
            //if (K2SQLDA.GetProcInstProcName(ProcInstID).Contains("CAPEX"))
            //{
            //    tdPrint.Visible = true;
            //    lbtnPrint.Attributes.Add("onclick", "ShowPrintableForm(true);");

            //    string ParamValues = string.Empty;
            //    ReportViewerURL = System.Configuration.ConfigurationManager.AppSettings["ReportViewerURL"].ToString();

            //    ParamValues = @"pid=" + ProcInstID;

            //    ReportViewerURL = ReportViewerURL.Replace("{ReportName}", Constants.Report.Name.CAPEXPrintForm).Replace("{ParamValues}", ParamValues).Replace(@"\", @"\\");
            //}
            //else
            //{
            //    tdPrint.Visible = false;
            //    lbtnPrint.Attributes.Add("onclick", "ShowPrintableForm(false);");
            //}

            Page.ClientScript.RegisterStartupScript(this.GetType(), "PageJavaScript", GetPageJavaScript());

            bool HasViewForm = SetFormView();

                if (!Page.IsPostBack)
                {
                    if (!HasViewForm)
                    {
                        PopulateDetails();
                        tdActionResult.Visible = false;
                        trAuditTrail.Visible = false;
                        trRemarks.Visible = false;
                    }

                    ShowActionButtons();
                }

                if (!HasViewForm)
                    PopulateAuditTrail();

                if (!lblReferenceID.Text.Contains("ITR") && !HasViewForm)
                {
                    ucRemarks.ProcInstID = ProcInstID;
                    ucRemarks.ProcInstGuid = ProcInstGUID;
                    ucRemarks.ShowDataFields(false);
                }

        }

        ucRemarks.ViewOnly = true;

        lbtnBackToResults.Attributes.Add("onclick", "positionLoadingProgress();");
    }

    private bool SetFormView()
    {
        XMLDataAccess.MasterDataLocation = ConfigurationManager.AppSettings["XMLMasterDataLocation"].ToString();
        DataTable dtViewFormLinks = XMLDataAccess.GetMasterData(XMLDataAccess.XMLData.ViewFormLinks);

        DataRow[] drViewFormLink = dtViewFormLinks.Select("Code = '" + ProcInstProcSetID + "'");

        trDetailsOld.Visible = (drViewFormLink.Length == 0);
        trDetailsNew.Visible = (drViewFormLink.Length != 0);

        if (drViewFormLink.Length != 0)
        {
            ViewFormFrame.Attributes.Add("src", drViewFormLink[0]["Description"].ToString() + "?pid=" + ProcInstID);
            return true;
        }

        return false;

    }

    private void ShowActionButtons()
    {
        tdResubmit.Visible = K2SQLDA.IsProcInstOKToResubmit(ProcInstID, User.Identity.Name);
        tdCancel.Visible = K2SQLDA.IsProcInstOKToCancel(ProcInstID, User.Identity.Name);
        tdRetrieve.Visible = K2SQLDA.IsProcInstOKToRetrieve(ProcInstID, User.Identity.Name, ref PrevActivity);
    }

    private void PopulateDetails()
    {
        if (ViewState["Details"] == null)
            ViewState["Details"] = K2SQLDA.GetProcInstDataFields(Convert.ToInt32(ProcInstID));

        //gvRequestDetails.PageIndex = 0;

        gvRequestDetails.DataSource = (DataTable)ViewState["Details"];
        gvRequestDetails.DataBind();
    }

    private void PopulateAuditTrail()
    {

        BaseAuditTrail auditTrail = new BaseAuditTrail();

        List<BaseAuditTrail> audits;

        DataTable dt = new DataTable();
        dt.Columns.Add("dCreateAt");
        dt.Columns.Add("sUserName");
        dt.Columns.Add("sEventType");

        // get audit trail

        audits = auditTrail.GetBy<BaseAuditTrail>("", ProcInstGUID, "");

        try
        {

            foreach (BaseAuditTrail au in audits)
            {
                DataRow dr = dt.NewRow();

                dr["dCreateAt"] = au.CreatedAt.ToString("HH:mm MMM dd, yyyy");
                dr["sUserName"] = au.UserName;
                dr["sEventType"] = au.EventType.ToString();

                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count <= 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("PopulateAuditTrail : " + ex.Message);       
        }

        gvAuditTrail.DataSource = dt;
        gvAuditTrail.DataBind();
    }

    protected void gvRequestDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRequestDetails.PageIndex = e.NewPageIndex;
        PopulateDetails();
    }

    protected void gvAuditTrail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAuditTrail.PageIndex = e.NewPageIndex;
        PopulateAuditTrail();
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        if (K2SQLDA.IsProcInstOKToCancel(ProcInstID, User.Identity.Name))
        {
            K2ROMDA.CancelInstance(ProcInstID, User.Identity.Name);

            InsertAuditTrail(EventType.Cancel);

            ShowActionButtons();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Constants.Message.SuccessfulCancel + "');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Constants.Message.FailedCancel + "');</script>");
        }
        //tdActionResult.Visible = true;
        //lblActionResult.Text = Constants.Message.SuccessfulCancel;       
    }

    protected void lbtnRetrieveBack_Click(object sender, EventArgs e)
    {
        if (K2SQLDA.IsProcInstOKToRetrieve(ProcInstID, User.Identity.Name, ref PrevActivity))
        {
            int[] WorklistItemID = new int[1];
            string SerialNo = string.Empty;

            try
            {
                SerialNo = K2SQLDA.GetProcInstDataFieldValue(ProcInstID, Emerson.WF.Constants.DataField._wfFinishOnRetrieveBack).ToString().Trim();
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains(Constants.ERR.WF_001)) // datafield does not exist
                    throw ex;
                else
                {
                    //WorklistItemID = new int[1];
                    WorklistItemID[0] = -1;
                }
            }
            
            if (SerialNo != string.Empty)
            {
                string[] arrSerialNo = SerialNo.Split(';');

                WorklistItemID = new int[arrSerialNo.Length];

                for (int i = 0; i < arrSerialNo.Length; i++)
                {
                    System.Data.DataRow dr = K2SQLDA.GetWorklistItemInfo(arrSerialNo[i].Trim());
                    WorklistItemID[i] = Convert.ToInt32(dr["WorklistItemID"].ToString());
                }

            }

            K2ROMDA.RetrieveBackInstance(ProcInstID, PrevActivity, WorklistItemID);

            InsertAuditTrail(EventType.Retrieveback);

            ShowActionButtons();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Constants.Message.SuccessfulRetrieve + "');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Constants.Message.FailedRetrieve + "');</script>");
        }

        //tdActionResult.Visible = true;
        //lblActionResult.Text = 
    }

    protected void lbtnResubmit_Click(object sender, EventArgs e)
    {
        if (K2SQLDA.IsProcInstOKToResubmit(ProcInstID, User.Identity.Name))
        {
            K2ROMDA.ResubmitInstance(ProcInstID);

            InsertAuditTrail(EventType.Resubmit);

            ShowActionButtons();

            tdRetrieve.Visible = false;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Constants.Message.SuccessfulResubmit + "');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=javascript>alert('" + Constants.Message.FailedResubmit + "');</script>");
        }
        //tdActionResult.Visible = true;
        //lblActionResult.Text = Constants.Message.SuccessfulResubmit;
    }

    private void InsertAuditTrail(EventType et)
    {
        BaseAuditTrail baseAuditTrail = new BaseAuditTrail();

        baseAuditTrail.EventType = et;
        baseAuditTrail.CreatedAt = DateTime.Now;
        baseAuditTrail.ProcessID = ReferenceID.Substring(1, 1);
        baseAuditTrail.UserName = User.Identity.Name.ToString();
        baseAuditTrail.ProcessName = K2SQLDA.GetProcInstProcName(ProcInstID);
        baseAuditTrail.RequestID = ProcInstGUID;
        baseAuditTrail.Serialize();
        baseAuditTrail.Insert();

        PopulateAuditTrail();
    }

    protected void lbtnSwitch_Click(object sender, EventArgs e)
    {
        if (lbtnSwitch.Text.Trim().ToUpper() == "VIEW AUDIT TRAIL/REMARKS")
        {
            gvRequestDetails.Visible = false;
            trAuditTrail.Visible = true;
            trRemarks.Visible = true;
            //ucRemarks.Visible = true;
            lbtnSwitch.Text = "View Details";
        }
        else
        {
            gvRequestDetails.Visible = true;
            trAuditTrail.Visible = false;
            trRemarks.Visible = false;
            lbtnSwitch.Text = "View Audit Trail/Remarks";
        }

    }

    private bool IsOKToView()
    {
        

        if (ViewState["OKToView"] == null)
        {
            string ADGroups = string.Empty;

            // 12/19/2007 abrs
            /*
            ActiveDirectory AD = new ActiveDirectory(ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Domain"], ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Username"], ConfigurationManager.AppSettings["Emerson.WF.ADUtilities.ADConnector.Password"]);
            string[] arrADGroups = AD.GetGroupsOfUser(User.Identity.Name.ToString().Substring(User.Identity.Name.ToString().LastIndexOf(@"\") + 1)).ToArray();

            foreach (string strGroup in arrADGroups)
            {
                ADGroups += strGroup;
            }
            */

            ViewState["OKToView"] = k2MngDA.IsProcInstOKToView(ProcInstID, User.Identity.Name.ToString(), ADGroups, ConfigurationManager.AppSettings["Emerson.WF.SQLUtilities.SQLHelper.ConnectionString"]);
        }

        return (bool)ViewState["OKToView"];
    }

    protected void lbtnBackToResults_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["SearchResultUrl"].ToString());
    }

    private string GetPageJavaScript()
    {
        StringBuilder strJS = new StringBuilder();

        strJS.Append("<script language='javascript' type='text/javascript'> \n");
        strJS.Append("<!-- \n\n");

        strJS.Append("function ShowPrintableForm(show)\n");
        strJS.Append("{\n");

        strJS.Append("  if (!show)\n");
        strJS.Append("      return false;\n");

        strJS.Append("  window.open('" + ReportViewerURL + "', '', 'top=0, left=0,height=600, width=900, toolbar=no, scrollbars=yes, resizable=yes'); \n");

        strJS.Append("  return false;\n");
        strJS.Append("}\n");

        strJS.Append("\n//--> \n");
        strJS.Append("</script>\n\n");
        return strJS.ToString();
    }
}
