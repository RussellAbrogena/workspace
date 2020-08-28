<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportsOptions.aspx.cs" Inherits="UI_Reports_ReportsOptions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reports Options</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">BODY { MARGIN: 0px }
	        .style5 { FONT-SIZE: 11px; FONT-FAMILY: Tahoma }
	        .style7 { FONT-WEIGHT: bold; COLOR: #ffffff }
	        .style9 { FONT-SIZE: 11px; COLOR: #1e3c7c; FONT-FAMILY: Tahoma }
	        .style12 { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #1e3c7c; FONT-FAMILY: Tahoma }
	</style>
	<script language="javascript" type="text/javascript">
	function toggleMoreOptions(toggle)
	{    
	    if (toggle)
	    {
	        var str = document.all("iconMoreOptions").src;
	        
	        if (str.search("expand") > 0)
	        {
	            document.all("iconMoreOptions").src = str.replace(/expand/, "collapse");
	            document.all("trMoreOptions").style.display = "inline";
	        }
	        else
	        {
	            document.all("iconMoreOptions").src = str.replace(/collapse/, "expand");
	            document.all("trMoreOptions").style.display = "none";
	        }
    	    
	        document.all("hdnMoreOptions").value = document.all("iconMoreOptions").src;
        }
        else
        {
            if (document.all("hdnMoreOptions").value != '')
            {
               var str = document.all("hdnMoreOptions").value;
               document.all("iconMoreOptions").src = document.all("hdnMoreOptions").value;
            }
            else
            {
                document.all("hdnMoreOptions").value = document.all("iconMoreOptions").src;
                return;
            }
               
            if (str.search("expand") > 0)
	            document.all("trMoreOptions").style.display = "none";
	        else
	            document.all("trMoreOptions").style.display = "inline";
        }	    
        
        if (document.all("trMoreOptions").style.display == "inline")
            document.all("iconMoreOptions").title = "Close More Options"
        else
            document.all("iconMoreOptions").title = "View More Options"
	}
	
	function toggleReportsOptions(toggleReports)
	{    
	    if (toggleReports)
	    {
	        var str = document.all("iconReportsOptions").src;
	        
	        if (str.search("expand") > 0)
	        {
	            document.all("iconReportsOptions").src = str.replace(/expand/, "collapse");
	            document.all("trReportsOptions").style.display = "inline";
	        }
	        else
	        {
	            document.all("iconReportsOptions").src = str.replace(/collapse/, "expand");
	            document.all("trReportsOptions").style.display = "none";
	        }
    	    
	        document.all("hdnReportsOptions").value = document.all("iconReportsOptions").src;
        }
        else
        {
            if (document.all("hdnReportsOptions").value != '')
            {
               var str = document.all("hdnReportsOptions").value;
               document.all("iconReportsOptions").src = document.all("hdnReportsOptions").value;
            }
            else
            {
                document.all("hdnReportsOptions").value = document.all("iconReportsOptions").src;
                return;
            }
               
            if (str.search("expand") > 0)
	            document.all("trReportsOptions").style.display = "none";
	        else
	            document.all("trReportsOptions").style.display = "inline";
        }	    
        
        if (document.all("trReportsOptions").style.display == "none")
            document.all("iconReportsOptions").title = "View Reports Options"
        else
            document.all("iconReportsOptions").title = "Close Reports Options"
	}	
	
	function LoadReport(reportName)
	{
	    var reportViewerFrame = parent.frames['MainFrame'];
		    
		    		    
//	    switch (reportName)
//	    {
//	        case 'Summary of Initiated Requests':
//	        {
//	            break;
//	        }
////	        case 'Second Item goes here...':
////	        {
////	            break;
////	        }
//	    }

		reportViewerFrame.location = "ReportViewer.aspx?rname=" + reportName;

	}
	
	</script>
</head>
<body onload="toggleMoreOptions(false);toggleReportsOptions(false);">
    <form id="form1" runat="server">
    <table cellspacing="2" cellpadding="4" border="0" style="vertical-align: top; width: 100%; height:100%">
												            <tr>
													            <td style="width: 188px; border: solid 1px #92AEC9; background-color:#D2DEE9; font-weight: bold; color:#000000"><span><img alt="" id="iconReportsOptions" style="cursor:hand;" src="../../Images/icon_collapse.GIF" onclick="toggleReportsOptions(true);" title="Close Reports Options"/></span>&nbsp;&nbsp;&nbsp;<span>Report Listing</span></td>
												            </tr>
												            
												            <tr id="trReportsOptions">
													            <td style="height:100%; width: 188px; border: solid 1px #92AEC9;">
													            <table cellpadding = "1" cellspacing="2">
													                <tr style="height:3px"><td>
													                <!--img src="../../Images/spacer.gif" alt="" /-->
													                </td></tr>
													                <tr id="trGeneral" runat="server"><td>
													                <span><u><b>General</b></u></span>
													                </td></tr>
													                <tr><td>
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfInitiatedRequests" runat="server"  NavigateUrl="#">Summary of Initiated Requests</asp:HyperLink>
													                </td></tr>
													                <tr><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfApprovedDeniedRequests" runat="server"  NavigateUrl="#">Summary of Approved & Declined Requests</asp:HyperLink>
													                </td></tr>
													                <tr id="tr1" runat="server"><td>
													                </td></tr>
													            <%--    <tr id="trReportBuilder" runat="server"><td>
													                <span><u><b>Report Builder</b></u></span>
													                </td></tr>
													                <tr><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlReportBuilder" runat="server"  NavigateUrl="#">Create your own report</asp:HyperLink>
													                </td></tr>	--%>												                
													                  <tr><td valign="top"></td></tr>
													                  <tr id="trCAPEXAll" runat="server"><td style="height: 15px">
													                <span><u><b>CAPEX Summary</b></u></span>
													                </td></tr>
													                 <tr id="trCAPEXSummaryAll" runat="server"><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfCAPEXRequestsAll" runat="server"  NavigateUrl="#">Summary of Requests</asp:HyperLink>
													                </td></tr>
													                 <tr style="height:8px;"><td></td></tr>
													                 <tr id="trIPRSummary" runat="server"><td style="height: 15px">
													                <span><u><b>IT Project Intake Request</b></u></span>
													                </td></tr>
													                 <tr id="trIPRTitle" runat="server"><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfITProjectIntake" runat="server"  NavigateUrl="#">Summary of Requests</asp:HyperLink>
													                </td></tr>
													                <tr style="height:8px;"><td></td></tr>
													                 <tr id="trDocSub" runat="server"><td style="height: 15px">
													                <span><u><b>Document Submission</b></u></span>
													                </td></tr>
													                 <tr id="trDocSubSummary" runat="server"><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfSummaryRequests" runat="server"  NavigateUrl="#">Summary of Requests</asp:HyperLink>
													                </td></tr>
													                 <tr style="height:8px;"><td></td></tr>
													                 <tr id="trHCRSummary" runat="server"><td style="height: 15px">
													                <span><u><b>Headcount Request</b></u></span>
													                </td></tr>
													                 <tr id="tr3" runat="server"><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlHCRSummary" runat="server"  NavigateUrl="#">Summary of Requests</asp:HyperLink>
													                </td></tr>
                                                                    <tr style="height:8px;"><td></td></tr>
                                                                    
                                                                     <tr id="trTRP" runat="server"><td style="height: 15px">
													                <span><u><b>Travel Request</b></u></span>
													                </td></tr>
													                 <tr id="trTRPSummary" runat="server"><td valign="top">
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfTravelRequests" runat="server"  NavigateUrl="#">Summary of Requests</asp:HyperLink>
													                </td></tr>
													                

                                                                    <tr style="height:8px;"><td></td></tr>
                                                                    
													                <tr id="trCAPEX" runat="server"><td style="height: 15px">
													                <span><u><b>CAPEX</b></u></span>
													                </td></tr>
													                <tr id="trCAPEXSummary" runat="server"><td>
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlSummaryOfCAPEXRequests" runat="server"  NavigateUrl="#">Summary of Requests</asp:HyperLink>
													                </td></tr>
													                <!--tr><td>
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="HyperLink4" runat="server"  NavigateUrl="#">FI Report 1</asp:HyperLink>
													                </td></tr>
													                <tr><td>
													                <span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="HyperLink5" runat="server"  NavigateUrl="#">FI Report 2</asp:HyperLink>
													                </td></tr-->
													                <tr style="height:3px"><td>
													                <!--img src="../../Images/spacer.gif" alt="" /-->
													                </td></tr>													                                                                                    
													 
													                <tr style="height:3px"><td>
													                <!--img src="../../Images/spacer.gif" alt="" /-->
													                </td></tr>
													            </table>
													            </td>
												            </tr>
												            <tr>
													            <td style="display:none; width: 188px; border: solid 1px #92AEC9; background-color:#D2DEE9; font-weight: bold; color:#000000"><span><img alt="" id="iconMoreOptions" style="cursor:hand;" src="../../Images/icon_expand.GIF" onclick="toggleMoreOptions(true);" title="View More Options"/></span>&nbsp;&nbsp;&nbsp;<span>More Options</span></td>
												            </tr>
												            <tr id="trMoreOptions" style="display: none">
													            <td style="display:none; width: 188px; border: solid 1px #92AEC9; height: 30px;">
                                                                </td>
												            </tr>
											            </table>				                                   
            <input id="hdnIsPostBack" runat="server" style="display: none" type="hidden" value="false" />&nbsp;
            <asp:TextBox id="hdnMoreOptions" runat="server" style="display: none" ></asp:TextBox>
            <asp:TextBox id="hdnReportsOptions" runat="server" style="display: none" ></asp:TextBox>
    </form>
</body>
</html>
