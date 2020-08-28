<%@ control language="C#" autoeventwireup="true" inherits="UserControls_TopPanel, App_Web_toppanel.ascx.6c0a2e64" %>
<link href="../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" language="javascript" src="../JS/DateFunctions.js"></script>


<style type="text/css">
<!--
.style13 {font-family: Tahoma; font-size: 11px; color: #FFFFFF; font-weight: bold; }
.style14 {
	font-family: tahoma;
	font-size: 11px;
	font-weight: bold;
	color: #FFFFFF;
}
.style15 {font-size: 11px; color: #FF5100; font-family: tahoma;}
.style16 {color: #FFFFFF}
-->
</style>
<div id="divHeader">
<table style="width:1000px;" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td style="height:90px; width:259px;" align="left" valign="top"><img src="../Images/top1.jpg" width="259" height="90" alt="" /></td>
    <td style="height:90px; width:161px;" align="left" valign="top"><img src="../Images/top2.jpg" width="161" height="90" alt="" /></td>
    <td style="height:90px; width:134px;" align="left" valign="top"><img src="../Images/top3.jpg" width="134" height="90" alt="" /></td>
    <td style="height:90px; width:193px;" align="left" valign="top"><img src="../Images/top4.jpg" width="193" height="90" alt="" /></td>
    <td style="width:253px; background-image: url('../Images/top5.jpg'); height: 90px;" colspan="2" align="left" valign="top">
    <table style="width:253px;" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td style="height:60px;" colspan="4">&nbsp;</td>
      </tr>
      <tr>
        <td style="height:30px; width:94px;"><div style="text-align:right;"><img src="../Images/orange_icon.gif" width="7" height="7" alt=""/></div></td>
        <td style="height:30px; width:60px;"><span class="style13">&nbsp;&nbsp;<asp:HyperLink id="hlFAQ" runat="server">FAQ</asp:HyperLink></span></td>
        <td style="height:30px; width:8px;"><div style="text-align:right" class="style13"><img src="../Images/orange_icon.gif" width="7" height="7" alt=""/></div></td>
        <td style="height:30px; width:91px;"><span class="style13">&nbsp;&nbsp;<asp:HyperLink id="hlSupport" runat="server">Support</asp:HyperLink></span></td>
      </tr>
    </table>
    </td>
  </tr>
  <tr style="background-color:#000000">
    <td style="height:26px;" colspan="2" align="left"><span class="style14">&nbsp;&nbsp;&nbsp;&nbsp;Logged in as&nbsp;:&nbsp;</span><span class="style15"><asp:Label ID="lblUser" runat="server" Text="ENP-AP\ALBERTO.SANTIAGO"></asp:Label></span> </td>
    <!--td style="height: 26px">&nbsp;</td-->
    <!--td style="height: 26px">&nbsp;</td-->
    <td style="text-align:right; height: 26px;" colspan="4" >
        <span class="style14">Server Date (GMT +8) :&nbsp;</span>
        <span class="style15"> 
        <asp:Label ID="lblServerDate" runat="server"></asp:Label><span class="style16">&nbsp;|</span> 
        <asp:Label ID="lblServerTime" runat="server"></asp:Label>
        </span>&nbsp;&nbsp;
        <span class="style14">Local Date :&nbsp;</span><span class="style15"> 
        <asp:Label ID="lblSystemDate" runat="server"></asp:Label><span class="style16">&nbsp;|</span> 
        <asp:Label ID="lblSystemTime" runat="server"></asp:Label>
        </span>&nbsp;&nbsp;&nbsp;&nbsp;
    </td>
  </tr>
  <!--tr style="background-color:#FFFFFF">
    <td style="height:1px;" colspan="5">
    <img src="../Images/spacer.gif" height="0" alt=""/></td>
  </tr-->  
</table>
</div>