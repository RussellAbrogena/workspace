<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorklistItems.aspx.cs" Inherits="UI_Worklist_WorklistItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Worklist Items</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="">
    
    
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
<body style="margin-top: 0px; margin-bottom: 0px; margin-left: 0px; height: 100%; width: 100%">
    <form id="form1" runat="server">
        <table id="tblMain" style="height: 100%; width: 100%; background-color: #CAD2E0"
            cellspacing="3">
            <tr>
                <td valign="top">
                    <table class="MainPanelTable">
<%--                        <tr style="height: 1px">
                            <td>
                                <img src="../../Images/spacer.gif" alt="" /></td>
                        </tr>
                        <tr>
                            <td valign="bottom">
                                <span class="Text13">&nbsp;&nbsp;Worklist Items</span>
                                <span style="vertical-align: middle;
                                    font-weight: bold; color: #92AEC9">&nbsp;::&nbsp;</span>
                                <asp:Label ID="lblSearchMatchNo" runat="server" Text="No records found" Style="font-weight: bold"
                                    CssClass="ms-redtext"></asp:Label>
                                <br />
                                <hr style="color: #92aec9; height: 1px;" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td valign="top">
                                 <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <tr style="height: 5px">
                                    <td style="width: 749px" ><img src="../../Images/spacer.gif" alt="" /></td>
                                    <td style="text-align:right; vertical-align:middle;" rowspan="2">
                                        <asp:ImageButton ID="ibtnRefresh" runat="server" Height="16px" ImageUrl="~/Images/refresh.JPG" OnClick="ibtnRefresh_Click" ToolTip="Refresh" Width="16px" AlternateText="Refresh" BackColor="White" BorderStyle="Solid" BorderWidth="0" BorderColor="#92AEC9" Visible="False" />
                                        &nbsp;
                                    </td>                                        
                                    </tr>
                                    <tr>
                                    <td valign="top" style="height:20px; width: 749px;">
                                        <span class="Text13">&nbsp;&nbsp;Worklist Items</span><span style="vertical-align: top; font-weight: bold; color: #92AEC9">&nbsp;&nbsp;::&nbsp;</span>
                                        <asp:Label ID="lblSearchMatchNo" runat="server" Text="No records found" Style="font-weight: bold" CssClass="ms-redtext"></asp:Label>
                                        &nbsp;
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2" style="height:1px; border-top: solid 1px #92aec9">
                                    <img src="../../Images/spacer.gif" alt="" />
                                    </td>
                                    </tr>
                                </table>        
                            </td>
                        </tr>                       
                        <tr style="height: 523px;" valign="top">
                            <td style="height: 584px">
                                <table cellspacing="8" style="width: 100%">
                                    <tr valign="top">
                                        <td>
                                                <div id="divGrid">
                                                <asp:GridView ID="gvWorklistItems" BorderColor="#92AEC9" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="3" AllowPaging="True" AllowSorting="true" DataKeyNames="ApprovalPageUrl" HorizontalAlign="Left" PageSize="14"
                                                    OnPageIndexChanging="gvWorklistItems_PageIndexChanging" OnSelectedIndexChanged="gvWorklistItems_SelectedIndexChanged" OnSorting="gvWorklistItems_Sorting" OnRowDataBound="gvWorklistItems_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Reference ID" SortExpression="Folio">
                                                            <HeaderStyle Wrap="false" />
                                                            <ItemStyle Width="13%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnReferenceID" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Eval("Folio") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process Name" SortExpression="ProcSetName">
                                                            <HeaderStyle Wrap="false" />
                                                            <ItemStyle Width="14%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnProcesName" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Eval("ProcSetName") %>'></asp:LinkButton>
                                                                   
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Initiator" SortExpression="DisplayName">
                                                            <HeaderStyle Wrap="false" />
                                                            <ItemStyle Width="16%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnInitiator" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Emerson.WF.Common.GetDisplayName(Eval("Originator").ToString(), Eval("DisplayName").ToString()) %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date (GMT+8)" SortExpression="StartDate">
                                                            <ItemStyle Width="12%" HorizontalAlign="Center" Wrap="false" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnStartDate" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# String.Format(Emerson.WF.Konstants.DateFormat.TimeDate, Eval("StartDate")) %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="ReqStatus">
                                                            <HeaderStyle Wrap="false" />
                                                            <ItemStyle Width="8%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnStatus" Style="text-transform: capitalize;" runat="server"
                                                                    CommandName="Select" CausesValidation="false" Font-Underline="false" Text='<%# Emerson.WF.Common.ToProperCase(Eval("ReqStatus").ToString()) %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last Activity" SortExpression="LastActivity">
                                                            <HeaderStyle Wrap="false" />
                                                            <ItemStyle Width="18%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnLastActivity" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Eval("LastActivity") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last User" SortExpression="LastActivityUserDisplayName">
                                                            <HeaderStyle Wrap="false" />
                                                            <ItemStyle/>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnLastActivityUser" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Emerson.WF.Common.GetDisplayName(Eval("LastActivityUser").ToString(), Eval("LastActivityUserDisplayName").ToString()) %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                        
                                                        <asp:TemplateField HeaderText="Age (DD:HH:MM)" SortExpression="ActivityAge">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnActivityAge" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Emerson.WF.Common.FormatAge(Convert.ToInt32(Eval("ActivityAge"))) %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeaderStyle" ForeColor="White" Font-Bold="True"/>
                                                    <AlternatingRowStyle CssClass="GridAlternatingRowStyle" />
                                                    <RowStyle CssClass="GridRowStyle" />
                                                    <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                </asp:GridView>
                                            </div>
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

