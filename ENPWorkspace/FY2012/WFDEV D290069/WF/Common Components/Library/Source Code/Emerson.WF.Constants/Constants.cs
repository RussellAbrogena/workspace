using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

namespace Emerson.WF
{
    public static class Constants
    {
        /* Used for delimited strings storing multiple values */
        public const string Delimiter = ";";

        public struct DateFormat
        {
            public const string DateTime = "{0:MMM dd, yyyy HH:mm}";
            public const string TimeDate = "{0:HH:mm MMM dd, yyyy}";
        }

        /* BEGIN Process Instance Status Values*/
        public struct Status
        {
            public const string Completed = "COMPLETED";
            public const string Cancelled = "CANCELLED";
            public const string Pending = "PENDING";
            public const string SentBack = "SENTBACK";
            public const string Approved = "APPROVED";
            public const string Declined = "DECLINED";
            public const string Rejected = "REJECTED";
            public const string Error = "ERROR";
        }
        /* END   Process Instance Status Values */

        /* BEGIN Process Activity Names */
        public struct Activity
        {
            public const string Final = "FINAL STATUS";
            public const string Draft = "PENDING FOR SUBMIT";

            public const string Cancelled = "CANCELLED";
        }
        /* END   Process Activity Names */


        /* BEGIN Starndardized Data Field Names*/
        public struct DataField
        {
            public const string PreviousApprover = "_PrevApprover";
            public const string PreviousActivity = "_PrevActivity";
            public const string ProcInstGuid = "_GUID";
            public const string Remarks = "Remarks";
            public const string RemarksTemp = "RemarksTmp";
            public const string ReqStatus = "_ReqStats";

            public const string _InBehalfUserFQN = "_InBehalfUserFQN";
            public const string _CancelUserFQN = "_CancelUserFQN";
            public const string _wfFinishOnRetrieveBack = "_wfFinishOnRetrieveBack";
            public const string _wfUpdateOnlyOnRetieveBack = "_wfUpdateOnlyOnRetieveBack";

            public const string wfAction = "wfAction";
        }
        /* END  Starndardized Data Field Names*/

        /* BEGIN Messages */
        public struct Message
        {
            public const string ConfirmRetrieve = "This request will be SENT BACK to your worklist. Proceed?   ";
            public const string ConfirmCancel = "This will CANCEL this request in workflow. Proceed?   ";
            public const string ConfirmResubmit = "This will RESUBMIT this request in workflow. Proceed?   ";

            public const string FailedRetrieve = "Unable to retrieve back the item to your worklist.   ";
            public const string FailedCancel = "Cancellation of request failed.   ";
            public const string FailedResubmit = "Unable to resubmit request.   ";

            public const string SuccessfulRetrieve = "Request has been successfully RETRIEVED.   ";
            public const string SuccessfulCancel = "Request has been successfully CANCELLED.   ";
            public const string SuccessfulResubmit = "Request has been successfully RESUBMITTED.   ";

            public const string SaveSuccessful = "Save Successful.";
            
        }
        /* END  Messages */

