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

using Emerson.WF;

public partial class UI_Reports_ReportsOptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AddAttributes();
        ShowMenuItems();
    }

    private void AddAttributes()
    {
        hlSummaryOfInitiatedRequests.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.InitiatedRequests + "');");
        hlSummaryOfApprovedDeniedRequests.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.ApprovedDeniedRequests + "');");
        //hlReportBuilder.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.ReportBuilder + "');");
        hlSummaryOfSummaryRequests.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.DocSubSummaryRequests + "');");
        hlSummaryOfTravelRequests.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.TravelSummaryRequests + "');");
        hlSummaryOfCAPEXRequestsAll.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.CAPEXSummaryRequests + "');");
        hlSummaryOfCAPEXRequests.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.CAPEXRequests + "');");
        hlHCRSummary.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.CAPEXRequests + "');");
        hlSummaryOfITProjectIntake.Attributes.Add("onclick", "LoadReport('" + Constants.Report.Name.IPRSummaryRequest + "');");
    }

    private void ShowMenuItems()
    {
        try
        {
            trCAPEXSummary.Visible = ((UserPassport)Session["UserPassport"]).HasCAPEXReportsRights;
            trCAPEXSummaryAll.Visible = ((UserPassport)Session["UserPassport"]).HasCAPEXAllReportsRights;
            trDocSubSummary.Visible = ((UserPassport)Session["UserPassport"]).HasDocSubReportsRights;
            trTRPSummary.Visible = ((UserPassport)Session["UserPassport"]).HasTravelReportsRights;
            trIPRSummary.Visible = ((UserPassport)Session["UserPassport"]).HasIPRReportsRights;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains(Constants.ExceptionMessage.UserDoesNotExistInMasterConfig))
                trCAPEXSummary.Visible = false;

            else
                throw ex;
        }

        trCAPEX.Visible = trCAPEXSummary.Visible;
        trCAPEXAll.Visible = trCAPEXSummaryAll.Visible;
        trTRP.Visible = trTRPSummary.Visible;
        trDocSub.Visible = trDocSubSummary.Visible;
        trIPRTitle.Visible = trIPRSummary.Visible;
        
    }

}
