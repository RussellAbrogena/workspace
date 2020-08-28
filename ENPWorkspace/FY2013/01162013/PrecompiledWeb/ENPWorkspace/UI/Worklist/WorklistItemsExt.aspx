<%@ page language="C#" autoeventwireup="true" inherits="UI_Worklist_WorklistItemsExt, App_Web_worklistitemsext.aspx.1ce3260c" %>

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
        <table id="tblMain" style="height: 90%; width: 100%; background-color: #CAD2E0"
            cellspacing="3">
            <tr>
                <td valign="top" style="height: 448px">
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
                                        <asp:ImageButton ID="ibtnRefresh" runat="server" Height="16px" ImageUrl="~/Images/refresh.JPG"  ToolTip="Refresh" Width="16px" AlternateText="Refresh" BackColor="White" BorderStyle="Solid" BorderWidth="0" BorderColor="#92AEC9" />
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
                        <tr style="height: 423px;" valign="top">
                            <td style="height: 523px">
                                <table cellspacing="8" style="width: 80%">
                                    <tr valign="top">
                                        <td align="left" class="PendingLabel" style="width: 667px" valign="top">
                                            <asp:Label ID="lblPendingItems" runat="server"></asp:Label></td>
                                        <td align="right" valign="top">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="left" valign="top" style="padding-left: 50px; width: 667px">
                                                <div id="divGrid">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:GridView ID="gvWorklistItemsExt" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="Transparent" BorderStyle="None" BorderWidth="0px"
                                                        CellPadding="5" PageSize="30"  DataKeyNames="ID" ShowHeader="False" OnSelectedIndexChanged="gvWorklistItemsExt_SelectedIndexChanged">
                                                        <Columns>                                                         
                                                            <asp:TemplateField HeaderText="ID">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="ID" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Eval("ID") %>' Visible=false></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ProcessName">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnProcesName" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="true" Text='<%# Eval("ProcessName") %>' CssClass="PendingItems"></asp:LinkButton>
                                                                   
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Pending">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPending" runat="server" CommandName="Select" CausesValidation="false"
                                                                    Font-Underline="false" Text='<%# Eval("Pending") %>' CssClass="PendingCounts"></asp:LinkButton>
                                                                   
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>                                                    
                                                        <RowStyle BorderColor="Silver" BorderStyle="Double" BorderWidth="1px" />
                                                        <EditRowStyle BorderColor="#E0E0E0" />
                                                        <SelectedRowStyle BorderColor="#E0E0E0" />
                                                    </asp:GridView>
                                                    </div>
                                        </td>
                                        <td align="right" style="padding-left: 50px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <em class="PendingFooter">&nbsp; Version&nbsp;
                                    <asp:Label ID="lblVersion" runat="server" Font-Bold="True" Font-Italic="True" Text="1.0" CssClass="PendingFooter"></asp:Label></em><br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