        /* BEGIN Reports */
        public struct Report
        {
            public struct Name // IMPT: String values should EXACTLY correspond to the name of the .rdl file
            {
                public const string InitiatedRequests = "InitiatedRequests";
                public const string ApprovedDeniedRequests = "ApprovedDeniedRequests";
                public const string ReportBuilder = "ReportBuilder";
                public const string DocSubSummaryRequests = "SummaryRequests";
                public const string TravelSummaryRequests = "TravelSummaryRequests";
                public const string CAPEXSummaryRequests = "CAPEXRequestsAll";
                public const string CAPEXRequests = "CAPEXRequests";
                public const string CAPEXPrintForm = "CAPEXPrintForm";
                public const string ITRPrintForm = "ITRPrintForm";
                public const string HCRPrintForm = "HCRPrintForm";
                public const string DSRPrintForm = "DSRPrintForm";
                public const string TRPPrintForm = "TRPPrintForm";
                public const string EEPPrintForm = "EEPPrintForm";
                public const string CRPPrintForm = "CRPPrintForm";
                public const string IPRPrintForm = "IPRPrintForm";
                public const string ITR3PrintFrom = "ITR3PrintFrom";
                public const string INVSummaryReports = "INVSummaryReports";
                public const string HCRSummaryReports = "HCRSummaryReports"; 
                public const string COPSummaryReportB = "COPSummaryReportB";
                public const string COPSummaryReportC = "COPSummaryReportC";
                public const string IPRSummaryRequest = "IPRSummaryRequest";
                public const string COPSummaryReports = "COPSummaryReports";
                public const string CRPSummaryReport = "CRPSummaryReport";
                public const string CRPSummaryReportDetailed = "CRPSummaryReportDetailed";
                public const string SCRSummaryReport = "SCRSummaryReport";
                public const string SCRSummaryReportDetailed = "SCRSummaryReportDetailed";
                public const string SCRSummaryReportTotal = "SCRSummaryReportTotal";
                public const string SCRSummaryReportAll = "SCRSummaryReportAll";
                public const string CRPSummaryReportByContractType = "CRPSummaryReportByContractType";
                public const string CRPSummaryReportByContractTypeWithMU = "CPRSummaryReportByContractTypeWithMU";
                public const string PRMasterReport2 = "PRMasterReport2";
                public const string PRMasterReportByMU = "PRMasterReportByMU";
                public const string PRMasterReport3 = "PRMasterReport3";
                public const string PRDetailReportByMU = "PRDetailReportByMU";
                public const string PRMasterReportCurrentApprover = "PRMasterReportCurrentApprover";
                public const string PROverallSummaryReport = "PROverallSummaryReport";
                public const string PROverallSummaryReportByMU = "PROverallSummaryReportByMU";
                public const string ePRMasterDetailReport = "ePRMasterDetailReport";
                public const string ePRMasterDetailReportByMU = "ePRMasterDetailReportByMU";
                public const string ePRMasterTATReport = "ePRMasterTATReport";
                public const string ePRMasterTATReportByMU = "ePRMasterTATReportByMU";
                public const string ePRSavingsReport = "ePRSavingsReport";
                public const string ePRSavingsReportByMU = "ePRSavingsReportByMU";
                public const string CSRSummaryReport = "CSRSummaryReport";
                public const string CSRSummaryReportDetailed = "CSRSummaryReportDetailed";
                public const string CSRSummaryReportTotal = "CSRSummaryReportTotal";
                public const string CSRSummaryReportAll = "CSRSummaryReportAll";
                public const string OARSummaryReport = "OARSummaryReport";
                public const string OARSummaryReportDetailed = "OARSummaryReportDetailed";
                public const string OARSummaryReportTotal = "OARSummaryReportTotal";
                public const string OARSummaryReportAll = "OARSummaryReportAll";
                public const string SMRSummaryReport = "SMRSummaryReport";
                public const string SMRSummaryReportDetailed = "SMRSummaryReportDetailed";
                public const string SMRSummaryReportTotal = "SMRSummaryReportTotal";
                public const string SMRSummaryReportAll = "SMRSummaryReportAll";

            }

            public struct DisplayName 
            {
                public const string InitiatedRequests = "Summary of Initiated Requests";
                public const string ApprovedDeniedRequests = "Summary of Approved & Declined Requests";
                public const string ReportBuilder = "Report Builder";
                public const string DocSubSummaryRequests = "Summary of Document Submission Requests";
                public const string TravelSummaryRequests = "Summary of Travel Requests";
                public const string CAPEXSummaryRequests = "Summary of CAPEX Request";
                public const string CAPEXRequests = "Summary of CAPEX Requests";
                public const string CAPEXPrintForm = "CAPEX Printable Form";
                public const string DSRPrintForm = "DSR Printable Form";
                public const string ITRPrintForm = "ITR Printable Form";
                public const string HCRPrintForm = "HCR Printable Form";
                public const string TRPPrintForm = "TRP Printable Form";
                public const string EEPPrintForm = "EEP Printable Form";
                public const string CRPPrintForm = "CRP Print Form";
                public const string IPRPrintForm = "IPR Print Form";
                public const string ITR3PrintFrom = "ITR Print From";
                public const string INVSummaryRequests = "Invoice Summary Requests";
                public const string HCRSummaryRequests = "Headcount Summary Requests";
                public const string COPSummaryRequestB = "Summary Report";
                public const string COPSummaryRequestC = "Summary of SO Turnaround Time";
                public const string IPRSummaryRequest = "Summary of IT Project Intake Request";
                public const string COPSummaryReports = "Summary of COP Request";
                public const string CRPSummaryReport = "Summary of AP Contract Requests";
                public const string CRPSummaryReportDetailed = "Detailed AP Contract Requests";
                public const string SCRSummaryReport = "Summary of Service AP Contract Requests";
                public const string SCRSummaryReportDetailed = "Detailed Service Contract Requests";
                public const string SCRSummaryReportTotal = "Summary of Service Contract Requests by Contract Type";
                public const string SCRSummaryReportAll = "Summary of All Service Contract Requests";
                public const string CRPSummaryReportByContractType = "Summary of Requests by Contract Type";
                public const string CRPSummaryReportByContractTypeWithMU = "Summary of Requests by Contract Type with MU";

