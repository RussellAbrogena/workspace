<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="UI_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ENP WorkFlow System - Error</title>
    <link href="../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="8" style="width:100%">
                           <tr valign="top">
                           <td rowspan="2" style="width:50px">
                               <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/error.gif" />
                           </td>
                           <td id="tdErrorMessage" runat="server">
                           <table cellpadding="0" cellspacing="0"> 
                           <tr><td style="color: Red; font-size:18px; font-weight: bold">System Error</td></tr>
                           <tr><td>
                            <br/>
                               <span style="font-size: 12px; color: #000000">An unexpected system error has occcurred.<br />
                               </span><span style="font-size:12px; color:#000000;">
                                   <br />
                                   For support, you may contact your system administrator.</span></td></tr>
                           </table>
                          </td>
                           </tr>                            

                           <tr id="Tr1" valign="top" runat="server">
                           <td id="tdSessionExpired">
                           <table cellpadding="0" cellspacing="0"> 
                           <tr><td style="color: Red; font-size:18px; font-weight: bold; width: 250px;">
                           Session Expired
                           </td></tr>
                           <tr><td style="height: 54px; width: 250px;">
                            <br/><span style="font-size:12px; color:#000000;">Your session has expired due to inactivity.</span>
                           <br/><br/>
                           <span style="font-size:12px; color:#000000;">
                           Click <a>here</a> to re-login and start a new session.
                           </span>
                           </td></tr>
                           </table>                          
                           </td>
                           </tr>
        </table>    
    </div>
    </form>
</body>
</html>
