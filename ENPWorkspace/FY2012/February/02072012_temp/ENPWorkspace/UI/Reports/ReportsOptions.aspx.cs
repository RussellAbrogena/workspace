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
        hlSummaryOfInitiatedRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.InitiatedRequests + "');");
        hlSummaryOfApprovedDeniedRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ApprovedDeniedRequests + "');");
        hlSummaryOfSummaryRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.DocSubSummaryRequests + "');");
        hlSummaryOfTravelRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.TravelSummaryRequests + "');");
        hlSummaryOfCAPEXRequestsAll.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CAPEXSummaryRequests + "');");
        hlSummaryOfCAPEXRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CAPEXRequests + "');");
        //hlHCRSummaryRpt.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CAPEXRequests + "');");
        hlSummaryOfITProjectIntake.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.IPRSummaryRequest + "');");
        hlSummaryOfContractRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CRPSummaryReport + "');");
        hlSummaryOfContractRequestsDetailed.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CRPSummaryReportDetailed + "');");
        hlSummaryOfContractType.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CRPSummaryReportByContractType + "');");
        hlSummaryOfContractTypeWithMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CRPSummaryReportByContractTypeWithMU + "');");        
        hlSummaryOfServiceContractRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.SCRSummaryReport + "');");
        hlSummaryOfServiceContractRequestsDetailed.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.SCRSummaryReportDetailed + "');");
        hlSummaryOfPR.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PRMasterReport2 + "');");
        hlSummaryOfPRDetailed.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ePRMasterTATReport + "');");
        hlSummaryOfPRItem.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PRMasterReport3 + "');");
        hlSummaryOfPRByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PRMasterReportByMU + "');");
        hlSummaryOfPendingPRByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PRMasterReportCurrentApprover + "');");
        hlPROverallSummaryReport.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PROverallSummaryReport + "');");
        hlPRDetailReportByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PRDetailReportByMU + "');");
        hlPROverallSummaryReportByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.PROverallSummaryReportByMU + "');");
        hlPRMasterDetailReport.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ePRMasterDetailReport + "');");
        hlPRMasterDetailReportByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ePRMasterDetailReportByMU + "');");
        hlPRSummaryDetailedByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ePRMasterTATReportByMU + "');");
        hlEPRSavingReport.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ePRSavingsReport + "');");
        hlEPRSavingReportByMU.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.ePRSavingsReportByMU + "');");
        hlSummaryOfAPOracleAccessRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.OARSummaryReport + "');");
        hlSummaryOfAPOracleAccessRequestsDetailed.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.OARSummaryReportDetailed + "');");
        hlSummaryOfCustomerSetupRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CSRSummaryReport + "');");
        hlSummaryOfCustomerSetupRequestsDetailed.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.CSRSummaryReportDetailed + "');");
        hlSummaryOfSupplierMaintenanceRequests.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.SMRSummaryReport + "');");
        hlSummaryOfSupplierMaintenanceRequestsDetailed.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.SMRSummaryReportDetailed + "');");
        hlTSRRequestSummaryFormatted.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.TSRSummaryReportFormatted + "');");
        hlTSRRequestSummaryNonFormatted.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.TSRSummaryReportNonFormatted + "');");
        hlTSRAdminReport.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.TSRAdminReport + "');");
        hlTSRMUReport.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.TSRMUReport + "');");
        hlHRTSummaryRpt.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.HRTSummaryReport + "');");
        hlHRTMonthlyRpt.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.HRTMonthlyReport + "');");
        hlHRTApprovedRpt.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.HRTApprovedReport + "');");
        hlHRTPendingRpt.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.HRTPendingReport + "');");
        hlEEPSummaryRpt.Attributes.Add("onclick", "LoadReport('" + Konstants.Report.Name.EEPSummaryReport + "');");
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
            trContractSummary.Visible = ((UserPassport)Session["UserPassport"]).HasCRPReportsRights;
            trContractDetailed.Visible = ((UserPassport)Session["UserPassport"]).HasCRPReportsRights;
            trContractType.Visible = ((UserPassport)Session["UserPassport"]).HasCRPReportsRightsByContractType;
            trContractTypeWithMU.Visible = ((UserPassport)Session["UserPassport"]).HasCRPReportsRightsByContractTypeWithMU;
            trServiceContractSummary.Visible = ((UserPassport)Session["UserPassport"]).HasSCRReportsRights;
            trServiceContractDetailed.Visible = ((UserPassport)Session["UserPassport"]).HasSCRReportsRights;
            trPRSummary.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;
            trPRSummaryDetailed.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;
            trPRSummaryItem.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;
            trPROverallSummaryReport.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;
            trPRMasterDetailReport.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;
            trEPRSavingReport.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;
            //trPRSummaryByMU.Visible = ((UserPassport)Session["UserPassport"]).HasPRReportsRights;

            trAPOracleAccess.Visible = ((UserPassport)Session["UserPassport"]).HasOARReportsRights;
            trAPOracleAccessDetailed.Visible = ((UserPassport)Session["UserPassport"]).HasOARReportsRights;
            trAPOracleAccessSummary.Visible = ((UserPassport)Session["UserPassport"]).HasOARReportsRights;
            trCustomerSetup.Visible = ((UserPassport)Session["UserPassport"]).HasCSRReportsRights;
            trCustomerSetupDetailed.Visible = ((UserPassport)Session["UserPassport"]).HasCSRReportsRights;
            trCustomerSetupSummary.Visible = ((UserPassport)Session["UserPassport"]).HasCSRReportsRights;
            trSupplierMaintenance.Visible = ((UserPassport)Session["UserPassport"]).HasSMRReportsRights;
            trSupplierMaintenanceDetailed.Visible = ((UserPassport)Session["UserPassport"]).HasSMRReportsRights;
            trSupplierMaintenanceSummary.Visible = ((UserPassport)Session["UserPassport"]).HasSMRReportsRights;

            trTimeSURE.Visible = ((UserPassport)Session["UserPassport"]).HasTSRReportsRights;
            trTSRSummaryRep1.Visible = ((UserPassport)Session["UserPassport"]).HasTSRReportsRights;
            trTSRSummaryRep2.Visible = ((UserPassport)Session["UserPassport"]).HasTSRReportsRights;
            trTSRAdminRep.Visible = ((UserPassport)Session["UserPassport"]).HasTSRReportsRights;
            trTSRMURep.Visible = ((UserPassport)Session["UserPassport"]).HasTSRReportsRights;

            trHRT.Visible = ((UserPassport)Session["UserPassport"]).HasHRTReportRights;
            trHRTSummaryRpt.Visible = ((UserPassport)Session["UserPassport"]).HasHRTReportRights;
            trHRTMonthlyRpt.Visible = ((UserPassport)Session["UserPassport"]).HasHRTReportRights;
            trHRTApprovedRpt.Visible = ((UserPassport)Session["UserPassport"]).HasHRTReportRights;
            trHRTPendingRpt.Visible = ((UserPassport)Session["UserPassport"]).HasHRTReportRights;

            trEEP.Visible = ((UserPassport)Session["UserPassport"]).HasEEPReportRights;
            trEEPSummaryRpt.Visible = ((UserPassport)Session["UserPassport"]).HasEEPReportRights;

            trPRSummaryByMU.Visible = true;
            trPRSummaryCurrentApproverByMU.Visible = true;
            trPRDetailReportByMU.Visible = true;
            trPROverallSummaryReportByMU.Visible = true;
            trPRMasterDetailReportByMU.Visible = true;
            trPRSummaryDetailedByMU.Visible = true;
            trEPRSavingReportByMU.Visible = true;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains(Konstants.ExceptionMessage.UserDoesNotExistInMasterConfig))
                trCAPEXSummary.Visible = false;

            else
                throw ex;
        }

        trCAPEX.Visible = trCAPEXSummary.Visible;
        trCAPEXAll.Visible = trCAPEXSummaryAll.Visible;
        trTRP.Visible = trTRPSummary.Visible;
        trDocSub.Visible = trDocSubSummary.Visible;
        trIPRTitle.Visible = trIPRSummary.Visible;
        //trPR.Visible = trPRSummary.Visible;
        //trPR.Visible = trPRSummaryDetailed.Visible;
        //trPR.Visible = trPRSummaryItem.Visible;
        //trPR.Visible = trPRSummaryByMU.Visible;
        //trPR.Visible = trPROverallSummaryReport.Visible;
        trPR.Visible = true;
        trContract.Visible = trContractSummary.Visible;
        trContractDetailed.Visible = trContractDetailed.Visible;
        trContractType.Visible = trContractType.Visible;
        trContractTypeWithMU.Visible = trContractTypeWithMU.Visible;
        trServiceContract.Visible = trServiceContractSummary.Visible;
        trServiceContractDetailed.Visible = trServiceContractDetailed.Visible;
    }

}