                public const string ePRMasterTATReport = "Purchase Requisition Turn Around Time Report";
                public const string ePRMasterTATReportByMU = "Purchase Requisition Turn Around Time Report By MU";
                public const string PRMasterReport2 = "Summary of Purchase Requisition Requests";
                public const string PRMasterReportByMU = "Summary of Purchase Requisition Requests By MU";
                public const string PRMasterReport3 = "Purchase Requisition Item Detail Report";
                public const string PRDetailReportByMU = "Purchase Requisition Item Detail Report By MU";
                public const string PRMasterReportCurrentApprover = "Summary of Pending Requests with Current Approver By MU";
                public const string PROverallSummaryReport = "Overall Summary of Purchase Requests Report";
                public const string PROverallSummaryReportByMU = "Overall Summary of Purchase Requests Report By MU";
                public const string ePRMasterDetailReport = "All PR Master Detail Report";
                public const string ePRMasterDetailReportByMU = "All PR Master Detail Report By MU";
                public const string ePRSavingsReport = "Purchase Requisition Savings Report";
                public const string ePRSavingsReportByMU = "Purchase Requisition Savings Report By MU";

                public const string CSRSummaryReport = "Summary of Customer Setup Requests";
                public const string CSRSummaryReportDetailed = "Detailed Customer Setup Requests";
                public const string SMRSummaryReport = "Summary of Supplier Maintenance Requests";
                public const string SMRSummaryReportDetailed = "Detailed Supplier Maintenance Requests";
                public const string OARSummaryReport = "Summary of AP Oracle Access Requests";
                public const string OARSummaryReportDetailed = "Detailed AP Oracle Access Requests";
             
            }
        }
        /* END Reports */

        /* BEGIN Error Message Types */
        public struct ErrorMessage
        {
            public const string SystemError = "SystemError";
            public const string SessionExpired = "SessionExpired";
        }
        /* END Error Message Types */

        /* BEGIN Process Names */
        public struct ProcessName
        {
            public const string CAPEX = "CAPEX Request";
            public const string ITR = "IT Request Process 3.0";
            public const string PDC = "Price Discount for Channels";
            public const string HCR = "Headcount Request";
            public const string TRP = "Travel Request";
            public const string DSR = "Document Submission Request";
            public const string INV = "Invoice Process Requests";
            public const string EEP = "Employee Exit Request";
            public const string EEP2 = "Employee Exit Request 2.0";
            public const string TRP2 = "Travel Request 2.0";
            public const string CAPEX3 = "CAPEX Request 3.0";
            public const string ITR4 = "IT Request Process 4.0";
            public const string CRP = "AP Contract Request Process";
            public const string SCR = "Service Contract Request Process";
            public const string SMR = "Supplier Maintenance Request Process";
            public const string CSR = "Customer Setup Request Process";
            public const string OAR = "AP Oracle Access Request Process";

        }
        /* END Error Message Types */


        /* BEGIN Process Names */
        public struct ExceptionMessage
        {
            public const string UserDoesNotExistInMasterConfig = "User does not exist in User List table.";
        }
        /* END Error Message Types */

        /* BEGIN Custom Error Numbers */
        public struct ERR
        {
            public const string WF_001 = "ENP_WF_ERROR_001"; // Data Field does not exist
            public const string WF_002 = "ENP_WF_ERROR_002"; // Worklist Item does not exist

        }
        /* END Custom Error Numbers */
     
    }

}
