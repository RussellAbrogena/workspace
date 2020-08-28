<%@ page language="C#" autoeventwireup="true" inherits="UI_Worklist_EmailNotificationSettings, App_Web_emailnotificationsettings.aspx.1ce3260c" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ENP Workspace</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <base target="_self">
</head>
<body>
    <form id="form1" runat="server">
<table id="tblMain" style="width:100%; height:100%; background-color: #CAD2E0" cellspacing="3">
            <tr>
                <td>
                    <table class="MainPanelTable" style="height: 212px">
                        <tr style="width:100%; height:20px;">
                            <td>
                                 <table style="width:100%; height:28px;" cellpadding="0" cellspacing="0">
                                    <tr><td style="height:16px; width: 749px; vertical-align:middle;">
                                        <span style="font-weight: bold; color:#000000">&nbsp;&nbsp;&nbsp;Email Notification Settings</span>
                                    </td></tr>
                                    <tr><td style="height:1px;"><img src="../../Images/spacer.gif" alt="" /></td></tr>
                                    <tr><td style="height:1px; border-top: solid 1px #92aec9"><img src="../../Images/spacer.gif" alt="" /></td></tr>
                                </table>        
                            </td>
                        </tr>                       
                        <tr valign="top">
                            <td valign="top">
                                <table cellspacing="8" style="width: 100%;">
                                    <tr><td valign="top">
                                        <table cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr><td colspan="2"><asp:RadioButton ID="rbtnOff" runat="server" Text="Turn Off Weekly Worklist Summary" CssClass="ms-formlabel" GroupName="WWS"/></td></tr>
                                            <tr><td colspan="2" style="height:2px;"><img src="../../Images/spacer.gif" alt="" /></td></tr>
                                            <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td><asp:Label ID="lblDesc1" runat="server" Text="Configurable Message"></asp:Label></td></tr>
                                            <tr><td colspan="2" style="height:10px;"><img src="../../Images/spacer.gif" alt="" /></td></tr>
                                            <tr><td colspan="2"><asp:RadioButton ID="rbtnOn" runat="server" Text="Turn On Weekly Worklist Summary" CssClass="ms-formlabel" GroupName="WWS"/>
                                            <span style="display:none;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlDays" runat="server" CssClass="ms-input">
                                                    <asp:ListItem Selected="True" Value="">&lt;Select Day&gt;</asp:ListItem>
                                                    <asp:ListItem>Monday</asp:ListItem>
                                                    <asp:ListItem>Tuesday</asp:ListItem>
                                                    <asp:ListItem>Wednesday</asp:ListItem>
                                                    <asp:ListItem>Thursday</asp:ListItem>
                                                    <asp:ListItem>Friday</asp:ListItem>
                                                    <asp:ListItem>Saturday</asp:ListItem>
                                                    <asp:ListItem>Sunday</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>    
                                                <asp:RequiredFieldValidator ID="rfvDay" runat="server" ErrorMessage="Day is required.   " ControlToValidate="ddlDays" Display="None"></asp:RequiredFieldValidator></td></tr>
                                            <tr><td colspan="2" style="height:5px;"><img src="../../Images/spacer.gif" alt="" /></td></tr>
                                            <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td><asp:Label ID="lblDesc2" runat="server" Text="Configurable Message"></asp:Label></td></tr>    
                                            <tr><td colspan="2" style="height:15px;"><img src="../../Images/spacer.gif" alt="" /></td></tr>
                                            <tr><td colspan="2" style="text-align:right;">
                                                <asp:Button ID="btnSave" runat="server" Text="   Save   " CssClass="ms-button" OnClick="btnSave_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnClose" runat="server" Text="   Close   " CssClass="ms-button" />
                                                &nbsp;&nbsp;
                                            </td></tr>
                                                
                                        </table>
                                    </td></tr>
                                </table>
                                <asp:ValidationSummary ID="vs" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
