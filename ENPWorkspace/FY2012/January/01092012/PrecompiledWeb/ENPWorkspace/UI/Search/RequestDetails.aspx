<%@ page language="C#" autoeventwireup="true" inherits="UI_Search_RequestDetails, App_Web_requestdetails.aspx.187d51b8" %>

<%@ Register Src="../UserControls/Remarks.ascx" TagName="Remarks" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Request Details</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

    
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
    </script>
</head>
<body style="margin-top: 0px; margin-bottom: 0px; margin-left:0px; height:100%; width:100%">
    <form id="form1" runat="server"> 
        <table id="tblMain" style="height:100%; width:100%; background-color: #D2DEE9" cellspacing="3">
            <tr>
                <td valign="top">
                <table class="MainPanelTable">
                    <tr style="height:1px">
                        <td><img src="../../Images/spacer.gif" alt=""/></td>
                    </tr>
                    <tr>
                        <td valign="bottom">                       
                        <table style="width:100%" cellpadding="0" cellspacing="0">
                        <tr>
                        <td valign="bottom" style="height: 14px">
                        <span class="Text13">&nbsp;&nbsp;Request Details</span>
                        <span style="vertical-align:middle; font-weight:bold; color:#92AEC9">&nbsp;::&nbsp;</span>
                        <asp:Label ID="lblReferenceID" runat="server" Text="" style="font-weight: bold" CssClass="ms-redtext"></asp:Label>
                        <span runat="server" id="tdCancel" style="font-weight:bold; color:#92AEC9">&nbsp;::&nbsp;&nbsp;<asp:LinkButton CssClass="ActionButton" ID="lbtnCancel" runat="server" OnClick="lbtnCancel_Click">Cancel</asp:LinkButton></span>
                        <span runat="server" id="tdRetrieve" style="font-weight:bold; color:#92AEC9">&nbsp;::&nbsp;&nbsp;<asp:LinkButton CssClass="ActionButton" ID="lbtnRetrieveBack" runat="server" OnClick="lbtnRetrieveBack_Click">Retrieve Back</asp:LinkButton></span>
                        <span runat="server" id="tdResubmit" style="font-weight:bold; color:#92AEC9">&nbsp;::&nbsp;&nbsp;<asp:LinkButton CssClass="ActionButton" ID="lbtnResubmit" runat="server" OnClick="lbtnResubmit_Click">Resubmit</asp:LinkButton></span>
                        <span runat="server" id="tdPrint" style="font-weight:bold; color:#92AEC9">&nbsp;::&nbsp;&nbsp;<asp:LinkButton CssClass="ActionButton" ID="lbtnPrint" runat="server">Print</asp:LinkButton></span>
                        <span runat="server" id="tdActionResult" style="font-weight:bold; color:#92AEC9">&nbsp;::&nbsp;&nbsp;<asp:Label ID="lblActionResult" runat="server"></asp:Label></span>
                        </td>
                        <td style="text-align:right; height: 14px;" valign="bottom">
                        <span style="font-weight:bold;"><asp:LinkButton CssClass="ActionButton" ID="lbtnBackToResults" runat="server" OnClick="lbtnBackToResults_Click">[Back to Search Results]</asp:LinkButton></span>
                        &nbsp;&nbsp;
                        </td>
                        
                        </tr>
                        </table>
                        <hr style="color: #92aec9; height: 1px;" />
                        </td>
                        
                    </tr>
                     <tr id="trDetailsOld" runat="server" style="height: 528px;" valign="top" > 
                        <td>
                           <table cellspacing="8" style="width:100%" id="tblOldView" runat="server">
                           <tr valign="top"><td style="height: 495px"> 
                           > <asp:LinkButton id="lbtnSwitch" runat="server" OnClick="lbtnSwitch_Click">View Audit Trail/Remarks</asp:LinkButton><br/><br/>
                            <div id="divGrid">
                            <table style="width:100%">
                            <tr><td>
                            <asp:GridView  ID="gvRequestDetails" BorderColor="#92AEC9"  runat="server" AutoGenerateColumns="False" CellPadding="3" AllowPaging="True"
                                HorizontalAlign="Left" PageSize="20" OnPageIndexChanging="gvRequestDetails_PageIndexChanging" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="MetaData" HeaderText="Field Name"><ItemStyle Width="50%" ForeColor="Black" /></asp:BoundField>
                                    <asp:BoundField DataField="Value" HeaderText="Current Value"><ItemStyle Width="50%" ForeColor="Black" /></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="GridHeaderStyle"/>
                                <AlternatingRowStyle CssClass="GridAlternatingRowStyle" />
                                <RowStyle CssClass="GridRowStyle"/>
                                <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                                <PagerSettings Mode="NumericFirstLast"  />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            </asp:GridView>
                            </td></tr>                            
                            
                            <tr runat="server" id="trAuditTrail"><td>
                            <u><b>Audit Trail</b></u><br/><br/>
                            <asp:GridView  ID="gvAuditTrail" BorderColor="#92AEC9"  runat="server" AutoGenerateColumns="False" CellPadding="3" AllowPaging="True"
                                HorizontalAlign="Left" PageSize="50" Width="100%" OnPageIndexChanging="gvAuditTrail_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="dCreateAt" HeaderText="Time / Date"><ItemStyle Width="34%" ForeColor="Black" /></asp:BoundField>
                                    <asp:BoundField DataField="sUserName" HeaderText="User"><ItemStyle Width="33%"  ForeColor="Black"/></asp:BoundField>
                                    <asp:BoundField DataField="sEventType" HeaderText="Event"><ItemStyle Width="33%" ForeColor="Black" /></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="GridHeaderStyle"/>
                                <AlternatingRowStyle CssClass="GridAlternatingRowStyle" />
                                <RowStyle CssClass="GridRowStyle"/>
                                <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                                <PagerSettings Mode="NumericFirstLast"  />
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            </asp:GridView>
                            </td></tr>
                            <tr><td>&nbsp;</td></tr>
                            
                            <tr runat="server" id="trRemarks"><td>
                            <br/><u><b>Remarks</b></u><br/><br/>
                            <uc1:Remarks ID="ucRemarks" runat="server" />
                            </td></tr>
                            </table>
                            </div>
                            
                            </td></tr>
                            </table>
                          </td>
                        </tr>
                        <tr id="trDetailsNew" runat="server" style="height: 526px;" valign="top" > 
                        <td>
                            <iframe src="#" id="ViewFormFrame" runat="server" frameborder="0" width="100%" scrolling="auto" height="100%">
							</iframe>
                        </td>
                        </tr>
                        <tr id="trAccessDenied" runat="server" style="height: 526px;" valign="top" > 
                        <td>
                           <table cellspacing="8" style="width:100%">
                           <tr valign="top">
                           <td style="height: 495px; width:49px;">
                               <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ERROR.GIF" /></td>
                           <td style="height: 495px;">
                           <br/><span style="font-size:12px; color:#000000;">Sorry, you are not allowed to view the details of this request.</span>
                           <br/><br/><span style="font-size:12px; color:#000000;">For support/assistance you may need,
                               please contact your workflow administrator.</span>
                           <br/><br/><span style="font-size:12px; color:#000000;">NOTE: If you are a designated Approver of this item, you can instead view the details of this request in your worklist.</span>
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
