<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorklistOptions.aspx.cs" Inherits="UI_Worklist_WorklistOptions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Worklist Options</title>
    <link href="../../StyleSheets/MainStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">BODY { MARGIN: 0px }
	        .style5 { FONT-SIZE: 11px; FONT-FAMILY: Tahoma }
	        .style7 { FONT-WEIGHT: bold; COLOR: #ffffff }
	        .style9 { FONT-SIZE: 11px; COLOR: #1e3c7c; FONT-FAMILY: Tahoma }
	        .style12 { FONT-WEIGHT: bold; FONT-SIZE: 11px; COLOR: #1e3c7c; FONT-FAMILY: Tahoma }
	</style>
	<script language="javascript" type="text/javascript">
	
	function OutOfOffice()
	{
	    var user = '<%= Server.UrlEncode(User.Identity.Name.ToString()) %>';
	    var roothpath = '<%= System.Configuration.ConfigurationManager.AppSettings["OutOfOfficeRootPath"] %>';
	        
	    window.showModalDialog(roothpath + "/find/DialogHost.aspx?srcStr=" + roothpath + "/OutOfOffice.aspx?PageName=WorkList&Skin=" + 'stylesheet/mainstyle_olive.css' + "&Connstr=" + '' + "&User=" + user + "&scrOpt=no&Title=Out Of Office","a","dialogheight:220px;dialogwidth:520px;center:yes;help:no;resizable:no;status:no;scroll:no")
	    return false;
	}
	
	function EmailSettings()
	{
	    // if Ie 7.0
	    if (navigator.appVersion.indexOf('MSIE 7.0') != -1)
	        window.showModalDialog('EmailNotificationSettings.aspx','a','dialogheight:220px;dialogwidth:520px;center:yes;help:no;resizable:no;status:no;scroll:no')
	    else
	        window.showModalDialog('EmailNotificationSettings.aspx','a','dialogheight:265px;dialogwidth:520px;center:yes;help:no;resizable:no;status:no;scroll:no')

   
	    return false;
	}
	
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
	
	function toggleWorklistOptions(toggleWorklist)
	{    
	    if (toggleWorklist)
	    {
	        var str = document.all("iconWorklistOptions").src;
	        
	        if (str.search("expand") > 0)
	        {
	            document.all("iconWorklistOptions").src = str.replace(/expand/, "collapse");
	            document.all("trWorklistOptions").style.display = "inline";
	        }
	        else
	        {
	            document.all("iconWorklistOptions").src = str.replace(/collapse/, "expand");
	            document.all("trWorklistOptions").style.display = "none";
	        }
    	    
	        document.all("hdnWorklistOptions").value = document.all("iconWorklistOptions").src;
        }
        else
        {
            if (document.all("hdnWorklistOptions").value != '')
            {
               var str = document.all("hdnWorklistOptions").value;
               document.all("iconWorklistOptions").src = document.all("hdnWorklistOptions").value;
            }
            else
            {
                document.all("hdnWorklistOptions").value = document.all("iconWorklistOptions").src;
                return;
            }
               
            if (str.search("expand") > 0)
	            document.all("trWorklistOptions").style.display = "none";
	        else
	            document.all("trWorklistOptions").style.display = "inline";
        }	    
        
        if (document.all("trWorklistOptions").style.display == "none")
            document.all("iconWorklistOptions").title = "View Worklist Options"
        else
            document.all("iconWorklistOptions").title = "Close Worklist Options"
	}	
	
	</script>
</head>
<body onload="toggleMoreOptions(false);toggleWorklistOptions(false);">
    <form id="form1" runat="server">
    
    <table cellspacing="2" cellpadding="4" border="0" style="vertical-align: top; width: 100%; height:100%">
												            <tr>
													            <td style="width: 188px; border: solid 1px #92AEC9; background-color:#D2DEE9; font-weight: bold; color:#000000"><span><img alt="" id="iconWorklistOptions" style="cursor:hand;" src="../../Images/icon_collapse.GIF" onclick="toggleWorklistOptions(true);" title="Close Worklist Options"/></span>&nbsp;&nbsp;&nbsp;<span>Worklist Options</span></td>
												            </tr>
												            
												            <tr id="trWorklistOptions">
													            <td style="height:100%; width: 188px; border: solid 1px #92AEC9;">
													         <table cellspacing="2" cellpadding="4" border="0" style="vertical-align: top; width: 100%; height:100%">  
												            <tr>
													            <td style="width: 180px"><span class="style12">Process Filter</span></td>
												            </tr>
												            <tr>
													            <td valign="top" style="width: 180px"><asp:checkboxlist id="chklProcess" runat="server" CssClass="ms-formdropdown" Width="160px" CellPadding="0" CellSpacing="0"></asp:checkboxlist></td>
												            </tr>
												            <tr>
													            <td valign="top" style="width: 100%"><br/>
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="ms-button" Height="24px" Text="View Items"
                                                                        Width="52%" OnClick="btnViewItems_Click" />
                                                                    <br/><br/>
                                                                 </td>
												            </tr>
											                </table>
													            </td>
												            </tr>
												            <tr>
													            <td style="width: 188px; border: solid 1px #92AEC9; background-color:#D2DEE9; font-weight: bold; color:#000000"><span><img alt="" id="iconMoreOptions" style="cursor:hand;" src="../../Images/icon_expand.GIF" onclick="toggleMoreOptions(true);" title="View More Options"/></span>&nbsp;&nbsp;&nbsp;<span>More Options</span></td>
												            </tr>
												            <tr id="trMoreOptions" style="display: none">
													            <td style="width: 188px; border: solid 1px #92AEC9; height: 30px;">
													            <table cellpadding = "3" cellspacing="0"><tr><td>
													            <table cellpadding = "3" cellspacing="1">
													                <tr><td><span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlOutOffice" runat="server"  NavigateUrl="#">Out of Office Settings</asp:HyperLink></td></tr>
													                <tr><td><span style="font-family:Wingdings 3; font-size:8px">u</span>&nbsp;&nbsp;<asp:HyperLink ID="hlEmailSettings" runat="server"  NavigateUrl="#">Email Notification Settings</asp:HyperLink></td></tr>
													            </table>
													            </td></tr></table>
                                                                </td>
												            </tr>
											            </table>				                                   
            <input id="hdnIsPostBack" runat="server" style="display: none" type="hidden" value="false" />
            <input id="hdnProcessFilter" runat="server" style="display: none" type="hidden" />
            <asp:TextBox id="hdnMoreOptions" runat="server" style="display: none" ></asp:TextBox>
            <asp:TextBox id="hdnWorklistOptions" runat="server" style="display: none" ></asp:TextBox>
    </form>
</body>
</html>
