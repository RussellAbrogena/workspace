<%@ page language="C#" autoeventwireup="true" inherits="UI_Reports_ReportViewer, App_Web_lfbwky6a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Viewer</title>
    
    <!--#include file='../../JS/PopUpCalendar.js'-->
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="">
    
    var ReportViewerURL = new  String('<%= System.Configuration.ConfigurationManager.AppSettings["ReportViewerURL"].ToString()%>');
    var Username = '<%= User.Identity.Name.ToString()%>';
    var ReportName = '<%= Request.QueryString["rname"]%>';
    
    function displayReportViewer()
    {
        var ParamValues = '';
        
        switch (ReportName)
        {
            case 'InitiatedRequests':
            {
                ParamValues = 'dtStartDateFrom=' + document.getElementById('txtDateFrom').value + '&dtStartDateTo=' + document.getElementById('txtDateTo').value + '&sUsername=' + Username;
            }
           case 'ApprovedDeniedRequests':
            {
                ParamValues = 'dtADDateFrom=' + document.getElementById('txtDateFromAD').value + '&dtADDateTo=' + document.getElementById('txtDateToAD').value + '&sUsername=' + Username;
            }
            
            
        }
        
        ReportViewerURL = ReportViewerURL.replace('{ReportName}', ReportName);
        ReportViewerURL = ReportViewerURL.replace('{ParamValues}', ParamValues);
        ReportViewerURL = ReportViewerURL.replace('\\','')
        ReportViewerURL = ReportViewerURL.replace('ENP-AP','ENP-AP\\')
              
        window.open(ReportViewerURL, '', 'top=0, left=0,height=600, width=900, toolbar=no, scrollbars=yes, resizable=yes'); 
        
    }
    
    function positionLoadingProgress()
    {
        	if ((window.screen.height/window.screen.width) == 0.75) // normal resolution
            {
		        var top = parseInt((window.screen.height / 2) + 20)
                var left = parseInt((window.screen.width  / 2) + 50)	        
		    }
		    else // widescreen
            {		        
		        var top = parseInt((window.screen.height / 2) - 10)
                var left = parseInt((window.screen.width  / 2) - 90)
            }
        
            parent.document.getElementById("divLoading").style.display = "";
            parent.document.getElementById("divLoading").style.left = left + "px";
            parent.document.getElementById("divLoading").style.top = top + "px";    
    }
    
    function CheckProcessFilter(sender, args)
    {
        var elem;
        
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= chklProcess.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }        
        
        args.IsValid = false;    
    }
  
      
    function CheckCountryFilter(sender, args)
    {
        var elem;
        
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= chklCountry.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }        
        
        args.IsValid = false;    
    }
    
    function CheckStatusFilter(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= chklStatus.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
    function CheckBestCostFilter(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= chklBestCost.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
   function CheckDepartmentFilter(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= ddlDepartment.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].item)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
   function CheckTravellerFilter(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= ddlTraveller.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].item)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
    function CheckStatusFilterAD(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= chklStatusAD.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
    function CheckARTypeFilter(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= chklARType.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
  function CheckDataFieldsFilter(sender, args)
    {
        for(var i=0; i<document.form1.elements.length; i++)
        {
            if (document.form1.elements[i].name.indexOf("<%= cb_Datafields.ID.ToString() %>") >= 0)
                if(document.form1.elements[i].checked)
                {
                    args.IsValid = true;
                    return;
                }        
                    
        }
        
        args.IsValid = false; 
    }
    
    
    
    </script>
</head>
<body style="margin-top: 0px; margin-bottom: 0px; margin-left: 0px; height: 100%; width: 100%; background-color: #CAD2E0">
    <form id="form1" runat="server">
      <table id="tblMain" style="height: 100%; width: 100%; background-color: #CAD2E0" cellspacing="3">
            <tr>
                <td valign="top" style="width: 802px">
                                 <table class="MainPanelTable" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td valign="top" style="height: 572px; width: 100%;">
                                <table width="100%" border="0" cellspacing="1" cellpadding="1">
                                      <tr>
                                        <td colspan="2" style="padding-left: 20px; height: 40px;">
                                        <asp:Label ID="lblReportName" runat="server" Text="No records found" Style="font-weight: bold" CssClass="Report-headertitle" Width="100%"></asp:Label></td>
                                        </tr>
                                      <tr>
                                        <td colspan="2" style="padding-left: 20px; padding-right: 20px;">
                                        <table width="100%" height="1px" border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td style="height: 1px; background-image: url(../../Images/dotted_line.gif)">
                                                <img src="../../Images/spacer.gif" /></td>
                                          </tr>
                                        </table><br /></td>
                                      </tr>
                                      <tr id="tr1" runat="server">
                                        <td style="padding-left: 20px; width: 350px;" class="Report-filtertitle"></td>
                                        <td rowspan="7" align="left" valign="top" style="width: 414px; padding-left: 10px; padding-top: 16px;">
                                        <table cellpadding="0" cellspacing="0" style="width: 395px; background-color: #FEFBE0; padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px;" class="Report-Note">
                                                <tr id="trNoteInitiated" runat="server">
                                                    <td style="width: 100px; height: 100px; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px;" align="left" valign="top" class="Report-note">
                                                        <asp:Label ID="lbl_Note" runat="server" CssClass="Report-notetitle" Text="Note:" Height="20px"></asp:Label><br />
                                                        <asp:Label ID="lbl_notemessage" runat="server" CssClass="Report-notemessage" Text="Message here (Initiated Requests)...." Height="123px" Width="373px"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr id="trNoteAD" runat="server">
                                                    <td style="width: 100px; height: 100px; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px;" align="left" valign="top" class="Report-note">
                                                        <asp:Label ID="Label7" runat="server" CssClass="Report-notetitle" Text="Note:" Height="20px"></asp:Label><br />
                                                        <asp:Label ID="Label8" runat="server" CssClass="Report-notemessage" Text="Message here (Approved & Denied Requests)...." Height="123px" Width="373px"></asp:Label><br />
                                                    </td>
                                                </tr>
                                           <tr id="trDataFields" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px; background-color: white;" class="Report-filtertitle">
                                            <asp:Label id="Label13" runat="server" Width="164px" Text="DataFields"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px; height: 49px;" valign="middle">
                                                        &nbsp; &nbsp;&nbsp;&nbsp;
                                                        <asp:CheckBoxList ID="cb_Datafields" runat="server" CssClass="Report-fieldtitle" RepeatColumns="2" Width="100%" Height="100%">
                                                        </asp:CheckBoxList></td>
                                                </tr>
                                            </table>
                                            &nbsp;<br />
                                            <asp:CustomValidator ID="cvDatafields" runat="server" ClientValidationFunction="CheckDataFieldsFilter"
                                                CssClass="ms-errormessage" Display="None" ErrorMessage="At least one Datafield Filter should be selected.   "></asp:CustomValidator></td>
                                      </tr>                                    
                                       <tr id="trDepartment" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px; background-color: white;" class="Report-filtertitle">
                                            <asp:Label id="lbl_Department" runat="server" Width="164px" Text="Department"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px; height: 49px;" valign="middle">
                                                        &nbsp; &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="Report-fieldtitle" AutoPostBack="True" Enabled="False">
                                                            <asp:ListItem>&lt; Select All &gt;</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                            &nbsp;<br />
                                        </td>
                                      </tr>
                                       <tr id="trTraveller" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px; background-color: white;" class="Report-filtertitle">
                                            <asp:Label id="Lbl_Traveller" runat="server" Width="164px" Text="Traveller's Name"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px" valign="middle">
                                                        &nbsp; &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlTraveller" runat="server" CssClass="Report-fieldtitle">
                                                        </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                            &nbsp;<br />
                                        </td>
                                      </tr>
                                           <tr id="trCostCenter" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px; background-color: white;" class="Report-filtertitle">
                                            <asp:Label id="lbl_CostCenter" runat="server" Width="164px" Text="Cost Center"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px" valign="middle">
                                                        &nbsp; &nbsp;&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtCostCenter" runat="server" CssClass="ms-input" Width="288px"></asp:TextBox></td>
                                                </tr>
                                            </table>  
                                        </td>
                                      </tr>
                                   <%--   <tr id="trHotelName" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px; background-color: white;" class="Report-filtertitle">
                                            <asp:Label id="Lbl_HotelName" runat="server" Width="164px" Text="Hotel Name"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px" valign="middle">
                                                        &nbsp; &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlHotelName" runat="server" CssClass="Report-fieldtitle">
                                                        </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                                <asp:CustomValidator ID="cv_HotelName" runat="server" ClientValidationFunction="CheckHotelNameFilter"
                                                    ErrorMessage="At least one Hotel Name Filter should be selected.   " Display="None" CssClass="ms-errormessage"></asp:CustomValidator>&nbsp;<br />
                                        </td>
                                      </tr>--%>
                                        
                                            </table>
                                        </td>
                                      </tr>
                                      <tr id="trDateFilter" runat="server">
                                        <td style="padding-left: 20px; width: 350px; height: 73px;" class="Report-filtertitle">
                                            &nbsp;<asp:Label ID="lbl_DateSubmitted" runat="server" Text="Date Submitted"></asp:Label>
                                              <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="Report-td" style="height: 30px;" align="left" valign="middle">
                                                        &nbsp;<asp:Label ID="Label1" runat="server" Text="From" Style="font-weight: normal;" CssClass="Report-fieldtitle"></asp:Label>
                                                       <asp:TextBox CssClass="ms-input" ID="txtDateFrom" runat="server" Width="70px"></asp:TextBox>
                                                        <img id="imgDateFrom" runat="server" alt="" height="16" src="../../Images/calendarButton.gif" style="width: 16px; height: 16px" width="16" />
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="Label2" runat="server" Text="To" Style="font-weight: normal;" CssClass="Report-fieldtitle"></asp:Label>
                                                        <asp:TextBox CssClass="ms-input" ID="txtDateTo" runat="server" Width="70px"></asp:TextBox>
                                                        <img id="imgDateTo" runat="server" alt="" height="16" src="../../Images/calendarButton.gif" style="width: 16px; height: 16px" width="16" /></td>
                                                </tr>
                                       
                                            </table><br />
                                        </td>
                                      </tr>         
                                         <tr id="trApprovalDateFilter" runat="server">
                                        <td style="padding-left: 20px; width: 350px;" class="Report-filtertitle">
                                            &nbsp;<asp:Label ID="Label11" runat="server" Text="Date Approved"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="Report-td" style="height: 30px;" align="left" valign="middle">
                                                        &nbsp;<asp:Label ID="Label9" runat="server" Text="From" Style="font-weight: normal;" CssClass="Report-fieldtitle"></asp:Label>
                                                       <asp:TextBox CssClass="ms-input" ID="txtApprovedDateFrom" runat="server" Width="70px"></asp:TextBox>
                                                        <img id="imgApprovedDateFrom" runat="server" alt="" height="16" src="../../Images/calendarButton.gif" style="width: 16px; height: 16px" width="16" />
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="Label10" runat="server" Text="To" Style="font-weight: normal;" CssClass="Report-fieldtitle"></asp:Label>
                                                        <asp:TextBox CssClass="ms-input" ID="txtApprovedDateTo" runat="server" Width="70px"></asp:TextBox>
                                                        <img id="imgApprovedDateTo" runat="server" alt="" height="16" src="../../Images/calendarButton.gif" style="width: 16px; height: 16px" width="16" /></td>
                                                </tr>
                                            
                                            </table><br />
                                        </td>
                                      </tr>                                    
                                    <tr id="trInitiatedRequests" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px;" class="Report-filtertitle">
                                            <asp:Label id="lbl_StatusFilter" runat="server" Width="164px" Text="Status Filter"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px" valign="middle">
                                                <asp:checkboxlist RepeatLayout="Table" id="chklStatus" runat="server" CssClass="Report-fieldtitle" Width="288px" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" BorderColor="#92AEC9" BorderWidth="0px" Height="40px">
                                                <asp:ListItem Selected="True" Value="Pending">Pending&amp;nbsp;&amp;nbsp;</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="Approved">Approved&amp;nbsp;&amp;nbsp;</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="Rejected">Rejected&amp;nbsp;&amp;nbsp;</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="Cancelled">Cancelled&amp;nbsp;&amp;nbsp;</asp:ListItem>                                                
                                            </asp:checkboxlist></td>
                                                </tr>
                                            </table>
                                                <asp:CustomValidator ID="cvStatusFilter" runat="server" ClientValidationFunction="CheckStatusFilter"
                                                    ErrorMessage="At least one Status Filter should be selected.   " Display="None" CssClass="ms-errormessage"></asp:CustomValidator>&nbsp;<br />
                                        </td>
                                      </tr>
                                        <tr id="trBestCost" runat="server">
                                        <td style="padding-left: 20px; height: 21px; width: 350px;" class="Report-filtertitle">
                                            <asp:Label id="lbl_BestCost" runat="server" Width="164px" Text="Best Cost"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                                <tr>
                                                    <td align="left" class="Report-td" style="width: 414px" valign="middle">
                                                        <asp:CheckBoxList ID="chklBestCost" runat="server" RepeatColumns="2" CssClass="Report-fieldtitle">
                                                            <asp:ListItem Value="True" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                                                        </asp:CheckBoxList></td>
                                                </tr>
                                            </table>
                                                <asp:CustomValidator ID="cv_BestCost" runat="server" ClientValidationFunction="CheckBestCostFilter"
                                                    ErrorMessage="At least one BestCost Filter should be selected.   " Display="None" CssClass="ms-errormessage"></asp:CustomValidator>&nbsp;<br />
                                        </td>
                                      </tr>  
                                      
                                   <tr id="trApprovedDeniedRequests" runat="server">
                                    <td style="padding-left: 20px; height: 21px; width: 350px;" class="Report-filtertitle">
                                        <asp:Label id="Label6" runat="server" Width="164px" Text="Status Filter"></asp:Label>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                            <tr>
                                                <td align="left" class="Report-td" style="width: 414px; height: 40px;" valign="top">
                                        <asp:checkboxlist RepeatLayout="Table" id="chklStatusAD" runat="server" CssClass="Report-fieldtitle" Width="288px" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" BorderColor="#92AEC9" BorderWidth="0px" Height="40px">
                                            <asp:ListItem Selected="True" Value="2">All items that I APPROVED&amp;nbsp;&amp;nbsp;</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="16">All items that I DECLINED&amp;nbsp;&amp;nbsp;</asp:ListItem>                                             
                                        </asp:checkboxlist></td>
                                            </tr>
                                        </table>
                                            <asp:CustomValidator ID="cvStatusFilterAD" runat="server" ClientValidationFunction="CheckStatusFilterAD"
                                                ErrorMessage="At least one Status Filter should be selected.   " Display="None" CssClass="ms-errormessage"></asp:CustomValidator>&nbsp;<br />
                                    </td>
                                  </tr>
                                    <tr id="trCountry" runat="server">
                                    <td style="padding-left: 20px; height: 21px; width: 350px;" class="Report-filtertitle">
                                        <asp:Label id="Label12" runat="server" Width="164px" Text="Country"></asp:Label>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
                                            <tr>
                                                <td align="left" class="Report-td" style="width: 414px; height: 40px;" valign="top">
                                                    <asp:CheckBoxList ID="chklCountry" runat="server" CssClass="Report-fieldtitle" CellSpacing="2" RepeatColumns="2" CellPadding="2" OnSelectedIndexChanged="chklCountry_SelectedIndexChanged">
                                                    </asp:CheckBoxList></td>
                                            </tr>
                                        </table>
                                            <asp:CustomValidator ID="cvCountry" runat="server" ClientValidationFunction="CheckCountryFilter"
                                                ErrorMessage="At least one Country Filter should be selected.   " Display="None" CssClass="ms-errormessage"></asp:CustomValidator>&nbsp;<br />
                                    </td>
                                  </tr>  
                                        <tr id="trProcessFilter" runat="server">
                                        <td style="padding-left: 20px; width: 350px;" class="Report-filtertitle">
                                            <asp:Label id="lbl_ProcessFilter" runat="server" Text="Include these processes "></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="Report-td" style="width: 100px; height: 200px;" align="left" valign="top">
                                            <asp:checkboxlist RepeatLayout="Table" id="chklProcess" runat="server" CssClass="Report-fieldtitle" Width="288px" CellPadding="0" CellSpacing="0" BorderColor="#92AEC9" BorderWidth="0px"></asp:checkboxlist></td>
                                                </tr>
                                            </table>
                                                <asp:CustomValidator ID="cvProcessFilter" runat="server" ClientValidationFunction="CheckProcessFilter"
                                                    ErrorMessage="At least one Process Filter should be selected.   " Display="None" CssClass="ms-errormessage"></asp:CustomValidator>
                                                    <br />
                                        </td>
                                      </tr>
                                         <tr id="trProcessFilterDDL" runat="server">
                                        <td style="padding-left: 20px; width: 350px;" class="Report-filtertitle">
                                            <asp:Label id="lbl_ProcessFilterDDL" runat="server" Text="Please select a process"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 1px;">
                                                <tr>
                                                    <td class="Report-td" style="width: 100px; height: 20px;" align="left" valign="top">
                                                        <asp:DropDownList ID="ddl_ProcessFilter" runat="server" CssClass="Report-fieldtitle" Width="288px" AutoPostBack="True" OnSelectedIndexChanged="ddl_ProcessFilter_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp;<br />
                                            <asp:CompareValidator ID="cv_DLLProcessFilter" runat="server" ControlToValidate="ddl_ProcessFilter"
                                                CssClass="ms-errormessage" Display="None" ErrorMessage="At least one Process Filter should be selected.   "
                                                Operator="NotEqual" ValueToCompare="Select"></asp:CompareValidator></td>
                                      </tr>
                                      <tr id="trARTypeFilter" runat="server" style="height:10px;">
                                       <td style="padding-left: 20px; width: 350px;" class="Report-filtertitle">
                                            <asp:Label id="Label3" runat="server" Text="AR Type Filter"></asp:Label>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="Report-td" style="width: 100px; height: 200px;" align="left" valign="top">
                                                <asp:checkboxlist RepeatLayout="Table" id="chklARType" runat="server" CssClass="Report-fieldtitle" Width="288px" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" BorderColor="#92AEC9" BorderWidth="0px">
                                                <asp:ListItem Selected="True" Value="1">CAPEX for IT</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="2">Non-IT CAPEX</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="3">CAPEX Disposal NBV</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="4">Motor Vehicle Lease Agreement</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="5">Office Lease Agreement</asp:ListItem>
                                            </asp:checkboxlist>&nbsp;
                                            </td>
                                                </tr>
                                            </table>
                                            <asp:CustomValidator ID="cvARTypeFilter" runat="server" ClientValidationFunction="CheckARTypeFilter"
                                                    ErrorMessage="At least one AR Type Filter should be selected.   " Display="None"></asp:CustomValidator><br />
                                        </td>
                                      </tr>
                                      
                                      <tr>
                                        <td style="padding-left: 20px; width: 350px;">
                                        <asp:Button ID="btnViewReport" runat="server" CssClass="Report-buttons" Height="24px" Text="View Report" Width="100px" OnClick="btnViewReport_Click"/></td>
                                      </tr>
                                      <tr>
                                        <td style="padding-left: 20px; width: 350px;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" CssClass="ms-errormessage" Width="136px" />
                                            </td>
                                      </tr>                                      
                                </table>
                            </td>
                        </tr>
                       
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

