<%@ page language="C#" autoeventwireup="true" inherits="UI_Maintenance, App_Web_maintenance.aspx.76ff77b3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ENP WorkFlow System - Maintenance</title>
    <link href="../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="8" style="width:100%">
                           <tr valign="top">
                           <td rowspan="2" style="width:50px">
                               <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/settings.gif" />
                           </td>
                           <td id="tdErrorMessage" runat="server">
                           <table cellpadding="0" cellspacing="0"> 
                           <tr><td style="color: Red; font-size:18px; font-weight: bold">Site Maintenance</td></tr>
                           <tr><td>
                            <br/>
                               <span style="font-size: 12px; color: #000000">ENP Workflow System is currently under maintenance.<br />
                               </span><span style="font-size:12px; color:#000000;">
                                   <br />
                                   Please try accessing the site again later.</span></td></tr>
                           </table>
                          </td>
                           </tr>                            

        </table>    
    </div>
    </form>
</body>
</html>
